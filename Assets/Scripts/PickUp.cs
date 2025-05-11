using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject CoffeeOnPlayer;
    public GameObject FoodOnPlayer;
    public GameObject DrinksOnPlayer;
    public GameObject SnacksOnPlayer;
    public GameObject WaterOnPlayer;

    public GameObject currentItemObject = null;

    public bool hasItem = false;
    public bool isContactWithEmployee = false;

    void Start()
    {
        CoffeeOnPlayer.SetActive(false);
        FoodOnPlayer.SetActive(false);
        DrinksOnPlayer.SetActive(false);
        SnacksOnPlayer.SetActive(false);
        WaterOnPlayer.SetActive(false);
    }

    void Update()
    {
        if ( hasItem == true && isContactWithEmployee == true && Input.GetKey(KeyCode.E) )
        {
            Debug.Log("you gave item");
            DropItem();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if ( hasItem == false )
        {
            ///coffee
            if( other.gameObject.tag == "COFFEE" )
            {
                if ( Input.GetKey(KeyCode.E) )
                {
                    currentItemObject = other.gameObject;

                    other.gameObject.SetActive(false);
                    CoffeeOnPlayer.SetActive(true);
                    hasItem = true;
                }
            }

            ///burger
            if( other.gameObject.tag == "FOOD" )
            {
                if ( Input.GetKey(KeyCode.E) )
                {
                    currentItemObject = other.gameObject;

                    other.gameObject.SetActive(false);
                    FoodOnPlayer.SetActive(true);
                    hasItem = true;
                }
            }
        
            ///soda
            if( other.gameObject.tag == "DRINKS" )
            {
                if ( Input.GetKey(KeyCode.E) )
                {
                    currentItemObject = other.gameObject;

                    other.gameObject.SetActive(false);
                    DrinksOnPlayer.SetActive(true);
                    hasItem = true;
                }
            }

            ///chip
            if( other.gameObject.tag == "SNACKS" )
            {
                if ( Input.GetKey(KeyCode.E) )
                {
                    currentItemObject = other.gameObject;

                    other.gameObject.SetActive(false);
                    SnacksOnPlayer.SetActive(true);
                    hasItem = true;
                }
            }
        
            ///water
            if( other.gameObject.tag == "WATER" )
            {
                if ( Input.GetKey(KeyCode.E) )
                {
                    currentItemObject = other.gameObject;
                    
                    other.gameObject.SetActive(false);
                    WaterOnPlayer.SetActive(true);
                    hasItem = true;
                }
            }
        }
                    
        ///drop item///
        if( other.gameObject.tag == "TrashCan" && Input.GetKey(KeyCode.E) )
        {
            DropItem();
        }
    }

    public void DropItem()
    {
        Debug.Log("you now have no item");
        ObjectPool.GetInstance().Return(currentItemObject);

        CoffeeOnPlayer.SetActive(false);
        FoodOnPlayer.SetActive(false);
        DrinksOnPlayer.SetActive(false);
        SnacksOnPlayer.SetActive(false);
        WaterOnPlayer.SetActive(false);

        hasItem = false;
    }

    ///deliver item///
    public void OnTriggerEnter ( Collider customer )
    {
        if ( customer.gameObject.CompareTag("Employee") )
        {
            isContactWithEmployee = true;
            Debug.Log("press E to give item");
        }
    }

    public void OnTriggerExit ( Collider customer )
    {
        if ( customer.gameObject.CompareTag("Employee") )
        {
            isContactWithEmployee = false;
            Debug.Log("you give item");
        }
    }
}
