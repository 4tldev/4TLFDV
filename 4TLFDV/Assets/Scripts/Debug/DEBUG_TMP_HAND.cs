using TMPro;
using UnityEngine;
using static BaseFarmer;

public class DEBUG_TMP_HAND : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI handText;
    [SerializeField] private Player player;

    private void Update()
    {
        switch (player.handState)
        {
            case HANDSTATE.EMPTY:
                handText.text = "Hand: Empty";
                break;

            case HANDSTATE.WATER:
                handText.text = "Hand: Water";
                break;

            case HANDSTATE.SEED:
                handText.text = "Hand: Seed";
                break;

            case HANDSTATE.CROP:
                handText.text = "Hand: Crop";
                break;
        }
    }

}
