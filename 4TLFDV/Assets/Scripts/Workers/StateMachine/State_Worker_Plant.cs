using UnityEngine;
using System.Collections;

public class State_Worker_Plant : BaseWorkerState
{
    private readonly Farmhand farmhand;
    private bool walkingToTarget = false;
    private Vector3 targetPosition;

    public State_Worker_Plant(Farmhand worker) : base(worker)
    {
        this.farmhand = worker;
    }

    public override void OnEnterState()
    {
        if (farmhand.CurrentHandState != HANDSTATE.SEED)
        {
            farmhand.SetState(new State_Worker_GatherTaskResource(farmhand, HANDSTATE.SEED));
            return;
        }

        if (farmhand.CurrentTargetTile != null)
        {
            targetPosition = farmhand.CurrentTargetTile.transform.position;
            walkingToTarget = true;
        }
        else
        {
            farmhand.SetState(new State_Worker_AquireTargetForTask(
                farmhand,
                State_Worker_AquireTargetForTask.TaskType.PLANT
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
            farmhand.StartCoroutine(PerformPlanting());
        }
    }

    private IEnumerator PerformPlanting()
    {
        yield return new WaitForSeconds(farmhand.actionTimePlantSeed);

        if (farmhand.CurrentTargetTile != null)
        {
            farmhand.CurrentTargetTile.SetState(new State_PlantedUnwatered(farmhand.CurrentTargetTile));
        }

        farmhand.ReduceEnergy(Farmhand.EnergyCostPerFarmingAction);
        farmhand.ClearHand();
        farmhand.CurrentTargetTile = null;

        // Loop back to keep planting if assigned
        farmhand.SetState(new State_Worker_AquireTargetForTask(farmhand, State_Worker_AquireTargetForTask.TaskType.PLANT));
    }



    public override void OnExitState() { }
}
