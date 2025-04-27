using System;

[System.Serializable]
public class UpgradeData
{
    public enum UpgradeType
    {
        None,
        HireFarmhand,
        ExpandFarm,
        // Add more upgrade types later
    }


    public string upgradeName;
    public UpgradeType upgradeType;
    public int baseCost;
    public int costIncreasePerPurchase;
    public int currentLevel;
    public Action onPurchaseAction;

    public UpgradeData(string name, int baseCost, int costIncrease, Action onPurchaseAction)
    {
        this.upgradeName = name;
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
