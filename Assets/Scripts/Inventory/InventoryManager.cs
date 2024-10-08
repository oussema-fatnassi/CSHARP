using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    public static InventoryManager instance;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    private const int maxItemCount = 5; 
    int selectedSlot = -1;

    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.3f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (IsSlotClicked(inventorySlots[i]))
                {
                    if (Time.time - lastClickTime < doubleClickThreshold)
                    {
                        ChangeSelectedSlot(i);
                    }
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

    private void Awake()
    {
        instance = this;
    }

    bool IsSlotClicked(InventorySlot slot)
    {
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

    public Item GetSelectedItem(bool use){
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null){
            Item item = itemInSlot.item;
            if(use == true){
                itemInSlot.count--;
                if(itemInSlot.count <= 0){
                    Destroy(itemInSlot.gameObject);
                } else {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }

    public void UseSelectedItem()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            Player player = playerObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player component is missing on the GameObject.");
                return;
            }

            Item selectedItem = GetSelectedItem(true);
            if (selectedItem != null)
            {
                ItemConsumptionManager.instance.ConsumeItem(player, selectedItem);
            }
            else
            {
                Debug.LogError("No item selected or item unavailable.");
            }
        }
        else
        {
            Debug.LogError("No player found in the scene.");
        }
    }

    public void DeactivateMovement()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            PlayerController playerController = playerObject.GetComponent<PlayerController>();
            
            if (playerController != null)
            {
                playerController.enabled = false;
                Debug.Log("Player movement has been deactivated.");
            }
            else
            {
                Debug.LogError("PlayerController component is missing on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("No player found in the scene.");
        }
    }

    public void ActivateMovement()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            PlayerController playerController = playerObject.GetComponent<PlayerController>();
            
            if (playerController != null)
            {
                playerController.enabled = true;
                Debug.Log("Player movement has been activated.");
            }
            else
            {
                Debug.LogError("PlayerController component is missing on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("No player found in the scene.");
        }
    }

    public void SaveData(ref GameData data)
    {
        data.inventoryItems.Clear(); 

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                InventoryItemData itemData = new InventoryItemData(itemInSlot.item.name, itemInSlot.count, i);
                data.inventoryItems.Add(itemData);
            }
        }
    }


    public void LoadData(GameData data)
    {
        foreach (var slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Destroy(itemInSlot.gameObject); 
            }
        }

        foreach (var itemData in data.inventoryItems)
        {
            if (itemData.slotIndex < 0 || itemData.slotIndex >= inventorySlots.Length)
            {
                continue;
            }

            Item item = Resources.Load<Item>($"ScriptableObjects/Consumable/{itemData.itemName}"); // Adjust the path accordingly
            if (item != null)
            {
                InventorySlot slot = inventorySlots[itemData.slotIndex];
                GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
                InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
                inventoryItem.InitializeItem(item);

                inventoryItem.count = itemData.count;
                inventoryItem.RefreshCount();
            }
            else
            {
                Debug.LogError($"Failed to load item {itemData.itemName} from Resources. Check the path or item existence.");
            }
        }
    }
}