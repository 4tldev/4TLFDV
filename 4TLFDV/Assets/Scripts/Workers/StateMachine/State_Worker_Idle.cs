public class State_Worker_Idle : BaseWorkerState
{
    public State_Worker_Idle(Worker worker) : base(worker) { }

    public override void OnEnterState()
    {
        // Do nothing on enter
    }

    public override void Tick(float deltaTime)
    {
        // Stay idle — logic is externally driven by assigned task or handstate
    }

    public override void OnExitState()
    {
        // Do nothing on exit
    }
}
