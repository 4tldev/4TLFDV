using UnityEngine;

public class State_Water : TileState
{
    public State_Water(FarmTile tile) : base(tile) { }

    public override void OnEnterState()
    {
        tile.SetSprite(tile.spriteWaterTile);
        tile.gameObject.tag = "WaterTile";
    }

    public override void UpdateState()
    {
        Grid_Manager.Instance.Player.SetHand(HANDSTATE.WATER);
        Debug.Log("Water tile clicked → HANDSTATE set to WATER");
    }
}

