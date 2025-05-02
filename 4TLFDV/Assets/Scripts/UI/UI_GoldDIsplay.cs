using UnityEngine;
using TMPro;

public class UI_GoldDisplay : MonoBehaviour
{
    [SerializeField] private Player_Controller player;
    [SerializeField] private TextMeshProUGUI goldText;

    private void Update()
    {
        goldText.text = $"Gold: {player.Gold:N0}";
    }
}
