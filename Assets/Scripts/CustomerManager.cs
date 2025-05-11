
using UnityEngine;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> customers;
    [SerializeField] private int maxActive = 2;

    private int currentIndex = 0;

    void Start()
    {
        foreach (var c in customers)
        {
            
        }

        for (int i = 0; i < maxActive && i < customers.Count; i++)
        {
            ActivateCustomer(i);
        }
    }

    void ActivateCustomer(int index)
    {
        var customer = customers[index];
        customer.SetActive(true);

        var om = customer.GetComponentInChildren<OrderManager>(true);
        if (om != null)
        {
            om.ResetOrder(); 
        }
    }

    void OnDone(int index)
    {
        customers[index].SetActive(false);

        currentIndex++;
        if (currentIndex < customers.Count)
        {
            ActivateCustomer(currentIndex);
        }
    }
}
