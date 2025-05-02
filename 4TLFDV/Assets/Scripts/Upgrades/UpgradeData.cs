using System;

[System.Serializable]
public class UpgradeData
{
    public enum STATUPGRADETYPE
    {
        NONE,
        SEEDCAPACITY,
        WATERCAPACITY,
        HARVESTCAPACITY,
        PLANTRANGE,
        WATERRANGE,
        HARVESTRANGE,
        MOVEMENTSPEED,
        PLANTSPEED,
        WATERSPEED,
        HARVESTSPEED
    }


    public string upgradeName;
    public STATUPGRADETYPE statupgradeType;
    public int baseCost;
    public int costIncreasePerPurchase;
    public int currentLevel;
    public Action onPurchaseAction;

    public UpgradeData(string name, STATUPGRADETYPE type, int baseCost, int costIncrease, Action onPurchaseAction)
    {
        this.upgradeName = name;
        this.statupgradeType = type;
        this.baseCost = baseCost;
        this.costIncreasePerPurchase = costIncrease;
        this.onPurchaseAction = onPurchaseAction;
        this.currentLevel = 0;
    }

    public int GetCurrentCost()
    {
        return baseCost + (costIncreasePerPurchase * currentLevel);
    }

    public void Purchase()
    {
        currentLevel++;
        onPurchaseAction?.Invoke();
    }
}
