using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
    public string itemName; // The name of the item (or any unique identifier)
    public int count; // The count of that item
    public int slotIndex; // The index of the slot in the inventory

    public InventoryItemData(string itemName, int count, int slotIndex)
    {
        this.itemName = itemName;
        this.count = count;
        this.slotIndex = slotIndex;
    }
}