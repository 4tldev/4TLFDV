public abstract class TileState
{
    protected FarmTile tile;

    public TileState(FarmTile tile)
    {
        this.tile = tile;
    }

    public abstract void OnEnterState();       // Called when state becomes active
    public virtual void OnExitState() { }
    public abstract void UpdateState();   // Called when clicked/interacted
    public virtual void Tick(float deltaTime) { } // For timers if needed


}
