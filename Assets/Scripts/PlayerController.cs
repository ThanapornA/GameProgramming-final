using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputAction moveAction;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        float horiAction = moveAction.ReadValue<Vector2>().x;
        float verticalAction = moveAction.ReadValue<Vector2>().y;
        rb.AddForce( verticalAction * transform.right * speed , ForceMode.Force);
    }
}
