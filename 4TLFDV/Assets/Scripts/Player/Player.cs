using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Player : BaseFarmer
{
    [Header("Player Settings")]
    public List<FarmHand> farmHands = new List<FarmHand>();

    [Header("Economy")]
    [SerializeField] private int gold = 0;
    [SerializeField] private TextMeshProUGUI goldText;

    #region Tick
    private void Update()
    {
        HandleInput();
        HandleMouseInput();
    }
    #endregion

    #region Input
    private void HandleInput()
    {
        // TODO: Future hotkey handling, tool swapping, etc.
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

            if (hit != null)
            {
                if (hit.TryGetComponent<FarmTile>(out FarmTile tile))
                {
                    InteractWithFarmTile(tile);
                    return;
                }

                if (hit.TryGetComponent<WellTile>(out WellTile well))
                {
                    handState = HANDSTATE.WATER;
                    Debug.Log("Player filled their bucket with water!");
                    return;
                }

                if (hit.TryGetComponent<SeedBoxTile>(out SeedBoxTile seedBox))
                {
                    handState = HANDSTATE.SEED;
                    Debug.Log("Player grabbed seeds!");
                    return;
                }
            }
        }
    }
    #endregion

    #region Farming
    private void InteractWithFarmTile(FarmTile tile)
    {
        switch (tile.tileType)
        {
            case TileData.TileType.Grass:
                TillTile(tile);
                break;

            case TileData.TileType.Farmland:
                switch (tile.tileState)
                {
                    case TileData.TileState.Empty:
                        PlantSeed(tile);
                        break;
                    case TileData.TileState.PlantedUnwatered:
                        WaterSeed(tile);
                        break;
                    case TileData.TileState.ReadyForHarvest:
                        HarvestCrop(tile);
                        break;
                }
                break;
        }
    }

    public void TillTile(FarmTile tile)
    {
        if (tile.tileType == TileData.TileType.Grass)
        {
            tile.TillTile();
            Debug.Log("Player tilled a tile!");
        }
    }

    public override void PerformTileAction(FarmTile tile, TILEACTIONTYPE actionType)
    {
        switch (actionType)
        {
            case TILEACTIONTYPE.PLANT:
                PlantSeed(tile);
                break;
            case TILEACTIONTYPE.WATER:
                WaterSeed(tile);
                break;
            case TILEACTIONTYPE.HARVEST:
                HarvestCrop(tile);
                break;
        }
    }

    public override void PlantSeed(FarmTile tile)
    {
        if (handState == HANDSTATE.SEED)
        {
            tile.PlantSeed();
            Debug.Log("Player planted a seed!");
            handState = HANDSTATE.EMPTY;
        }
        else
        {
            Debug.LogWarning("Player tried to plant without seeds!");
        }
    }

    public override void WaterSeed(FarmTile tile)
    {
        if (handState == HANDSTATE.WATER)
        {
            tile.WaterTile();
            Debug.Log("Player watered a tile!");
            handState = HANDSTATE.EMPTY;
        }
        else
        {
            Debug.LogWarning("Player tried to water without water!");
        }
    }

    public override void HarvestCrop(FarmTile tile)
    {
        if (handState != HANDSTATE.EMPTY)
            EmptyHand();

        tile.HarvestCrop();
        AddGold(10);
        Debug.Log("Player harvested a crop!");
    }

    private void EmptyHand()
    {
        handState = HANDSTATE.EMPTY;
        Debug.LogWarning("Emptied Player hand so they could harvest!");
    }
    #endregion

    #region Economy And Gold Management
    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateGoldUI();
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough gold!");
            return false;
        }
    }

    private void UpdateGoldUI()
    {
        if (goldText != null)
            goldText.text = $"Gold: {gold}";
    }
    public int GetGold()
    {
        return gold;
    }
    #endregion
}
