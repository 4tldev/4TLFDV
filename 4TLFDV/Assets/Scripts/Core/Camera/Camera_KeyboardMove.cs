using UnityEngine;

public class Camera_KeyboardMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    public void TickMove()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(moveX, moveY, 0f).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
