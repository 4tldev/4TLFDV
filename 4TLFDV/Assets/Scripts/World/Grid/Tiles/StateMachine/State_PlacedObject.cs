using UnityEngine;

public class State_PlacedObject : TileState
{
    public State_PlacedObject(FarmTile tile) : base(tile) { }

    public override void OnEnterState()
    {
        // Optionally set a blocked tile sprite
        tile.SetSprite(tile.spriteGrassTile); // or a "disabled" variant
    }

    public override void UpdateState()
    {
        // No farming interaction allowed
        Debug.Log("Tile is occupied with a placed object.");
    }
}
