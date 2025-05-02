using UnityEngine;

public class Camera_MouseMove : MonoBehaviour
{
    [SerializeField] private float mouseEdgeThreshold = 50f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private bool enableMouseEdgeMovement = true;

    public void TickMove()
    {
        if (!enableMouseEdgeMovement) return;

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

        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
    }

    public void ToggleMouseMovement()
    {
        enableMouseEdgeMovement = !enableMouseEdgeMovement;
    }
}
