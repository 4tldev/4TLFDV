using UnityEngine;
using UnityEngine.UI;

public class UI_Toggler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject targetUI;

    [Header("Controls")]
    [SerializeField] private Button toggleButton;
    [SerializeField] private KeyCode toggleKey;

    private void Start()
    {
        if (targetUI == null)
        {
            // Debug.LogWarning("Target UI not assigned on " + gameObject.name); 
            return;
        }

        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleDisplay);
        }

        // OPTIONAL: Start disabled
        targetUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleDisplay();
        }
    }

    private void ToggleDisplay()
    {
        if (targetUI != null)
        {
            targetUI.SetActive(!targetUI.activeSelf);
        }
    }
}
