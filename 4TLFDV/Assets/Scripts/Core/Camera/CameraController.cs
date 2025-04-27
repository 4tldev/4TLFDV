using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float mouseEdgeThreshold = 50f; // Pixels from screen edge
    [SerializeField] private float mouseMoveSpeed = 10f;

    [Header("Mouse Movement Toggle")]
    [SerializeField] private KeyCode toggleMouseMoveKey = KeyCode.Y;
    private bool enableMouseMovement = true;

    private void Update()
    {
        HandleKeyboardMovement();
        HandleMouseEdgeMovement();
        HandleMouseMovementToggle();
    }

    private void HandleKeyboardMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D, Left/Right
        float moveY = Input.GetAxisRaw("Vertical");   // W/S, Up/Down

        Vector3 moveDir = new Vector3(moveX, moveY, 0f).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleMouseEdgeMovement()
    {
        if (!enableMouseMovement) return;

        Vector3 mousePos = Input.mousePosition;
        Vector3 moveDir = Vector3.zero;

        if (mousePos.x >= Screen.width - mouseEdgeThreshold)
            moveDir.x += 1f;
        else if (mousePos.x <= mouseEdgeThreshold)
            moveDir.x -= 1f;

        if (mousePos.y >= Screen.height - mouseEdgeThreshold)
            moveDir.y += 1f;
        else if (mousePos.y <= mouseEdgeThreshold)
            moveDir.y -= 1f;

        transform.position += moveDir.normalized * mouseMoveSpeed * Time.deltaTime;
    }

    private void HandleMouseMovementToggle()
    {
        if (Input.GetKeyDown(toggleMouseMoveKey))
        {
            enableMouseMovement = !enableMouseMovement;
            Debug.Log($"Mouse edge movement: {(enableMouseMovement ? "Enabled" : "Disabled")}");
        }
    }
}
