using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public GameObject CoffeePrefabs;
    public GameObject FoodPrefebs;
    public Transform[] positionsToDrop;

    public int objPoolSize = 0;

    private List<GameObject> CoffeePool = new List<GameObject>();
    private List<GameObject> FoodPool = new List<GameObject>();

    private static ObjectPool instance;

    void Awake()
    {
        if ( instance != null )
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static ObjectPool GetInstance()
    {
        return instance;
    }

    void Start()
    {
        for ( int i = 0; i < objPoolSize; i++ )
        {
            CreateCoffee();
            CreateFood();
        }
    }

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.A) && CoffeePool.Count > 0 )
        {
            AcquireCoffee();
        }
        else if( Input.GetKeyDown(KeyCode.B) && FoodPool.Count > 0 )
        {
            AcquireFood();
        }
    }

    
    public void CreateCoffee()
    {
        GameObject a = Instantiate(CoffeePrefabs , positionsToDrop[0].position , transform.rotation );
        a.SetActive(false);
        CoffeePool.Add(a);
    }

    public void CreateFood()
    {
        GameObject b = Instantiate(FoodPrefebs , positionsToDrop[1].position , transform.rotation );
        b.SetActive(false);
        FoodPool.Add(b);
    }

    public GameObject AcquireCoffee()
    {
        if ( CoffeePool.Count == 0  )
        {
            CreateCoffee();
            Debug.Log("test");
        }


        GameObject holding = null;

            holding = CoffeePool[0];
            CoffeePool.RemoveAt(0);
            holding.SetActive(true);


        return holding;
    }

    public GameObject AcquireFood()
    {
        if ( FoodPool.Count == 0 && FoodPool.Count <= objPoolSize )
        {
            CreateFood();
        }

        GameObject holding = null;

            holding = FoodPool[0];
            FoodPool.RemoveAt(0);
            holding.SetActive(true);


        return holding;
    }


    public void Return(GameObject returnObj)
    {
        if (!returnObj.activeInHierarchy) return;

        returnObj.SetActive(false);

        if (returnObj.CompareTag("COFFEE") && !CoffeePool.Contains(returnObj))
        {
            CoffeePool.Add(returnObj);
        }
        else if (returnObj.CompareTag("FOOD") && !FoodPool.Contains(returnObj))
        {
            FoodPool.Add(returnObj);
        }
    }
}
