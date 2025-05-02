//using UnityEngine;

//public class DEVTOOLS_FH_HireButton : MonoBehaviour
//{
//    [Header("References")]
//    [SerializeField] private FarmHandManager farmHandManager;
//    [SerializeField] private Player_GoldHandler playerGoldHandler;
//    [SerializeField] private GameObject farmHandPrefab;
//    [SerializeField] private Transform spawnPoint;

//    [Header("Dev Settings")]
//    [SerializeField] private bool chargeGold = false;
//    [SerializeField] private int hireCost = 100;

//    [Button("Hire New FarmHand")]
//    public void HireFarmHand()
//    {
//        if (farmHandManager == null || farmHandPrefab == null)
//        {
//            Debug.LogWarning("[DEVTOOLS_FH_HireButton] Missing FarmHandManager or FarmHandPrefab reference!");
//            return;
//        }

//        if (chargeGold)
//        {
//            if (playerGoldHandler == null)
//            {
//                Debug.LogWarning("[DEVTOOLS_FH_HireButton] Missing Player_GoldHandler reference!");
//                return;
//            }

//            if (!playerGoldHandler.SpendGold(hireCost))
//            {
//                Debug.Log("[DEVTOOLS_FH_HireButton] Not enough gold to hire FarmHand.");
//                return;
//            }
//        }

//        // Spawn new FarmHand
//        Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : Vector3.zero;
//        GameObject newFarmHandObj = Instantiate(farmHandPrefab, spawnPosition, Quaternion.identity);

//        // Register with FarmHandManager
//        FH_Controller newFarmHand = newFarmHandObj.GetComponent<FH_Controller>();
//        if (newFarmHand != null)
//        {
//            farmHandManager.RegisterFarmHand(newFarmHand);
//            Debug.Log("[DEVTOOLS_FH_HireButton] Successfully hired new FarmHand!");
//        }
//        else
//        {
//            Debug.LogWarning("[DEVTOOLS_FH_HireButton] Spawned object missing FH_Controller!");
//        }
//    }
//}
