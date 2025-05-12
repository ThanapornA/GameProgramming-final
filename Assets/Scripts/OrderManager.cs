using UnityEngine;

using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;


public class OrderManager : MonoBehaviour
{
    //เค้าลองใช้ Unity Events นะ
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI orderTextUI;

    [Header("Order Settings")]
    [SerializeField] private float orderPatienceTime = 15f; //15 วิ ไปก่อนไม่แน่ใจว่าเอาเท่าไหร่ดี
    [SerializeField] private bool multiItemMode = false; // เปิด true ถ้าจะสุ่มหลายอย่าง

    [Header("Events")] //EVENT ของ Check ว่าส่งเป็นไง
    public UnityEvent onOrderExpire;
    public UnityEvent onOrderComplete;

    private float currentPatience;
    private bool orderActive;
    
    public enum ItemType { COFFEE, FOOD, DRINKS, SNACKS, WATER }
    
    private readonly List<ItemType> currentOrder = new();
    private readonly ItemType[] allItems = (ItemType[])System.Enum.GetValues(typeof(ItemType));

    ///added : check what player is holding so that it can check the order
    public string Name = null;
    public PickUp pickUpSystem;
    public string tagEntered;
    private bool isItemGiven = false;
    private bool isContactWithPlayer = false;

    void Start()
    {
        GenerateNewOrder();
    }

    
    void Update()
    {
        if (orderActive)
        {
            currentPatience -= Time.deltaTime;
            UpdateOrderUI();

            if (currentPatience <= 0)
            {
                Debug.Log("[OrderManager] Order expired!");
                onOrderExpire?.Invoke();
                GenerateNewOrder();
            }
        }

        if ( pickUpSystem.gaveItem == true &&
            pickUpSystem.isContactWithEmployee == true &&
            isItemGiven == false &&
            isContactWithPlayer == true )
        {
            ItemType? deliveredItemEnum = GetItemTypeFromTag(tagEntered);

            if ( deliveredItemEnum != null )
            {
                Debug.Log("player gave item to me!");
                isItemGiven = true;

                List<ItemType> deliveredItemsEnumList = new List<ItemType> { deliveredItemEnum.Value };

                bool isCorrectOrder = CheckOrder(deliveredItemsEnumList);
                if (isCorrectOrder)
                {
                    Debug.Log("✅ Correct item delivered!");
                    CompleteOrder();
                }
                else
                {
                    Debug.Log("❌ Incorrect item");
                }

                isItemGiven = false;
                pickUpSystem.gaveItem = false;

            }
        }
    }

    void GenerateNewOrder()
    {
        currentOrder.Clear();
        if (multiItemMode)
        {
            int itemCount = Random.Range(3, 6);
            List<int> usedIndices = new();

            while (currentOrder.Count < itemCount)
            {
                int index = Random.Range(0, allItems.Length);
                if (!usedIndices.Contains(index))
                {
                    usedIndices.Add(index);
                    currentOrder.Add(allItems[index]);
                }
            }
        }
        else
        {
            currentOrder.Add(allItems[Random.Range(0, allItems.Length)]);
        }

        ///employee no + requests are show on console///
        Name = gameObject.name;
        Debug.Log($"{Name} [OrderManager] New Order Generated: {string.Join(", ", currentOrder)}");

        currentPatience = orderPatienceTime;
        orderActive = true;
        UpdateOrderUI(); 
    }

    /// <summary>
    /// อัปเดต UI ให้แสดงคำสั่งและเวลาแต่ไว้ก่อน
    /// </summary>
    
     void UpdateOrderUI()
    {
        if (orderTextUI != null)
        {
            string timeStr = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(currentPatience / 60), Mathf.FloorToInt(currentPatience % 60));
            orderTextUI.text = $"Order: {string.Join(", ", currentOrder)}\nTime Left: {timeStr}";
        }
    }
    
    /// <summary>
    /// ตรวจสอบว่าผู้เล่นส่งของครบตามคำสั่งหรือไม่
    /// </summary>
    /// 
    /// steps = get tag > convert tags to enum > check if player give the item > get enum > then checkOrder() will check that enum
    public void OnTriggerEnter(Collider objEntered)
    {
        if ( !objEntered.CompareTag("Player") )
        {
            tagEntered = objEntered.tag;
            Debug.Log($"tag entered : {tagEntered}");
            isContactWithPlayer = true;
        }
    }

    public void OnTriggerExit(Collider objExit)
    {
        if ( objExit.CompareTag("Player") )
        {
            isContactWithPlayer = false;
        }
    }

    public ItemType? GetItemTypeFromTag(string tag)
    {
        if (System.Enum.TryParse(tag, out ItemType itemType))
        {
            Debug.Log($"ENUM tag entered : {tag}");
            return itemType;
        }
        else
        {
            return null;
        }
    }

    public bool CheckOrder(List<ItemType> deliveredItems)
    {
        string currentOrderText = string.Join(", ", currentOrder);
        string deliveredItemsText = string.Join(", ", deliveredItems);
        Debug.Log($"{Name} Checking order: Current Order = {currentOrderText}, Delivered Items = {deliveredItemsText}");

        foreach (ItemType item in currentOrder)
        {
            if (!deliveredItems.Contains(item))
            {
                Debug.Log($".......{Name} dont want this.........");
                return false;
            }
        }
        Debug.Log($".......{Name} want this.........");
        return true;
    }
    
    

    public void CompleteOrder()
    {
        Debug.Log("[OrderManager] Order completed successfully!");
        onOrderComplete?.Invoke();
        GenerateNewOrder();
    }
    
    public void ResetOrder()
    {
        StopAllCoroutines(); // ถ้ามี coroutine ใดทำงานอยู่
        GenerateNewOrder();  // สุ่มคำสั่งใหม่
    }
    public void ClearOrder()
    {   
        currentOrder.Clear();
        orderActive = false;
        orderTextUI.text = ""; // หรือขึ้นว่า “รอคำสั่งถัดไป...”
    }
}