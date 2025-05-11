using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    ///Controller///
    private InputAction moveAction;
    private Rigidbody rb;
    public float speed;
    public float turnSpeed;
    public bool isAtCoffeePotZone = false;
    public bool isAtFridgeZone = false;
    public bool isAtSnacksBarZone = false;
    public bool isAtVendingMachineZone = false;
    public bool isAtWaterDispenserZone = false;

    ///Scoring///
    public int score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");

        isAtCoffeePotZone = false;
        isAtFridgeZone = false;
        isAtSnacksBarZone = false;
        isAtVendingMachineZone = false;
        isAtWaterDispenserZone = false;
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

    public void OnTriggerEnter( Collider interactables )
    {
        if ( interactables.CompareTag("CoffeePot") )
        {
            isAtCoffeePotZone = true;
        }
        if ( interactables.CompareTag("Fridge") )
        {
            isAtFridgeZone = true;
        }
        if ( interactables.CompareTag("SnacksBar") )
        {
            isAtSnacksBarZone = true;
        }
        if ( interactables.CompareTag("VendingMachine") )
        {
            isAtVendingMachineZone = true;
        }
        if ( interactables.CompareTag("WaterDispenser") )
        {
            isAtWaterDispenserZone = true;
        }
    }

    public void OnTriggerExit( Collider interactables )
    {
        if ( interactables.CompareTag("CoffeePot") )
        {
            isAtCoffeePotZone = false;
        }
        if ( interactables.CompareTag("Fridge") )
        {
            isAtFridgeZone = false;
        }
        if ( interactables.CompareTag("SnacksBar") )
        {
            isAtSnacksBarZone = false;
        }
        if ( interactables.CompareTag("VendingMachine") )
        {
            isAtVendingMachineZone = false;
        }
        if ( interactables.CompareTag("WaterDispenser") )
        {
            isAtWaterDispenserZone = false;
        }
    }
}