using UnityEngine;

public class Player_ActionHandler : MonoBehaviour
{
    [SerializeField] private KeyCode debugGoldKey = KeyCode.G;

    [SerializeField] private GameObject placeableObjectPrefab;
    [SerializeField] private KeyCode placeObjectKey = KeyCode.P;

    private Player_Controller player;

    private void Awake()
    {
        player = GetComponent<Player_Controller>();
    }

    private void Update()
    {
        HandleMouseClick();
        DEBUG_HandleRosebudInput();
        DEBUG_ASSIGNHANDTOPLACOBJ();
    }

    private void HandleMouseClick()
    {
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



    private void DEBUG_HandleRosebudInput()
    {
        if (Input.GetKeyDown(debugGoldKey))
        {
            player.AddGold(10_000_000);
            Debug.Log($"[ROSEBUD] Gold: {player.Gold}");
        }
    }

    private void DEBUG_ASSIGNHANDTOPLACOBJ() 
    {
        if (Input.GetKeyDown(placeObjectKey)) // or any test key
        {
            player.SetHand(HANDSTATE.PLACEABLEOBJECT);
            Debug.Log("HANDSTATE set to PLACEABLEOBJECT");
        }

    }
}
