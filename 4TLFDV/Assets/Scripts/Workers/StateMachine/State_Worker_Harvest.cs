using UnityEngine;
using System.Collections;

public class State_Worker_Harvest : BaseWorkerState
{
    private readonly Farmhand farmhand;
    private bool walkingToTarget = false;
    private Vector3 targetPosition;

    public State_Worker_Harvest(Farmhand worker) : base(worker)
    {
        this.farmhand = worker;
    }

    public override void OnEnterState()
    {
        if (farmhand.CurrentTargetTile != null)
        {
            targetPosition = farmhand.CurrentTargetTile.transform.position;
            walkingToTarget = true;
        }
        else
        {
            farmhand.SetState(new State_Worker_AquireTargetForTask(
                farmhand,
                State_Worker_AquireTargetForTask.TaskType.HARVEST
            ));
        }
    }

    public override void Tick(float deltaTime)
    {
        if (!walkingToTarget) return;

        Vector3 direction = (targetPosition - farmhand.transform.position).normalized;
        float distance = Vector3.Distance(farmhand.transform.position, targetPosition);

        if (distance > 0.05f)
        {
            farmhand.transform.position += direction * farmhand.MoveSpeed * deltaTime;
        }
        else
        {
            walkingToTarget = false;
            farmhand.transform.position = targetPosition;
            farmhand.StartCoroutine(PerformHarvesting());
        }
    }

    private IEnumerator PerformHarvesting()
    {
        yield return new WaitForSeconds(1f); // Optional delay

        if (farmhand.CurrentTargetTile != null &&
            farmhand.CurrentTargetTile.GetCurrentState() is State_ReadyToHarvest)
        {
            farmhand.CurrentTargetTile.SetState(new State_NotPlanted(farmhand.CurrentTargetTile));
        }

        Grid_Manager.Instance.Player.AddGold(20); // You can balance this later
        farmhand.ReduceEnergy(Farmhand.EnergyCostPerFarmingAction);
        farmhand.CurrentTargetTile = null;

        farmhand.SetState(new State_Worker_AquireTargetForTask(
            farmhand,
            State_Worker_AquireTargetForTask.TaskType.HARVEST
        ));
    }


    public override void OnExitState() { }
}
