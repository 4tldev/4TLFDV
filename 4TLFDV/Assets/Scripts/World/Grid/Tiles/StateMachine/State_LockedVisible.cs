using UnityEngine;

public class State_LockedVisible : TileState
{
    public State_LockedVisible(FarmTile tile) : base(tile) { }

    public override void OnEnterState()
    {
        tile.SetSprite(tile.spriteLockedVisibleTile);
    }

    public override void UpdateState()
    {
        // No interaction on locked visible tiles
    }

    public override void OnExitState()
    {
        Grid_Manager.Instance.ExpandLockedVisibleAround(tile.Position);
    }
}
