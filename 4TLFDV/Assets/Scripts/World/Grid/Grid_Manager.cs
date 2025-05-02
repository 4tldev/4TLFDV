using UnityEngine;
using System.Collections.Generic;

public class Grid_Manager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float tileSpacing = 1.05f;
    [SerializeField] private int initialRadius = 1;

    [Header("Dependencies")]
    [SerializeField] private Player_Controller player;

    public Player_Controller Player => player;
    public float TileSpacing => tileSpacing;

    private Dictionary<Vector2Int, FarmTile> tileGrid = new();

    public static Grid_Manager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        GenerateInitialGrid();
        ExpandFromInitialTiles();
    }

    private void GenerateInitialGrid()
    {
        for (int x = -initialRadius; x <= initialRadius; x++)
        {
            for (int y = -initialRadius; y <= initialRadius; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                int distance = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                Vector3 spawnWorldPos = new Vector3(pos.x * tileSpacing, pos.y * tileSpacing, 0f);

                GameObject tileGO = Instantiate(tilePrefab, spawnWorldPos, Quaternion.identity, transform);
                FarmTile tile = tileGO.GetComponent<FarmTile>();
                tile.Position = pos;
                tileGrid.Add(pos, tile);

                if (distance == 0)
                    tile.SetState(new State_Water(tile));
                else if (distance == 1)
                    tile.SetState(new State_Grass(tile));
                else
                    tile.SetState(new State_LockedVisible(tile));
            }
        }
    }

    private void ExpandFromInitialTiles()
    {
        // STEP 1: Collect all grass tiles
        List<FarmTile> grassTiles = new();
        foreach (var tile in tileGrid.Values)
        {
            if (tile.GetCurrentState() is State_Grass)
            {
                grassTiles.Add(tile);
            }
        }

        // STEP 2: Expand Unlockable around Grass
        List<Vector2Int> unlockablePositions = new();
        foreach (var grassTile in grassTiles)
        {
            foreach (var dir in CardinalDirs())
            {
                Vector2Int neighborPos = grassTile.Position + dir;
                if (!tileGrid.ContainsKey(neighborPos) && !unlockablePositions.Contains(neighborPos))
                {
                    unlockablePositions.Add(neighborPos);
                }
            }
        }

        foreach (var pos in unlockablePositions)
        {
            Vector3 worldPos = new Vector3(pos.x * tileSpacing, pos.y * tileSpacing, 0f);
            GameObject tileGO = Instantiate(tilePrefab, worldPos, Quaternion.identity, transform);
            FarmTile tile = tileGO.GetComponent<FarmTile>();
            tile.Position = pos;

            int distance = Mathf.Max(Mathf.Abs(pos.x), Mathf.Abs(pos.y));
            int cost = 500 * Mathf.RoundToInt(Mathf.Pow(2, distance - 2));
            tile.SetState(new State_Unlockable(tile, cost));
            tileGrid.Add(pos, tile);
        }

        // STEP 3: Expand LockedVisible around Unlockables
        List<Vector2Int> lockedVisiblePositions = new();
        foreach (var tile in tileGrid.Values)
        {
            if (tile.GetCurrentState() is State_Unlockable)
            {
                foreach (var dir in CardinalDirs())
                {
                    Vector2Int neighborPos = tile.Position + dir;
                    if (!tileGrid.ContainsKey(neighborPos) && !lockedVisiblePositions.Contains(neighborPos))
                    {
                        lockedVisiblePositions.Add(neighborPos);
                    }
                }
            }
        }

        foreach (var pos in lockedVisiblePositions)
        {
            Vector3 worldPos = new Vector3(pos.x * tileSpacing, pos.y * tileSpacing, 0f);
            GameObject tileGO = Instantiate(tilePrefab, worldPos, Quaternion.identity, transform);
            FarmTile tile = tileGO.GetComponent<FarmTile>();
            tile.Position = pos;
            tile.SetState(new State_LockedVisible(tile));
            tileGrid.Add(pos, tile);
        }
    }

    public void HandleTileUnlocked(Vector2Int pos)
    {
        TryPromoteAdjacentLockedVisible(pos);
        ExpandLockedVisibleAround(pos);
    }

    public void TryPromoteAdjacentLockedVisible(Vector2Int center)
    {
        foreach (var dir in CardinalDirs())
        {
            Vector2Int neighborPos = center + dir;
            if (tileGrid.TryGetValue(neighborPos, out FarmTile neighborTile))
            {
                if (neighborTile.GetCurrentState() is State_LockedVisible)
                {
                    int distance = Mathf.Max(Mathf.Abs(neighborPos.x), Mathf.Abs(neighborPos.y));
                    int cost = 500 * Mathf.RoundToInt(Mathf.Pow(2, distance - 2));
                    neighborTile.SetState(new State_Unlockable(neighborTile, cost));
                }
            }
        }
    }

    public void ExpandLockedVisibleAround(Vector2Int center)
    {
        foreach (var dir in CardinalDirs())
        {
            Vector2Int newPos = center + dir;
            if (tileGrid.ContainsKey(newPos)) continue;

            // Only spawn if adjacent to an unlockable
            foreach (var checkDir in CardinalDirs())
            {
                Vector2Int checkPos = newPos + checkDir;
                if (tileGrid.TryGetValue(checkPos, out FarmTile checkTile) &&
                    checkTile.GetCurrentState() is State_Unlockable)
                {
                    Vector3 worldPos = new Vector3(newPos.x * tileSpacing, newPos.y * tileSpacing, 0f);
                    GameObject tileGO = Instantiate(tilePrefab, worldPos, Quaternion.identity, transform);
                    FarmTile newTile = tileGO.GetComponent<FarmTile>();
                    newTile.Position = newPos;
                    newTile.SetState(new State_LockedVisible(newTile));
                    tileGrid.Add(newPos, newTile);
                    break;
                }
            }
        }
    }

    public FarmTile GetTileAtPosition(Vector2Int pos)
    {
        tileGrid.TryGetValue(pos, out var tile);
        return tile;
    }

    public Dictionary<Vector2Int, FarmTile> GetGrid() => tileGrid;

    public static IEnumerable<Vector2Int> CardinalDirs()
    {
        yield return Vector2Int.up;
        yield return Vector2Int.down;
        yield return Vector2Int.left;
        yield return Vector2Int.right;
    }
}
