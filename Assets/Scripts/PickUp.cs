using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject CoffeeOnPlayer;
    public GameObject FoodOnPlayer;
    public GameObject DrinksOnPlayer;
    public GameObject SnacksOnPlayer;
    public GameObject WaterOnPlayer;

    public bool hasItem = false;

    void Start()
    {
        CoffeeOnPlayer.SetActive(false);
        FoodOnPlayer.SetActive(false);
        DrinksOnPlayer.SetActive(false);
        SnacksOnPlayer.SetActive(false);
        WaterOnPlayer.SetActive(false);
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
                    other.gameObject.SetActive(false);
                    WaterOnPlayer.SetActive(true);
                    hasItem = true;
                }
            }
        }
    }
}
