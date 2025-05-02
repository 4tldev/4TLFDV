//using UnityEngine;

//public class DEVTOOLS_Player_PlaceSeedChest : MonoBehaviour
//{
//    [Header("Placeable Reference")]
//    [SerializeField] private GameObject seedChestPrefab;

//    private Player_ActionHandler actionHandler;
//    private Player_HandState handState;

//    private void Awake()
//    {
//        actionHandler = GetComponent<Player_ActionHandler>();
//        handState = GetComponent<Player_HandState>();
//        Debug.Log($"[DEVTOOLS_Player_PlaceSeedChest] Awake called. HandState assigned: {handState != null}");
//    }

//    private void Update()
//    {
//        if (handState == null) return;

//        if (Input.GetKeyDown(KeyCode.Keypad1))
//        {
//            if (handState.IsEmpty())
//            {
//                handState.SetPlaceableObject(seedChestPrefab);
//                Debug.Log("Holding a Seed Chest to place...");
//            }
//            else
//            {
//                Debug.Log("Already holding something.");
//            }
//        }
//    }

//    private void OnMouseDown()
//    {
//        if (!handState.IsHoldingPlaceable())
//            return;

//        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        Vector2Int gridPos = new(Mathf.RoundToInt(mouseWorldPos.x), Mathf.RoundToInt(mouseWorldPos.y));
//        Tile_Data targetTile = Grid_Manager.Instance.TileDataHandler.GetTileAt(gridPos);

//        if (targetTile != null && targetTile.isUnlocked && !targetTile.hasPlacedObject)
//        {
//            GameObject tileGO = Tile_ViewRenderer.Instance.GetTileViewAt(gridPos);
//            Tile_View tileView = tileGO?.GetComponent<Tile_View>();

//            if (tileView == null)
//            {
//                Debug.LogWarning("Tile_View not found for clicked tile.");
//                return;
//            }

//            GameObject newObject = Instantiate(handState.GetHeldPlaceableObject(), tileView.transform.position, Quaternion.identity);
//            targetTile.hasPlacedObject = true;
//            targetTile.placedObject = newObject;

//            targetTile.farmState = TILE_FARMTILESTATE.OBJECTPLACED;
//            handState.SetHandState(Player_HandState.HANDSTATE.EMPTY);

//            tileView.RefreshView(); // Ensures sprite updates

//            Debug.Log($"Placed seed chest at {gridPos}");
//        }
//    }
//}
