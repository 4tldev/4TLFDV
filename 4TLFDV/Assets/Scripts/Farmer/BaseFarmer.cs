using UnityEngine;

public abstract class BaseFarmer : MonoBehaviour, IFarmer
{
    public enum HANDSTATE
    {
        EMPTY,
        WATER,
        SEED,
        CROP
    }

    public enum RangeLevel { One, Two, Three, Four, Five }

    public RangeLevel PlantRange = RangeLevel.One;
    public RangeLevel WaterRange = RangeLevel.One;
    public RangeLevel HarvestRange = RangeLevel.One;

    public int HandCapacity = 1;


    [Header("Farming State")]
    public HANDSTATE handState = HANDSTATE.EMPTY;

    // --- Farming Actions ---
    public abstract void PlantSeed(FarmTile tile);
    public abstract void WaterSeed(FarmTile tile);
    public abstract void HarvestCrop(FarmTile tile);
    public abstract void PerformTileAction(FarmTile tile, TILEACTIONTYPE actionType);
}
