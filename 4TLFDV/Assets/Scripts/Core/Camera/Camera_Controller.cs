using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    private Camera_KeyboardMove keyboardMove;
    private Camera_MouseMove mouseMove;

    private void Awake()
    {
        keyboardMove = GetComponent<Camera_KeyboardMove>();
        mouseMove = GetComponent<Camera_MouseMove>();
    }

    private void Update()
    {
        keyboardMove?.TickMove();
        mouseMove?.TickMove();
    }
}
