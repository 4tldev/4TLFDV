using UnityEngine;

public interface IWorker
{
    int Energy { get; }
    float MoveSpeed { get; }
    FarmTile CurrentTargetTile { get; set; }

    void SetState(BaseWorkerState newState);
    void ReduceEnergy(int amount);
    void RestoreEnergy();
    void ReturnToHome();
}
