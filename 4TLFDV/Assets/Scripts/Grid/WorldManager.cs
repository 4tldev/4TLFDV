using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("World Settings")]
    [SerializeField] private int startingGridWidth = 5;
    [SerializeField] private int startingGridHeight = 5;
    [SerializeField] private float tileSpacing = 1.1f;

    [Header("Prefabs")]
    [SerializeField] private GameObject farmTilePrefab;
    [SerializeField] private GameObject wellPrefab;
    [SerializeField] private GameObject seedBoxPrefab;

    [Header("World Tracking")]
    private List<FarmTile> allTiles = new List<FarmTile>();

    public static WorldManager Instance { get; private set; }

    // --- Public Accessors ---
    public int StartingGridWidth => startingGridWidth;
    public int StartingGridHeight => startingGridHeight;
    public float TileSpacing => tileSpacing;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SpawnStartingGrid();
    }

    #region Grid Spawning
    private void SpawnStartingGrid()
    {
        Vector2 startPos = transform.position;

        for (int x = 0; x < startingGridWidth; x++)
        {
            for (int y = 0; y < startingGridHeight; y++)
            {
                Vector2 spawnPos = startPos + new Vector2(x * tileSpacing, y * tileSpacing);
                GameObject tileGO = Instantiate(farmTilePrefab, spawnPos, Quaternion.identity, this.transform);

                FarmTile tile = tileGO.GetComponent<FarmTile>();
                if (tile != null)
                {
                    allTiles.Add(tile);
                }
            }
        }

        // Special Spawns (Seed Box and Well)
        Vector2 centerPos = startPos + new Vector2(startingGridWidth / 2f * tileSpacing, startingGridHeight * tileSpacing);
        Vector2 seedBoxPos = centerPos + new Vector2(-tileSpacing, tileSpacing);
        Vector2 wellPos = centerPos + new Vector2(tileSpacing, tileSpacing);

        if (seedBoxPrefab != null)
            Instantiate(seedBoxPrefab, seedBoxPos, Quaternion.identity, this.transform);

        if (wellPrefab != null)
            Instantiate(wellPrefab, wellPos, Quaternion.identity, this.transform);
    }
    #endregion

    #region Tile Queries
    public List<FarmTile> GetTilesByState(TileData.TileState state)
    {
        List<FarmTile> matchingTiles = new List<FarmTile>();

        foreach (FarmTile tile in allTiles)
        {
            if (tile.tileState == state)
            {
                matchingTiles.Add(tile);
            }
        }

        return matchingTiles;
    }
    #endregion

    #region Special Object Finders

    public Transform GetClosestSeedBox(Vector3 fromPosition)
    {
        GameObject[] seedBoxes = GameObject.FindGameObjectsWithTag("SeedBox");

        if (seedBoxes.Length == 0) return null;

        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject seedBox in seedBoxes)
        {
            float distance = Vector3.Distance(fromPosition, seedBox.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = seedBox;
            }
        }

        return closest?.transform;
    }

    public Transform GetClosestWell(Vector3 fromPosition)
    {
        GameObject[] wells = GameObject.FindGameObjectsWithTag("Well");

        if (wells.Length == 0) return null;

        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject well in wells)
        {
            float distance = Vector3.Distance(fromPosition, well.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = well;
            }
        }

        return closest?.transform;
    }

    #endregion
}
