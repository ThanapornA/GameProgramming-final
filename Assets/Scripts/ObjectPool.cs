using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public GameObject CoffeePrefabs;
    public GameObject FoodPrefabs;
    public GameObject SnackPrefabs;
    public GameObject SodaPrefabs;
    public GameObject WaterPrefabs;

    public Transform[] positionsToDrop;

    public int objPoolSize = 0;

    private List<GameObject> CoffeePool = new List<GameObject>();
    private List<GameObject> FoodPool = new List<GameObject>();
    private List<GameObject> SnackPool = new List<GameObject>();
    private List<GameObject> SodaPool = new List<GameObject>();
    private List<GameObject> WaterPool = new List<GameObject>();

    private static ObjectPool instance;

    public PlayerController playerController;

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
            CreateSnack();
            CreateSoda();
            CreateWater();
        }
    }

    void Update()
    {
        if ( playerController.isAtCoffeePotZone == true && Input.GetKeyDown(KeyCode.Space) && CoffeePool.Count > 0 )
        {
            AcquireCoffee();
        }
        if ( playerController.isAtFridgeZone == true && Input.GetKeyDown(KeyCode.Space) && FoodPool.Count > 0 )
        {
            AcquireFood();
        }
        if ( playerController.isAtSnacksBarZone == true && Input.GetKeyDown(KeyCode.Space) && SnackPool.Count > 0 )
        {
            AcquireSnack();
        }
        if ( playerController.isAtVendingMachineZone == true && Input.GetKeyDown(KeyCode.Space) && SodaPool.Count > 0 )
        {
            AcquireSoda();
        }
        if ( playerController.isAtWaterDispenserZone == true && Input.GetKeyDown(KeyCode.Space) && WaterPool.Count > 0 )
        {
            AcquireWater();
        }
    }
    
    /// create items : coffee , food , snack , soda , water ///
    public void CreateItem( GameObject prefab , int posNumIndex , List<GameObject> poolName)
    {
        GameObject a = Instantiate( prefab , positionsToDrop[posNumIndex].position , transform.rotation );
        a.SetActive(false);
        poolName.Add(a);
    }

    public void CreateCoffee() => CreateItem(CoffeePrefabs , 0 , CoffeePool);
    public void CreateFood()   => CreateItem(FoodPrefabs , 1 , FoodPool);
    public void CreateSnack()  => CreateItem(SnackPrefabs , 2 , SnackPool);
    public void CreateSoda()   => CreateItem(SodaPrefabs , 3 , SodaPool);
    public void CreateWater()  => CreateItem(WaterPrefabs , 4 , WaterPool);

    /// acquire items ///
    public GameObject AcquireItems( System.Action createItemMethod , List<GameObject> poolName)
    {
        if ( poolName.Count == 0  )
        {
            createItemMethod();
        }

        GameObject holding = null;

            holding = poolName[0];
            poolName.RemoveAt(0);
            holding.SetActive(true);


        return holding;
    }

    public GameObject AcquireCoffee() => AcquireItems( CreateCoffee , CoffeePool );
    public GameObject AcquireFood() => AcquireItems( CreateFood , FoodPool );
    public GameObject AcquireSnack() => AcquireItems( CreateSnack , SnackPool );
    public GameObject AcquireSoda() => AcquireItems( CreateSoda , SodaPool );
    public GameObject AcquireWater() => AcquireItems( CreateWater , WaterPool );

    /// return items ///
    public void Return(GameObject returnObj)
    {
        returnObj.SetActive(false);

        if (returnObj.CompareTag("COFFEE") && !CoffeePool.Contains(returnObj))
        {
            CoffeePool.Add(returnObj);
        }
        else if (returnObj.CompareTag("FOOD") && !FoodPool.Contains(returnObj))
        {
            FoodPool.Add(returnObj);
        }
        else if (returnObj.CompareTag("SNACKS") && !SnackPool.Contains(returnObj))
        {
            SnackPool.Add(returnObj);
        }
        else if (returnObj.CompareTag("DRINKS") && !SodaPool.Contains(returnObj))
        {
            SodaPool.Add(returnObj);
        }
        else if (returnObj.CompareTag("WATER") && !WaterPool.Contains(returnObj))
        {
            WaterPool.Add(returnObj);
        }
    }
}
