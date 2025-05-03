using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class State_Worker_AquireTargetForTask : BaseWorkerState
{
    private Farmhand farmhand;
    private float retryCooldown = 1f;
    private float timer = 0f;

    public enum TaskType { PLANT, WATER, HARVEST }
    private TaskType currentTask;

    public State_Worker_AquireTargetForTask(Farmhand worker, TaskType task) : base(worker)
    {
        this.farmhand = worker;
        this.currentTask = task;
    }

    public override void OnEnterState()
    {
        timer = retryCooldown;
    }

    public override void Tick(float deltaTime)
    {
        timer -= deltaTime;
        if (timer > 0f) return;

        timer = retryCooldown;
        TryFindTarget();
    }

    private void TryFindTarget()
    {
        List<FarmTile> allTiles = Grid_Manager.Instance.GetGrid().Values.ToList();
        List<FarmTile> validTiles = new();

        foreach (var tile in allTiles)
        {
            switch (currentTask)
            {
                case TaskType.PLANT:
                    if (tile.GetCurrentState() is State_NotPlanted)
                        validTiles.Add(tile);
                    break;
                case TaskType.WATER:
                    if (tile.GetCurrentState() is State_PlantedUnwatered)
                        validTiles.Add(tile);
                    break;
                case TaskType.HARVEST:
                    if (tile.GetCurrentState() is State_ReadyToHarvest)
                        validTiles.Add(tile);
                    break;
            }
        }

        var unclaimed = validTiles.Where(tile => !IsTileClaimed(tile)).ToList();

        if (unclaimed.Count == 0)
            return; // Stay in this state and retry

        var chosenTile = unclaimed[Random.Range(0, unclaimed.Count)];
        farmhand.CurrentTargetTile = chosenTile;

        switch (currentTask)
        {
            case TaskType.PLANT:
                farmhand.SetState(new State_Worker_Plant(farmhand));
                break;
            case TaskType.WATER:
                farmhand.SetState(new State_Worker_Water(farmhand));
                break;
            case TaskType.HARVEST:
                farmhand.SetState(new State_Worker_Harvest(farmhand));
                break;
        }
    }

    private bool IsTileClaimed(FarmTile tile)
    {
        foreach (var fh in Farmhand.ActiveFarmhands)
        {
            if (fh != farmhand && fh.CurrentTargetTile == tile)
                return true;
        }
        return false;
    }

    public override void OnExitState() { }
}
