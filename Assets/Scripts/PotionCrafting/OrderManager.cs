using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Order
{
    public string customerName;
    public List<GameObject> potions;
}

public class OrderManager : MonoBehaviour
{
    [SerializeField] private List<Order> possibleOrders;
    [SerializeField] private List<Order> completedOrders;

    [SerializeField] private Order currentOrder;
    [SerializeField] private List<GameObject> potionsInDelivery;

    private void Start()
    {
        GenerateNewOrder();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CompleteOrder();
        }
    }

    private void GenerateNewOrder()
    {
        if (possibleOrders.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, possibleOrders.Count);
            currentOrder = possibleOrders[randomIndex];
            possibleOrders.RemoveAt(randomIndex);
        }
        else
        {
            Debug.Log("No more orders available.");
        }
    }

    private void CompleteOrder()
    {
        if (currentOrder != null && ValidateCurrentOrder())
        {
            completedOrders.Add(currentOrder);
            currentOrder = null;
            GenerateNewOrder();
        }
        else
        {
            Debug.Log("Current order is not valid or already completed.");
        }
    }

    private bool ValidateCurrentOrder()
    {
        if (currentOrder == null || potionsInDelivery.Count != currentOrder.potions.Count)
            return false;

        var requiredCounts = new Dictionary<GameObject, int>();
        foreach (var potion in currentOrder.potions)
        {
            if (requiredCounts.ContainsKey(potion))
                requiredCounts[potion]++;
            else
                requiredCounts[potion] = 1;
        }

        var deliveryCounts = new Dictionary<GameObject, int>();
        foreach (var potion in potionsInDelivery)
        {
            if (deliveryCounts.ContainsKey(potion))
                deliveryCounts[potion]++;
            else
                deliveryCounts[potion] = 1;
        }

        foreach (var kvp in requiredCounts)
        {
            if (!deliveryCounts.TryGetValue(kvp.Key, out int count) || count != kvp.Value)
                return false;
        }

        foreach (GameObject potion in potionsInDelivery)
            Destroy(potion);
        potionsInDelivery.Clear();

        //play feedback

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            if (other.transform.parent != null && other.transform.parent.CompareTag("Potion"))
            {
                potionsInDelivery.Add(other.transform.parent.gameObject);
            }
            else
            {
                potionsInDelivery.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            if (other.transform.parent != null && other.transform.parent.CompareTag("Potion"))
            {
                potionsInDelivery.Remove(other.transform.parent.gameObject);
            }
            else
            {
                potionsInDelivery.Remove(other.gameObject);
            }
        }
    }
}