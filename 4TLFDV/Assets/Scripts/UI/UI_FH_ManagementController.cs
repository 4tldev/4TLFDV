using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UI_FH_ManagementController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform rowParent;
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private Button hireButton;
    [SerializeField] private TextMeshProUGUI costText;

    [Header("Worker Settings")]
    [SerializeField] private GameObject farmhandPrefab;
    [SerializeField] private int baseCost = 200;
    [SerializeField] private int costIncrement = 100;

    private int hireCount = 0;
    private List<UI_FH_ManagementRow> rows = new List<UI_FH_ManagementRow>();

    public static bool IsOpen { get; private set; }


    private void Start()
    {
        UpdateCostText();
        hireButton.onClick.AddListener(HireFarmhand);
    }

    private void HireFarmhand()
    {
        int cost = baseCost + (costIncrement * hireCount);
        if (Grid_Manager.Instance.Player.TrySpendGold(cost))
        {
            hireCount++;
            UpdateCostText();

            GameObject farmhandGO = Instantiate(farmhandPrefab);
            Farmhand farmhand = farmhandGO.GetComponent<Farmhand>();

            PlacObj_Cabin cabin = FindClosestCabinToCamera();
            if (cabin != null)
            {
                farmhand.transform.position = cabin.GetRestingSpot();
            }

            GameObject rowGO = Instantiate(rowPrefab, rowParent);
            UI_FH_ManagementRow row = rowGO.GetComponent<UI_FH_ManagementRow>();
            row.Initialize(farmhand);
            rows.Add(row);
        }
    }


    private void UpdateCostText()
    {
        int cost = baseCost + (costIncrement * hireCount);
        if (costText != null)
        {
            costText.text = $"${cost:N0}";
        }
    }

    private PlacObj_Cabin FindAvailableCabin()
    {
        PlacObj_Cabin[] cabins = Object.FindObjectsByType<PlacObj_Cabin>(FindObjectsSortMode.None);

        foreach (var cabin in cabins)
        {
            if (cabin.HasRoom())
                return cabin;
        }
        return null;
    }

    private PlacObj_Cabin FindAnyCabin()
    {
        PlacObj_Cabin[] cabins = Object.FindObjectsByType<PlacObj_Cabin>(FindObjectsSortMode.None);
        return cabins.Length > 0 ? cabins[0] : null;
    }

    private PlacObj_Cabin FindClosestCabinToCamera()
    {
        PlacObj_Cabin[] cabins = Object.FindObjectsByType<PlacObj_Cabin>(FindObjectsSortMode.None);
        if (cabins.Length == 0) return null;

        Vector3 camPos = Camera.main.transform.position;
        PlacObj_Cabin closest = null;
        float closestDistance = float.MaxValue;

        foreach (var cabin in cabins)
        {
            float distance = Vector3.Distance(camPos, cabin.transform.position);
            if (distance < closestDistance)
            {
                closest = cabin;
                closestDistance = distance;
            }
        }

        return closest;
    }


    private void OnEnable()
    {
        IsOpen = true;
    }

    private void OnDisable()
    {
        IsOpen = false;
    }



}
