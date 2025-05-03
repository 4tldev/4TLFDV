using UnityEngine;

public class Player_ActionHandler : MonoBehaviour
{
    

    [SerializeField] private GameObject placeableObjectPrefab;
    [SerializeField] private GameObject cabinPrefab;
    [SerializeField] private GameObject seedChestPrefab;

    [Header("Keybinds")]
    [SerializeField] private KeyCode seedChestKeybind = KeyCode.P;
    [SerializeField] private KeyCode cabinKeybind = KeyCode.C;
    [SerializeField] private KeyCode debugGoldKeybind = KeyCode.G;


    private Player_Controller player;

    private void Awake()
    {
        player = GetComponent<Player_Controller>();
    }

    private void Update()
    {
        HandleMouseClick();
        //DEBUG_HandleRosebudInput();
        DEBUG_HandlePlaceableInput();
    }

    private void HandleMouseClick()
    {
        if (UI_IsBlocking()) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider == null) return;

            FarmTile tile = hit.collider.GetComponent<FarmTile>();
            if (tile == null) return;

            var hand = player.CurrentHandState;

            if (hand == HANDSTATE.PLACEABLEOBJECT && tile.CanPlaceObject())
            {
                tile.PlaceObject(placeableObjectPrefab);
                player.ClearHand();
                return;
            }

            tile.UpdateTileState();
        }
    }

    private bool UI_IsBlocking()
    {
        return UI_FH_ManagementController.IsOpen;
    }



    private void DEBUG_HandleRosebudInput()
    {
        if (Input.GetKeyDown(debugGoldKeybind))
        {
            player.AddGold(10_000_000);
            Debug.Log($"[ROSEBUD] Gold: {player.Gold}");
        }
    }

    private void DEBUG_HandlePlaceableInput()
    {
        if (Input.GetKeyDown(seedChestKeybind))
        {
            placeableObjectPrefab = seedChestPrefab;
            player.SetHand(HANDSTATE.PLACEABLEOBJECT);
            Debug.Log("HANDSTATE set to PLACEABLEOBJECT (Seed Chest)");
        }

        if (Input.GetKeyDown(cabinKeybind))
        {
            placeableObjectPrefab = cabinPrefab;
            player.SetHand(HANDSTATE.PLACEABLEOBJECT);
            Debug.Log("HANDSTATE set to PLACEABLEOBJECT (Cabin)");
        }
    }

}
