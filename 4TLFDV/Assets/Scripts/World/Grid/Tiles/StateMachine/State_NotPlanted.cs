using UnityEngine;

public class State_NotPlanted : TileState
{
    public State_NotPlanted(FarmTile tile) : base(tile) { }

    public override void OnEnterState()
    {
        tile.SetSprite(tile.spriteNotPlantedTile);
    }

    public override void UpdateState()
    {
        if (Grid_Manager.Instance.Player.CurrentHandState == HANDSTATE.SEED)
        {
            Grid_Manager.Instance.Player.Plant();
            tile.SetState(new State_PlantedUnwatered(tile));
            Debug.Log("Changed State To PlantedUnwatered!");
        }
        else 
        {
            Debug.Log("Go grab a seed first!");
        }
    }
}
