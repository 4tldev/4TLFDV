using UnityEngine;
using System.Collections;

public class State_Worker_Water : BaseWorkerState
{
    private readonly Farmhand farmhand;
    private bool walkingToTarget = false;
    private Vector3 targetPosition;

    public State_Worker_Water(Farmhand worker) : base(worker)
    {
        this.farmhand = worker;
    }

    public override void OnEnterState()
    {
        // Gather water if not already holding it
        if (farmhand.CurrentHandState != HANDSTATE.WATER)
        {
            farmhand.SetState(new State_Worker_GatherTaskResource(farmhand, HANDSTATE.WATER));
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
                State_Worker_AquireTargetForTask.TaskType.WATER
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
            farmhand.StartCoroutine(PerformWatering());
        }
    }

    private IEnumerator PerformWatering()
    {
        yield return new WaitForSeconds(farmhand.actionTimeWaterCrop);

        if (farmhand.CurrentTargetTile != null &&
            farmhand.CurrentTargetTile.GetCurrentState() is State_PlantedUnwatered)
        {
            farmhand.CurrentTargetTile.SetState(new State_PlantedWatered(farmhand.CurrentTargetTile));
        }

        farmhand.ReduceEnergy(Farmhand.EnergyCostPerFarmingAction);
        farmhand.ClearHand();
        farmhand.CurrentTargetTile = null;

        farmhand.SetState(new State_Worker_AquireTargetForTask(
            farmhand,
            State_Worker_AquireTargetForTask.TaskType.WATER
        ));
    }


    public override void OnExitState() { }
}
