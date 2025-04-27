using UnityEngine;

public class DebugTools : MonoBehaviour
{
    //[Header("References")]
    [SerializeField] private Player playerReference;
    public Player PlayerReference => playerReference;


    //[Header("Debug Settings")]
    [SerializeField] private int goldPerClick = 100;

    // No serializefield, runtime only
    private int currentGold;
    private BaseFarmer.HANDSTATE currentHandState;

    public int CurrentGold => currentGold;
    public BaseFarmer.HANDSTATE CurrentHandState => currentHandState;

    private void Update()
    {
        if (playerReference != null)
        {
            currentGold = playerReference.GetGold();
            currentHandState = playerReference.handState;
        }
    }

    public void ManualAddGold()
    {
        if (playerReference != null)
        {
            playerReference.AddGold(goldPerClick);
#if UNITY_EDITOR
            Debug.Log($"[DebugTools] Added {goldPerClick} gold!");
#endif
        }
    }
}
