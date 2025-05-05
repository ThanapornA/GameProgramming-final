using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    ///Controller///
    private InputAction moveAction;
    private Rigidbody rb;
    public float speed;
    public float turnSpeed;

    ///PickUp System///

    ///Scoring///
    public int score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        float horiAction = moveAction.ReadValue<Vector2>().x;
        float vertiAction = moveAction.ReadValue<Vector2>().y;
        transform.Translate( vertiAction * speed * Time.deltaTime * Vector3.forward);
        transform.Translate( horiAction * speed * Time.deltaTime * Vector3.right);

        if ( horiAction != 0 )
        {
            transform.Rotate( Vector3.up , turnSpeed * horiAction );
        }

        if ( Input.GetKeyDown(KeyCode.LeftShift) )
        {
            Debug.Log("You press SHIFT. now the speed = 9");
            speed *= 3;
        }
        else if ( Input.GetKeyUp(KeyCode.LeftShift) )
        {
            speed = 3;
        }
    }
}
