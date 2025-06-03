using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Order
{
    public string customerName;
    public List<PotionType> requiredPotions;
    
    public string GetOrderInfo()
    {
        string info = $"Customer: {customerName}\n\nRequired potions:\n";
        foreach (var potion in requiredPotions)
        {
            if (potion != null)
                info += $"- {potion.name} ";
                info += $"- {potion.GetPotionInfo()}\n";
        }
        return info;
    }
}

public class OrderManager : MonoBehaviour
{
    [Header("Order Management")]
    [SerializeField] private List<Order> possibleOrders;
    [SerializeField] private List<Order> completedOrders;
    [SerializeField] private Order currentOrder;
    [SerializeField] private Queue<Order> pendingOrders;

    [Header("Order Generation")]
    [SerializeField] private int ordersToGenerate = 10;

    [Header("Delivery System")]
    [SerializeField] private List<GameObject> potionsInDelivery;
    [SerializeField] private TextMeshProUGUI orderText;

    [Header("Debug")]
    [SerializeField] private bool showDebugLogs = true;

    private void Start()
    {
        GenerateInitialOrders();
        AssignNextOrder();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CompleteOrder();
        }

        if (Input.GetKeyDown(KeyCode.O) && showDebugLogs)
        {
            ShowCurrentOrderInfo();
        }
    }

    private void GenerateInitialOrders()
    {
        pendingOrders = new Queue<Order>();

        if (possibleOrders.Count == 0)
        {
            Debug.LogError("No possible orders configured!");
            return;
        }

        for (int i = 0; i < ordersToGenerate; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, possibleOrders.Count);
            Order selectedOrder = possibleOrders[randomIndex];

            Order newOrder = new Order
            {
                customerName = selectedOrder.customerName,
                requiredPotions = new List<PotionType>(selectedOrder.requiredPotions)
            };

            pendingOrders.Enqueue(newOrder);
        }

        if (showDebugLogs)
        {
            Debug.Log($"Generated {ordersToGenerate} orders. Orders in queue: {pendingOrders.Count}");
        }
    }

    private void AssignNextOrder()
    {
        if (pendingOrders.Count > 0)
        {
            currentOrder = pendingOrders.Dequeue();
            orderText.text = currentOrder.GetOrderInfo();

            if (showDebugLogs)
            {
                Debug.Log($"New order assigned:\n{currentOrder.GetOrderInfo()}");
                Debug.Log($"Remaining orders: {pendingOrders.Count}");
            }
        }
        else
        {
            currentOrder = null;
            orderText.text = "No more orders available.";
            Debug.Log("No more orders available.");
        }
    }

    public void CompleteOrder()
    {
        if (currentOrder != null && ValidateCurrentOrder())
        {
            completedOrders.Add(currentOrder);
            if (showDebugLogs)
            {
                Debug.Log($"Order completed for {currentOrder.customerName}!");
                Debug.Log($"Completed orders: {completedOrders.Count}");
            }
            currentOrder = null;
            AssignNextOrder();
        }
        else
        {
            AssignNextOrder();

            if (showDebugLogs)
            {
                Debug.Log("Current order is not valid or already completed.");
            }
        }
    }

    private bool ValidateCurrentOrder()
    {
        if (currentOrder == null)
        {
            if (showDebugLogs) Debug.Log("No current order");
            return false;
        }

        if (potionsInDelivery.Count != currentOrder.requiredPotions.Count)
        {
            if (showDebugLogs)
            {
                Debug.Log($"Potion count mismatch. Required: {currentOrder.requiredPotions.Count}, Delivered: {potionsInDelivery.Count}");
            }
            return false;
        }

        var requiredCounts = new Dictionary<int, int>();
        foreach (var potionType in currentOrder.requiredPotions)
        {
            if (potionType != null)
            {
                if (requiredCounts.ContainsKey(potionType.potionID))
                    requiredCounts[potionType.potionID]++;
                else
                    requiredCounts[potionType.potionID] = 1;
            }
        }

        var deliveryCounts = new Dictionary<int, int>();
        foreach (var potion in potionsInDelivery)
        {
            var potionType = potion.GetComponent<PotionType>();
            if (potionType != null)
            {
                if (deliveryCounts.ContainsKey(potionType.potionID))
                    deliveryCounts[potionType.potionID]++;
                else
                    deliveryCounts[potionType.potionID] = 1;
            }
            else if (showDebugLogs)
            {
                Debug.LogWarning($"Potion {potion.name} missing PotionType component");
            }
        }

        foreach (var kvp in requiredCounts)
        {
            if (!deliveryCounts.TryGetValue(kvp.Key, out int count) || count != kvp.Value)
            {
                if (showDebugLogs)
                {
                    Debug.Log($"Validation failed for potion ID {kvp.Key}. Required: {kvp.Value}, Delivered: {count}");
                }
                return false;
            }
        }

        foreach (GameObject potion in potionsInDelivery)
            Destroy(potion);
        potionsInDelivery.Clear();

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            potionsInDelivery.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            potionsInDelivery.Remove(other.gameObject);
        }
    }

    private void ShowCurrentOrderInfo()
    {
        if (currentOrder != null)
        {
            Debug.Log($"Current Order:\n{currentOrder.GetOrderInfo()}");
            Debug.Log($"Potions in delivery: {potionsInDelivery.Count}");
        }
        else
        {
            Debug.Log("No current order");
        }
    }

    public Order GetCurrentOrder()
    {
        return currentOrder;
    }

    public List<GameObject> GetPotionsInDelivery()
    {
        return new List<GameObject>(potionsInDelivery);
    }

    public int GetRemainingOrdersCount()
    {
        return pendingOrders?.Count ?? 0;
    }

    public int GetCompletedOrdersCount()
    {
        return completedOrders?.Count ?? 0;
    }

    public void GotoCombatScene()
    {
        SceneManager.LoadScene("CombatScene");
    }
}