using UnityEngine;
using System.Collections.Generic;

public class Grid_Initialize : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float tileSpacing = 1.05f;
    [SerializeField] private int spawnRadius = 4;

    public GameObject TilePrefab => tilePrefab;
    public float TileSpacing => tileSpacing;


    private Dictionary<Vector2Int, FarmTile> tileGrid = new();


    public Dictionary<Vector2Int, FarmTile> GenerateInitialGrid(Transform parent)
    {
        for (int x = -spawnRadius; x <= spawnRadius; x++)
        {
            for (int y = -spawnRadius; y <= spawnRadius; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                Vector3 spawnPos = new Vector3(pos.x * tileSpacing, pos.y * tileSpacing, 0f);

                GameObject tileGO = Instantiate(tilePrefab, spawnPos, Quaternion.identity, parent);
                FarmTile tile = tileGO.GetComponent<FarmTile>();
                tile.Position = pos;

                tileGrid.Add(pos, tile);
                AssignInitialState(tile, pos);
            }
        }

        return tileGrid;
    }

    private void AssignInitialState(FarmTile tile, Vector2Int pos)
    {
        int distance = Mathf.Max(Mathf.Abs(pos.x), Mathf.Abs(pos.y));

        if (distance == 0)
            tile.SetState(new State_Water(tile));
        else if (distance == 1)
            tile.SetState(new State_Grass(tile));
        else
            tile.SetState(new State_LockedVisible(tile));
    }
}
