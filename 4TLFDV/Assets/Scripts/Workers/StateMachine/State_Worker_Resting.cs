using System.Collections;
using UnityEngine;

public class State_Worker_Resting : BaseWorkerState
{
    private float restTime;

    public State_Worker_Resting(Worker worker) : base(worker)
    {
        restTime = Random.Range(20f, 30f);
    }

    public override void OnEnterState()
    {
        ((Worker)worker).StartCoroutine(RestoreRoutine());

    }

    private IEnumerator RestoreRoutine()
    {
        yield return new WaitForSeconds(restTime);
        worker.RestoreEnergy();
    }

    public override void Tick(float deltaTime) { }

    public override void OnExitState() { }
}
