
using UnityEngine;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> customers;
    [SerializeField] private int maxActive = 2;

    private HashSet<int> activeIndexes = new HashSet<int>();

    void Start()
    {
        foreach (var c in customers)
        {
            c.SetActive(true);
        }

        for (int i = 0; i < maxActive && i < customers.Count; i++)
        {
            ActivateRandomCustomer();
        }
    }
    void ActivateRandomCustomer()
    {
        var available = new List<int>();
        for (int i = 0; i < customers.Count; i++)
        {
            if (!activeIndexes.Contains(i))
                available.Add(i);
        }

        if (available.Count == 0) return;

        int randIndex = available[Random.Range(0, available.Count)];
        ActivateCustomer(randIndex);
    }

    void ActivateCustomer(int index)
    {
        activeIndexes.Add(index);

        var customer = customers[index];
        var om = customer.GetComponentInChildren<OrderManager>(true);
        if (om != null)
        {
            om.ResetOrder();

            om.onOrderComplete.RemoveAllListeners();
            om.onOrderExpire.RemoveAllListeners();

            int capturedIndex = index; // ป้องกัน closure bug
            om.onOrderComplete.AddListener(() => OnDone(capturedIndex));
            om.onOrderExpire.AddListener(() => OnDone(capturedIndex));
        }
    }

    void OnDone(int index)
    {
        activeIndexes.Remove(index);   // ปิดสถานะ active
        var om = customers[index].GetComponentInChildren<OrderManager>(true);
        if (om != null) om.ClearOrder();  // เคลียร์คำสั่ง

        ActivateRandomCustomer();
    }
}
