using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private TextMeshProUGUI costText;

    public void Initialize()
    {
        // Nothing special needed here for now
    }

    public void SetButtonAction(UnityEngine.Events.UnityAction action)
    {
        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(action);
    }

    public void UpdateCostText(int newCost)
    {
        costText.text = $"{newCost}g";
    }

    public void SetUpgradeName(string name)
    {
        upgradeNameText.text = name;
    }
}
