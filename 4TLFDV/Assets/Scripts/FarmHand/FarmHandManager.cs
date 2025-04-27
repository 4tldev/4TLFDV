using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmHandManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private GameObject farmHandPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform farmHandRowParent;
    [SerializeField] private GameObject farmHandRowPrefab;

    [Header("Button")]
    [SerializeField] private Button hireFarmHandButton;
    [SerializeField] private TextMeshProUGUI hireButtonText;


    [Header("Settings")]
    [SerializeField] private int startingFarmHandCost = 100;
    [SerializeField] private int costIncreasePerHire = 50;

    private int currentFarmHandCost;
    private int farmHandCounter = 0;

    private void Start()
    {
        currentFarmHandCost = startingFarmHandCost;

        if (hireFarmHandButton != null)
        {
            hireFarmHandButton.onClick.AddListener(HireFarmHand);
        }
        UpdateHireButtonText();
    }

    public void HireFarmHand()
    {
        if (player.GetGold() >= currentFarmHandCost)
        {
            player.AddGold(-currentFarmHandCost);

            GameObject newFarmHand = Instantiate(farmHandPrefab, spawnPoint.position, Quaternion.identity);
            FarmHand farmHandComponent = newFarmHand.GetComponent<FarmHand>();

            if (farmHandComponent != null)
            {
                farmHandComponent.SetPlayer(player);
                farmHandCounter++;
                newFarmHand.name = $"FH_{farmHandCounter}";

                player.farmHands.Add(farmHandComponent);

                CreateFarmHandUIRow(farmHandComponent);

                currentFarmHandCost += costIncreasePerHire;
                UpdateHireButtonText();

                farmHandComponent.currentTask = FarmHand.FARMHANDTASK.PLANTING;

            }
            else
            {
                Debug.LogError("FarmHand prefab is missing the FarmHand component!");
            }
        }
        else
        {
            Debug.LogWarning("Not enough gold to hire a FarmHand!");
        }
    }

    private void UpdateHireButtonText()
    {
        if (hireButtonText != null)
        {
            hireButtonText.text = $"{currentFarmHandCost}g";
        }
    }


    private void CreateFarmHandUIRow(FarmHand farmHand)
    {
        GameObject newRow = Instantiate(farmHandRowPrefab, farmHandRowParent);
        UI_FarmHandRow uiRow = newRow.GetComponent<UI_FarmHandRow>();

        if (uiRow != null)
        {
            uiRow.Initialize(farmHand);
            UIHelpers.ForceRefreshLayout(farmHandRowParent.gameObject); // <-- add it here!
        }
        else
        {
            Debug.LogError("FarmHandRow prefab missing UI_FarmHandRow component!");
        }
    }
}
