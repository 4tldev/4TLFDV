using UnityEngine;

public class State_PlantedUnwatered : TileState
{
    public State_PlantedUnwatered(FarmTile tile) : base(tile) { }

    public override void OnEnterState()
    {
        tile.SetSprite(tile.spritePlantedUnwateredTile);
    }

    public override void UpdateState()
    {
        if (Grid_Manager.Instance.Player.CurrentHandState == HANDSTATE.WATER)
        {
            Grid_Manager.Instance.Player.Water();
            tile.SetState(new State_PlantedWatered(tile));
            Debug.Log("Changed State To PlantedWatered!");
        }
        else 
        {
            Debug.Log("Grab some water first!");
        }
    }
}
