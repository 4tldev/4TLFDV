using UnityEngine;

public class State_Worker_ReturnToCabin : BaseWorkerState
{
    private readonly Vector3 targetPosition;

    public State_Worker_ReturnToCabin(Worker worker, Vector3 targetPosition) : base(worker)
    {
        this.targetPosition = targetPosition;
    }

    public override void OnEnterState()
    {
        // Optional setup
    }

    public override void Tick(float deltaTime)
    {
        var transform = ((Worker)worker).transform;

        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > 0.05f)
        {
            transform.position += direction * worker.MoveSpeed * deltaTime;
        }
        else
        {
            transform.position = targetPosition;
            worker.SetState(new State_Worker_Resting((Worker)worker));

        }
    }

    public override void OnExitState()
    {
        // Optional cleanup
    }
}
