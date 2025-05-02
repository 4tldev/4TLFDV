using UnityEngine;

public class State_PlantedWatered : TileState
{
    private float growTime;
    private float timer;

    public State_PlantedWatered(FarmTile tile) : base(tile)
    {
        growTime = GetGrowthTime();
        timer = growTime;
    }

    public override void OnEnterState()
    {
        tile.SetSprite(tile.spritePlantedWateredTile);
    }

    public override void Tick(float deltaTime)
    {
        timer -= deltaTime;
        if (timer <= 0f)
        {
            tile.SetState(new State_ReadyToHarvest(tile));
        }
    }

    public override void UpdateState()
    {
        // No interaction needed here
    }

    private float GetGrowthTime()
    {
        return Random.Range(10f, 30f); // Later: adjust based on upgrades, crop type, soil, etc.
    }
}
