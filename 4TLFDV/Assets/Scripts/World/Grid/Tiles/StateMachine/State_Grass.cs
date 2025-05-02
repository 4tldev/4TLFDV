using UnityEngine;

public class State_Grass : TileState
{
    public State_Grass(FarmTile tile) : base(tile) { }

    public override void OnEnterState()
    {
        tile.SetSprite(tile.spriteGrassTile);
    }

    public override void UpdateState()
    {
        tile.SetState(new State_NotPlanted(tile));
    }
}
