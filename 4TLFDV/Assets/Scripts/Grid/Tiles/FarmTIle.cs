using UnityEngine;

public class FarmTile : MonoBehaviour
{
    [Header("Tile Info")]
    public TileData.TileType tileType = TileData.TileType.Grass;
    public TileData.TileState tileState = TileData.TileState.Empty;

    [Header("Tile Sprites")]
    [SerializeField] private Sprite grassSprite;
    [SerializeField] private Sprite farmlandSprite;
    [SerializeField] private Sprite plantedUnwateredSprite;
    [SerializeField] private Sprite plantedWateredSprite;
    [SerializeField] private Sprite readyToHarvestSprite;

    private SpriteRenderer spriteRenderer;

    // Crop Growth
    private bool growing = false;
    private float growTimer = 0f;
    private float growTime = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError($"FarmTile on {gameObject.name} missing SpriteRenderer!");
        }

        UpdateVisual();
    }

    private void Update()
    {
        if (growing)
        {
            growTimer += Time.deltaTime;
            if (growTimer >= growTime)
            {
                growing = false;
                tileState = TileData.TileState.ReadyForHarvest;
                UpdateVisual();
                Debug.Log($"Tile {gameObject.name} is now Ready to Harvest!");
            }
        }
    }

    #region Public Interactions
    public void TillTile()
    {
        if (tileType == TileData.TileType.Grass)
        {
            tileType = TileData.TileType.Farmland;
            tileState = TileData.TileState.Empty;
            UpdateVisual();
        }
    }

    public void PlantSeed()
    {
        if (tileType == TileData.TileType.Farmland && tileState == TileData.TileState.Empty)
        {
            tileState = TileData.TileState.PlantedUnwatered;
            UpdateVisual();
        }
    }

    public void WaterTile()
    {
        if (tileType == TileData.TileType.Farmland && tileState == TileData.TileState.PlantedUnwatered)
        {
            tileState = TileData.TileState.PlantedWatered;
            UpdateVisual();
            StartGrowing();
        }
    }

    public void HarvestCrop()
    {
        if (tileType == TileData.TileType.Farmland && tileState == TileData.TileState.ReadyForHarvest)
        {
            tileState = TileData.TileState.Empty;
            UpdateVisual();
        }
    }
    #endregion

    #region Growth Logic
    private void StartGrowing()
    {
        growTimer = 0f;
        growTime = Random.Range(10f, 30f);
        growing = true;
        Debug.Log($"Tile {gameObject.name} will grow in {growTime} seconds.");
    }
    #endregion

    #region Visuals
    private void UpdateVisual()
    {
        if (spriteRenderer == null) return;

        switch (tileType)
        {
            case TileData.TileType.Grass:
                spriteRenderer.sprite = grassSprite;
                break;
            case TileData.TileType.Farmland:
                switch (tileState)
                {
                    case TileData.TileState.Empty:
                        spriteRenderer.sprite = farmlandSprite;
                        break;
                    case TileData.TileState.PlantedUnwatered:
                        spriteRenderer.sprite = plantedUnwateredSprite;
                        break;
                    case TileData.TileState.PlantedWatered:
                        spriteRenderer.sprite = plantedWateredSprite;
                        break;
                    case TileData.TileState.ReadyForHarvest:
                        spriteRenderer.sprite = readyToHarvestSprite;
                        break;
                }
                break;
        }
    }
    #endregion
}
