using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_FH_ManagementRow : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI nameField;
    [SerializeField] private Toggle togglePlant;
    [SerializeField] private Toggle toggleWater;
    [SerializeField] private Toggle toggleHarvest;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button infoButton;

    private Farmhand linkedFarmhand;

    public void Initialize(Farmhand farmhand)
    {
        linkedFarmhand = farmhand;
        nameField.text = "Farmhand";

        // Set all off by default (including in UI)
        togglePlant.isOn = false;
        toggleWater.isOn = false;
        toggleHarvest.isOn = false;
        linkedFarmhand.CanPlant = false;
        linkedFarmhand.CanWater = false;
        linkedFarmhand.CanHarvest = false;
        linkedFarmhand.SetState(new State_Worker_Idle(linkedFarmhand));

        togglePlant.onValueChanged.AddListener(OnPlantToggle);
        toggleWater.onValueChanged.AddListener(OnWaterToggle);
        toggleHarvest.onValueChanged.AddListener(OnHarvestToggle);

        upgradeButton.onClick.AddListener(() => Debug.Log("Upgrade not implemented yet"));
        infoButton.onClick.AddListener(() => Debug.Log("Info not implemented yet"));
    }

    private void OnPlantToggle(bool isOn)
    {
        if (isOn)
        {
            toggleWater.isOn = false;
            toggleHarvest.isOn = false;
            linkedFarmhand.CanPlant = true;
            linkedFarmhand.CanWater = false;
            linkedFarmhand.CanHarvest = false;
            linkedFarmhand.SetState(new State_Worker_Plant(linkedFarmhand));
        }
        else if (!toggleWater.isOn && !toggleHarvest.isOn)
        {
            linkedFarmhand.SetState(new State_Worker_Idle(linkedFarmhand));
        }
    }

    private void OnWaterToggle(bool isOn)
    {
        if (isOn)
        {
            togglePlant.isOn = false;
            toggleHarvest.isOn = false;
            linkedFarmhand.CanPlant = false;
            linkedFarmhand.CanWater = true;
            linkedFarmhand.CanHarvest = false;
            linkedFarmhand.SetState(new State_Worker_Water(linkedFarmhand));
        }
        else if (!togglePlant.isOn && !toggleHarvest.isOn)
        {
            linkedFarmhand.SetState(new State_Worker_Idle(linkedFarmhand));
        }
    }

    private void OnHarvestToggle(bool isOn)
    {
        if (isOn)
        {
            togglePlant.isOn = false;
            toggleWater.isOn = false;
            linkedFarmhand.CanPlant = false;
            linkedFarmhand.CanWater = false;
            linkedFarmhand.CanHarvest = true;
            linkedFarmhand.SetState(new State_Worker_Harvest(linkedFarmhand));
        }
        else if (!togglePlant.isOn && !toggleWater.isOn)
        {
            linkedFarmhand.SetState(new State_Worker_Idle(linkedFarmhand));
        }
    }
}
