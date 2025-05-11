using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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

    ///Special///
    public bool isTimeBoosted = false;

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

    public void OnTriggerEnter( Collider obj )
    {
        if ( obj.CompareTag("CoffeePot") )
        {
            isAtCoffeePotZone = true;
        }
        if ( obj.CompareTag("Fridge") )
        {
            isAtFridgeZone = true;
        }
        if ( obj.CompareTag("SnacksBar") )
        {
            isAtSnacksBarZone = true;
        }
        if ( obj.CompareTag("VendingMachine") )
        {
            isAtVendingMachineZone = true;
        }
        if ( obj.CompareTag("WaterDispenser") )
        {
            isAtWaterDispenserZone = true;
        }

        ///time boost///
        if ( obj.CompareTag("TimeBooster") )
        {
            isTimeBoosted = true;
            Destroy(obj.gameObject);
        }
    }

    public void OnTriggerExit( Collider obj )
    {
        if ( obj.CompareTag("CoffeePot") )
        {
            isAtCoffeePotZone = false;
        }
        if ( obj.CompareTag("Fridge") )
        {
            isAtFridgeZone = false;
        }
        if ( obj.CompareTag("SnacksBar") )
        {
            isAtSnacksBarZone = false;
        }
        if ( obj.CompareTag("VendingMachine") )
        {
            isAtVendingMachineZone = false;
        }
        if ( obj.CompareTag("WaterDispenser") )
        {
            isAtWaterDispenserZone = false;
        }
    }
}