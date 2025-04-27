using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_FarmHandRow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Toggle plantToggle;
    [SerializeField] private Toggle waterToggle;
    [SerializeField] private Toggle harvestToggle;
    [SerializeField] private Button renameButton;

    private FarmHand linkedFarmHand;

    public void Initialize(FarmHand farmHand)
    {
        linkedFarmHand = farmHand;
        nameText.text = farmHand.gameObject.name;

        // Set toggles without triggering OnValueChanged events
        plantToggle.SetIsOnWithoutNotify(true);
        waterToggle.SetIsOnWithoutNotify(false);
        harvestToggle.SetIsOnWithoutNotify(false);

        // Hook up listeners AFTER setting
        plantToggle.onValueChanged.AddListener(OnPlantToggleChanged);
        waterToggle.onValueChanged.AddListener(OnWaterToggleChanged);
        harvestToggle.onValueChanged.AddListener(OnHarvestToggleChanged);
    }


    private void OnPlantToggleChanged(bool isOn)
    {
        if (isOn)
        {
            linkedFarmHand.currentTask = FarmHand.FARMHANDTASK.PLANTING;
            waterToggle.isOn = false;
            harvestToggle.isOn = false;
        }
    }

    private void OnWaterToggleChanged(bool isOn)
    {
        if (isOn)
        {
            linkedFarmHand.currentTask = FarmHand.FARMHANDTASK.WATERING;
            plantToggle.isOn = false;
            harvestToggle.isOn = false;
        }
    }

    private void OnHarvestToggleChanged(bool isOn)
    {
        if (isOn)
        {
            linkedFarmHand.currentTask = FarmHand.FARMHANDTASK.HARVESTING;
            plantToggle.isOn = false;
            waterToggle.isOn = false;
        }
    }


    private void OnRenameButtonClicked()
    {
        Debug.Log($"Clicked Rename for {linkedFarmHand.name}");
        // Placeholder for rename later
    }
}
