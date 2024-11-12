using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is responsible for managing the player's inventory.
    It provides methods to add items to the inventory, use items, and save and load the inventory data.
*/

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    public static InventoryManager instance;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    private const int maxItemCount = 5; 
    int selectedSlot = -1;

    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.3f;
    // We update the inventory manager to handle double-clicking on the inventory slots to select them.
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
    // Method to change the selected slot based on the slot index.
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
    // Method to check if the slot is clicked.
    bool IsSlotClicked(InventorySlot slot)
    {
        RectTransform slotRect = slot.GetComponent<RectTransform>();
        Vector2 mousePosition = Input.mousePosition;
        return RectTransformUtility.RectangleContainsScreenPoint(slotRect, mousePosition);
    }
    // Method to add an item to the inventory.
    public bool AddItem(Item item, int quantity)
    {
        quantity = TryStackItem(item, quantity);

        while (quantity > 0)
        {
            int stackCount = Mathf.Min(quantity, maxItemCount);
            if (AddNewItem(item, stackCount))
            {
                quantity -= stackCount; 
            }
            else
            {
                Debug.Log($"{item.image.name} is full in the inventory!");
                return false; 
            }
        }

        return true; 
    }
    // Method to stack items in the inventory if possible.
    private int TryStackItem(Item item, int quantity)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxItemCount)
            {
                int spaceAvailable = maxItemCount - itemInSlot.count;
                int amountToStack = Mathf.Min(quantity, spaceAvailable);

                itemInSlot.count += amountToStack;
                itemInSlot.RefreshCount();
                quantity -= amountToStack;

                if (quantity <= 0) 
                {
                    return 0;
                }
            }
        }
        return quantity;
    }
    // Method to add a new item to the inventory.
    private bool AddNewItem(Item item, int count)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot, count);
                return true;
            }
        }
        return false;
    }
    //
    void SpawnNewItem(Item item, InventorySlot slot, int count)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
        inventoryItem.count = count; 
        inventoryItem.RefreshCount();
    }
    // Method to check if the inventory is full for the given item.
    private bool IsInventoryFullForItem(Item item, int quantity)
    {
        int totalSpaceAvailable = 0;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item)
            {
                totalSpaceAvailable += (maxItemCount - itemInSlot.count);
            }
            else if (itemInSlot == null)
            {
                totalSpaceAvailable += maxItemCount;
            }
        }

        return totalSpaceAvailable < quantity;
    }
    // Method to get the selected item from the inventory.
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
    // Method to use the selected item from the inventory.
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
    // Method to deactivate the player movement.
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
    // Method to activate the player movement.
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
    // Method to save the inventory data.
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
    // Method to load the inventory data.
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

            Item item = Resources.Load<Item>($"ScriptableObjects/Consumable/{itemData.itemName}"); 
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
    // Method to consolidate the inventory items.
    public void ConsolidateInventoryItems()
    {
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();

        foreach (var slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                string itemName = itemInSlot.item.name;
                if (!itemCounts.ContainsKey(itemName))
                {
                    itemCounts[itemName] = 0;
                }
                itemCounts[itemName] += itemInSlot.count;
                Destroy(itemInSlot.gameObject); 
            }
        }

        foreach (var kvp in itemCounts)
        {
            string itemName = kvp.Key;
            int totalItemCount = kvp.Value;

            Item item = Resources.Load<Item>($"ScriptableObjects/Consumable/{itemName}");
            while (totalItemCount > 0)
            {
                int stackCount = Mathf.Min(totalItemCount, maxItemCount);
                totalItemCount -= stackCount;

                AddItemStack(item, stackCount);
            }
        }
    }
    // Method to add an item stack to the inventory.
    private void AddItemStack(Item item, int count)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null) 
            {
                GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
                InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
                inventoryItem.InitializeItem(item);
                inventoryItem.count = count;
                inventoryItem.RefreshCount();
                break;
            }
        }
    }
}