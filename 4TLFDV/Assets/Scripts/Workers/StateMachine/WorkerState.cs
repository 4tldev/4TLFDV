public abstract class BaseWorkerState
{
    protected readonly IWorker worker;

    protected BaseWorkerState(IWorker worker)
    {
        this.worker = worker;
    }

    public abstract void OnEnterState();
    public abstract void Tick(float deltaTime);
    public abstract void OnExitState();
}
