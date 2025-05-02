using UnityEngine;

public class ChangeHandStatePlacObj : MonoBehaviour
{
    [SerializeField] private HANDSTATE newHandState = HANDSTATE.SEED;

    private void OnMouseDown()
    {
        if (Grid_Manager.Instance.Player != null)
        {
            Grid_Manager.Instance.Player.SetHand(newHandState);
            Debug.Log($"{gameObject.name} clicked → HANDSTATE set to {newHandState}");
        }
    }
}
