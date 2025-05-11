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
    public bool CheckOrder(List<ItemType> deliveredItems)
    {
        foreach (ItemType item in currentOrder)
        {
            if (!deliveredItems.Contains(item))
                return false;
        }
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