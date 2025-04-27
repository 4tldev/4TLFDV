using TMPro;
using UnityEngine;

public class UI_GoldUpdater : MonoBehaviour
{
    [SerializeField] private Player player; // Drag your Player here in Inspector
    [SerializeField] private TextMeshProUGUI goldText;

    private void Update()
    {
        if (player != null && goldText != null)
        {
            goldText.text = $"Gold: {player.GetGold()}";
        }
    }
}
