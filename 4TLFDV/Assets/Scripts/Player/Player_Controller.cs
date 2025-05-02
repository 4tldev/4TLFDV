using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Economy")]
    [SerializeField] private int gold = 0;
    public int Gold => gold;

    [Header("Hand State")]
    [SerializeField] private HANDSTATE currentHandState = HANDSTATE.EMPTY;
    public HANDSTATE CurrentHandState => currentHandState;


    public void SetHand(HANDSTATE hand)
    {
        currentHandState = hand;
    }

    public void ClearHand()
    {
        currentHandState = HANDSTATE.EMPTY;
    }

    public bool TrySpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        return false;
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void Plant()
    {
        Debug.Log("🌱 Planting seed...");
        ClearHand(); // You used the seed
    }

    public void Water()
    {
        Debug.Log("💧 Watering plant...");
        ClearHand(); // You used the water
    }

    public void Harvest(int goldReward)
    {
        Debug.Log($"🌾 Harvested crop for {goldReward}g!");
        AddGold(goldReward);
        ClearHand();
    }

}
