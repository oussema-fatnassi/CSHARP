using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    private const int maxItemCount = 5; 
    int selectedSlot = -1;

    private float lastClickTime = 0f; // Store the time of the last click
    private const float doubleClickThreshold = 0.3f;

    private void Update()
    {
        // Check for user input (click)
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            // Detect which slot was clicked
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (IsSlotClicked(inventorySlots[i]))
                {
                    // Check if this is a double-click
                    if (Time.time - lastClickTime < doubleClickThreshold)
                    {
                        ChangeSelectedSlot(i);
                    }
                    // Update the time of the last click
                    lastClickTime = Time.time;
                    break;
                }
            }
        }
    }
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0 && selectedSlot < inventorySlots.Length)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        selectedSlot = newValue;
        if (selectedSlot >= 0 && selectedSlot < inventorySlots.Length)
        {
            inventorySlots[selectedSlot].Select();
        }
    }

    // Helper method to determine if a specific slot was clicked
    bool IsSlotClicked(InventorySlot slot)
    {
        // Use the slot's RectTransform and check if the mouse is over it
        RectTransform slotRect = slot.GetComponent<RectTransform>();
        Vector2 mousePosition = Input.mousePosition;

        return RectTransformUtility.RectangleContainsScreenPoint(slotRect, mousePosition);
    }

    public bool AddItem(Item item)
    {
        if (IsInventoryFullForItem(item))
        {
            Debug.Log($"{item.image.name} is full in the inventory!");
            return false;
        }

        if (TryStackItem(item))
        {
            return true;
        }

        return AddNewItem(item);
    }

    private bool TryStackItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < 5)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        return false;
    }

    private bool AddNewItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    private bool IsInventoryFullForItem(Item item)
    {
        int totalItemCount = 0;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item)
            {
                totalItemCount += itemInSlot.count;
                if (totalItemCount >= maxItemCount)
                {
                    return true; 
                }
            }
        }
        return false; 
    }
}
