using UnityEngine;
using System.Collections;

public class State_Worker_GatherTaskResource : BaseWorkerState
{
    private readonly Farmhand farmhand;
    private readonly HANDSTATE targetHandState;
    private Transform resourceTarget;
    private bool walking = false;

    public State_Worker_GatherTaskResource(Farmhand worker, HANDSTATE handType) : base(worker)
    {
        farmhand = worker;
        targetHandState = handType;
    }

    public override void OnEnterState()
    {
        resourceTarget = FindClosestResource();

        if (resourceTarget == null)
        {
            Debug.LogWarning("No valid resource target found!");
            farmhand.SetState(new State_Worker_Idle(farmhand));
            return;
        }

        walking = true;
    }

    public override void Tick(float deltaTime)
    {
        if (!walking || resourceTarget == null) return;

        Vector3 targetPos = resourceTarget.position;
        Vector3 direction = (targetPos - farmhand.transform.position).normalized;
        float distance = Vector3.Distance(farmhand.transform.position, targetPos);

        if (distance > 0.05f)
        {
            farmhand.transform.position += direction * farmhand.MoveSpeed * deltaTime;
        }
        else
        {
            walking = false;
            farmhand.transform.position = targetPos;
            farmhand.StartCoroutine(PerformGathering());
        }
    }

    private IEnumerator PerformGathering()
    {
        float waitTime = targetHandState == HANDSTATE.SEED
            ? farmhand.actionTimeGrabSeed
            : farmhand.actionTimeFillWater;

        yield return new WaitForSeconds(waitTime);

        farmhand.ClearHand();
        farmhand.SetHand(targetHandState);

        if (targetHandState == HANDSTATE.SEED)
            farmhand.SetState(new State_Worker_Plant(farmhand));
        else if (targetHandState == HANDSTATE.WATER)
            farmhand.SetState(new State_Worker_Water(farmhand));
    }

    private Transform FindClosestResource()
    {
        if (targetHandState == HANDSTATE.SEED)
        {
            var chests = GameObject.FindGameObjectsWithTag("SeedChest");
            return GetClosest(chests);
        }
        else if (targetHandState == HANDSTATE.WATER)
        {
            var waterTiles = GameObject.FindGameObjectsWithTag("WaterTile");
            return GetClosest(waterTiles);
        }

        return null;
    }

    private Transform GetClosest(GameObject[] objects)
    {
        Transform closest = null;
        float minDist = float.MaxValue;
        Vector3 pos = farmhand.transform.position;

        foreach (var obj in objects)
        {
            float dist = Vector3.Distance(pos, obj.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = obj.transform;
            }
        }

        return closest;
    }

    public override void OnExitState() { }
}
