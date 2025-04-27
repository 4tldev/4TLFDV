using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("UI Prefab References")]
    [SerializeField] private GameObject upgradePanelPrefab;
    [SerializeField] private Transform upgradePanelParent;

    [Header("Farmhands")]
    [SerializeField] private GameObject farmhandPrefab;
    [SerializeField] private Transform farmhandSpawnPoint;
    [SerializeField] private float farmhandSpawnRadius = 5f;

    [Header("Economy")]
    [SerializeField] private Player playerReference;
    [SerializeField] private int farmhandBaseCost = 250;
    [SerializeField] private int farmhandCostIncrement = 250;
    private int currentFarmhandCost;

    private void Start()
    {
        currentFarmhandCost = farmhandBaseCost;
        SpawnUpgradePanels();
    }

    private void SpawnUpgradePanels()
    {
        GameObject panelGO = Instantiate(upgradePanelPrefab, upgradePanelParent);
        UpgradePanelUI panelUI = panelGO.GetComponent<UpgradePanelUI>();

        if (panelUI != null)
        {
            panelUI.Initialize();
            panelUI.SetButtonAction(SpawnFarmhand);
            panelUI.UpdateCostText(currentFarmhandCost);
            panelUI.SetUpgradeName("Hire Farmhand");
        }
        else
        {
            Debug.LogWarning("[UpgradeManager] Missing UpgradePanelUI component!");
        }
    }

    private void SpawnFarmhand()
    {
        if (playerReference == null)
        {
            Debug.LogWarning("[UpgradeManager] No Player Reference assigned!");
            return;
        }

        if (playerReference.SpendGold(currentFarmhandCost))
        {
            if (farmhandPrefab != null && farmhandSpawnPoint != null)
            {
                Vector2 randomOffset = Random.insideUnitCircle * 1.5f;
                Vector3 spawnPosition = farmhandSpawnPoint.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

                GameObject farmhandGO = Instantiate(farmhandPrefab, spawnPosition, Quaternion.identity);

                FarmHand farmhand = farmhandGO.GetComponent<FarmHand>();
                if (farmhand != null)
                {
                    farmhand.SetPlayer(playerReference);
                }

                Debug.Log($"[UpgradeManager] Spawned a new farmhand at {spawnPosition}");

                // 🆕 Increment cost for next farmhand
                currentFarmhandCost += farmhandCostIncrement;

                // 🆕 Update the button text
                UpdateAllUpgradePanels();
            }
            else
            {
                Debug.LogWarning("[UpgradeManager] Missing farmhandPrefab or farmhandSpawnPoint reference!");
            }
        }
        else
        {
            Debug.LogWarning("[UpgradeManager] Not enough gold to hire a Farmhand!");
        }
    }



    private void UpdateAllUpgradePanels()
    {
        foreach (UpgradePanelUI panel in upgradePanelParent.GetComponentsInChildren<UpgradePanelUI>())
        {
            panel.UpdateCostText(currentFarmhandCost);
        }
    }

}
