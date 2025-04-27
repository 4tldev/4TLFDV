public static class TileData
{
    public enum TileType
    {
        Grass,
        Farmland
    }

    public enum TileState
    {
        Empty,
        PlantedUnwatered,
        PlantedWatered,
        ReadyForHarvest
    }
}
