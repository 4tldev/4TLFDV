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

    [Header("Farming State")]
    public HANDSTATE handState = HANDSTATE.EMPTY;

    // Future tools (still TODO)
    //[Header("Tools")]
    //public WateringCan wateringCan;
    //public Hoe hoe;
    //public Scythe scythe;

    // --- Farming Actions ---
    public abstract void PlantSeed(FarmTile tile);
    public abstract void WaterSeed(FarmTile tile);
    public abstract void HarvestCrop(FarmTile tile);
    public abstract void PerformTileAction(FarmTile tile, TILEACTIONTYPE actionType);
}
