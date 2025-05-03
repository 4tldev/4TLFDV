using UnityEngine;

public class State_ReadyToHarvest : TileState
{
    public State_ReadyToHarvest(FarmTile tile) : base(tile) { }

    public override void OnEnterState()
    {
        tile.SetSprite(tile.spriteReadyToHarvestTile);
    }

    public override void UpdateState()
    {
        //TODO make sure we change the logic so that we aren't hard coding this gold value
        //Intend to implement different seed logic eventually.
        int value = 20;
        Grid_Manager.Instance.Player.Harvest(value);

        
        tile.SetState(new State_NotPlanted(tile));
        Debug.Log("Changing State to NotPlanted");
    }
}
