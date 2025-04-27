using UnityEngine;

public class WellTile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float playerFillTime = 0f; // Instant for Player
    [SerializeField] private float farmhandFillTime = 2f; // Farmhands take longer

    /// <summary>
    /// Gets how long it should take to fill water based on who is requesting.
    /// </summary>
    public float GetFillTime(bool isPlayer)
    {
        return isPlayer ? playerFillTime : farmhandFillTime;
    }

    // In the future we could add sound, VFX here when interacted with
}
