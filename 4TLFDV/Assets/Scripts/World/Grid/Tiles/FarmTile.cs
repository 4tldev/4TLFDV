using UnityEngine;
using TMPro;

public class FarmTile : MonoBehaviour
{
    private TileState currentState;

    [Header("Placement")]
    public GameObject placedObject;
    private TileState cachedState;

    public bool HasPlacedObject => placedObject != null;


    [Header("Sprites")]
    public Sprite spriteGrassTile;
    public Sprite spriteNotPlantedTile;
    public Sprite spritePlantedUnwateredTile;
    public Sprite spritePlantedWateredTile;
    public Sprite spriteReadyToHarvestTile;
    public Sprite spriteUnlockableTile;
    public Sprite spriteLockedVisibleTile;
    public Sprite spriteWaterTile;

    [Header("UI Elements")]
    public GameObject unlockPriceLabel;
    public TextMeshProUGUI priceText;

    [HideInInspector] public Vector2Int Position;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        currentState.Tick(Time.deltaTime);
    }

    public void SetState(TileState newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState.OnEnterState();
    }


    public void UpdateTileState()
    {
        currentState.UpdateState();
    }

    public void SetSprite(Sprite sprite)
    {
        sr.sprite = sprite;
    }

    public TileState GetCurrentState()
    {
        return currentState;
    }
    public bool CanPlaceObject()
    {
        return !HasPlacedObject && currentState is not State_Unlockable && currentState is not State_LockedVisible && currentState is not State_Water;
    }

    public void PlaceObject(GameObject objectPrefab)
    {
        if (HasPlacedObject) return;

        cachedState = currentState;
        Vector3 worldPos = new Vector3(Position.x * Grid_Manager.Instance.TileSpacing, Position.y * Grid_Manager.Instance.TileSpacing, 0f);
        placedObject = Instantiate(objectPrefab, transform.position, Quaternion.identity, transform);


        placedObject.transform.SetParent(transform);
        SetState(new State_PlacedObject(this));
    }

    public void RemoveObject()
    {
        if (!HasPlacedObject) return;

        Destroy(placedObject);
        placedObject = null;
        SetState(cachedState);
    }

}
