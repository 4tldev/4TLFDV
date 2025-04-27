using System.Collections.Generic;
using UnityEngine;

public class FarmHand : BaseFarmer
{
    public enum FARMHANDTASK { PLANTING, WATERING, HARVESTING }
    public enum FARMHANDSTATE { IDLE, ACQUIRINGTASK, PERFORMINGTASK }

    [Header("References")]
    private Player playerReference;

    [Header("Farmhand Settings")]
    public FARMHANDTASK currentTask;
    public FARMHANDSTATE currentState = FARMHANDSTATE.IDLE;

    [Header("Work Timers")]
    [SerializeField] private float bucketFillTimer = 5f;
    [SerializeField] private float grabSeedTimer = 5f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Targeting")]
    public Transform resourceTarget;
    public FarmTile tileTarget;

    #region Unity Tick
    private void Update()
    {
        HandleState();
    }
    #endregion

    #region State Machine
    private void HandleState()
    {
        switch (currentState)
        {
            case FARMHANDSTATE.IDLE:
                FindWork();
                break;
            case FARMHANDSTATE.ACQUIRINGTASK:
                AcquireResources();
                break;
            case FARMHANDSTATE.PERFORMINGTASK:
                PerformWork();
                break;
        }
    }
    #endregion

    #region Work Logic
    private void FindWork()
    {
        switch (currentTask)
        {
            case FARMHANDTASK.PLANTING:
                if (handState != HANDSTATE.SEED)
                    MoveToSeedBox();
                else
                    FindTileToPlant();
                break;

            case FARMHANDTASK.WATERING:
                if (handState != HANDSTATE.WATER)
                    MoveToWell();
                else
                    FindTileToWater();
                break;

            case FARMHANDTASK.HARVESTING:
                FindTileToHarvest();
                break;
        }
    }

    private void AcquireResources()
    {
        if (resourceTarget == null)
        {
            currentState = FARMHANDSTATE.IDLE;
            return;
        }

        MoveTowards(resourceTarget.position);

        if (Vector2.Distance(transform.position, resourceTarget.position) < 0.2f)
        {
            if (currentTask == FARMHANDTASK.PLANTING)
                GatherSeeds();
            else if (currentTask == FARMHANDTASK.WATERING)
                FillBucket();

            currentState = FARMHANDSTATE.IDLE;
        }
    }

    private void PerformWork()
    {
        if (tileTarget == null)
        {
            currentState = FARMHANDSTATE.IDLE;
            return;
        }

        MoveTowards(tileTarget.transform.position);

        if (Vector2.Distance(transform.position, tileTarget.transform.position) < 0.2f)
        {
            PerformTileAction(tileTarget, ConvertTaskToAction(currentTask));
            tileTarget = null;
            currentState = FARMHANDSTATE.IDLE;
        }
    }
    #endregion

    #region Farming Actions
    public override void PlantSeed(FarmTile tile)
    {
        Debug.Log($"{gameObject.name} planted a seed!");
        tile.PlantSeed();
        handState = HANDSTATE.EMPTY;
    }

    public override void WaterSeed(FarmTile tile)
    {
        Debug.Log($"{gameObject.name} watered a tile!");
        tile.WaterTile();
        handState = HANDSTATE.EMPTY;
    }

    public override void HarvestCrop(FarmTile tile)
    {
        Debug.Log($"{gameObject.name} harvested a crop!");
        tile.HarvestCrop();
        handState = HANDSTATE.EMPTY;

        // 🆕 Give the player some gold
        if (playerReference != null)
        {
            playerReference.AddGold(10); // or whatever value you want per harvest
        }
    }

    public override void PerformTileAction(FarmTile tile, TILEACTIONTYPE actionType)
    {
        switch (actionType)
        {
            case TILEACTIONTYPE.PLANT: PlantSeed(tile); break;
            case TILEACTIONTYPE.WATER: WaterSeed(tile); break;
            case TILEACTIONTYPE.HARVEST: HarvestCrop(tile); break;
        }
    }
    #endregion

    #region Movement Helpers
    private void MoveTowards(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
    #endregion

    #region Resource Finding
    private void MoveToSeedBox()
    {
        resourceTarget = WorldManager.Instance.GetClosestSeedBox(transform.position);
        currentState = FARMHANDSTATE.ACQUIRINGTASK;
    }

    private void MoveToWell()
    {
        resourceTarget = WorldManager.Instance.GetClosestWell(transform.position);
        currentState = FARMHANDSTATE.ACQUIRINGTASK;
    }
    #endregion

    #region Tile Finding
    private void FindTileToPlant()
    {
        List<FarmTile> candidates = new List<FarmTile>();

        foreach (FarmTile tile in WorldManager.Instance.GetTilesByState(TileData.TileState.Empty))
        {
            if (tile.tileType == TileData.TileType.Farmland) // <- Only farmland allowed
            {
                candidates.Add(tile);
            }
        }

        if (candidates.Count > 0)
        {
            tileTarget = candidates[Random.Range(0, candidates.Count)];
            currentState = FARMHANDSTATE.PERFORMINGTASK;
        }
        else
        {
            Debug.Log($"{gameObject.name}: No plantable farmland tiles found!");
            currentState = FARMHANDSTATE.IDLE;
        }
    }


    private void FindTileToWater()
    {
        List<FarmTile> candidates = WorldManager.Instance.GetTilesByState(TileData.TileState.PlantedUnwatered);
        if (candidates.Count > 0)
        {
            tileTarget = candidates[Random.Range(0, candidates.Count)];
            currentState = FARMHANDSTATE.PERFORMINGTASK;
        }
        else
        {
            Debug.Log($"{gameObject.name}: No waterable tiles found!");
            currentState = FARMHANDSTATE.IDLE;
        }
    }

    private void FindTileToHarvest()
    {
        List<FarmTile> candidates = WorldManager.Instance.GetTilesByState(TileData.TileState.ReadyForHarvest);
        if (candidates.Count > 0)
        {
            tileTarget = candidates[Random.Range(0, candidates.Count)];
            currentState = FARMHANDSTATE.PERFORMINGTASK;
        }
        else
        {
            Debug.Log($"{gameObject.name}: No harvestable tiles found!");
            currentState = FARMHANDSTATE.IDLE;
        }
    }
    #endregion

    #region Hand Actions
    private void FillBucket()
    {
        handState = HANDSTATE.WATER;
        Debug.Log($"{gameObject.name} filled their bucket with water!");
    }

    private void GatherSeeds()
    {
        handState = HANDSTATE.SEED;
        Debug.Log($"{gameObject.name} grabbed seeds!");
    }
    #endregion

    #region Helper
    private TILEACTIONTYPE ConvertTaskToAction(FARMHANDTASK task)
    {
        return task switch
        {
            FARMHANDTASK.PLANTING => TILEACTIONTYPE.PLANT,
            FARMHANDTASK.WATERING => TILEACTIONTYPE.WATER,
            FARMHANDTASK.HARVESTING => TILEACTIONTYPE.HARVEST,
            _ => TILEACTIONTYPE.PLANT
        };
    }
    #endregion

    #region References
    public void SetPlayer(Player player)
    {
        playerReference = player;
    }

    #endregion
}
