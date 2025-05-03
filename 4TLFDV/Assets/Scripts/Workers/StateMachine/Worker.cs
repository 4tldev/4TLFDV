using UnityEngine;

public class Worker : MonoBehaviour, IWorker
{
    public int Energy { get; private set; } = 100;

    [SerializeField] private float moveSpeed = 2f;
    public float MoveSpeed => moveSpeed;

    public BaseWorkerState currentState;

    // Cabin logic (optional fallback for future logic)
    public PlacObj_Cabin assignedCabin;

    // Target tile (used by task states like Plant, Water, Harvest)
    public FarmTile CurrentTargetTile { get; set; }

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    public void SetState(BaseWorkerState newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState.OnEnterState();
    }

    public void ReturnToHome()
    {
        if (assignedCabin != null)
        {
            Vector3 restSpot = assignedCabin.GetRestingSpot();
            CurrentTargetTile = null;
            SetState(new State_Worker_ReturnToCabin(this, restSpot));
        }
    }

    public virtual void ReduceEnergy(int amount)
    {
        Energy -= amount;
        if (Energy <= 0)
        {
            Energy = 0;
            ReturnToHome();
        }
    }

    public virtual void RestoreEnergy()
    {
        Energy = 100;
    }
}
