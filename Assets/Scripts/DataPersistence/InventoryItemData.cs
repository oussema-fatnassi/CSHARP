using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
    public string itemName;
    public int count;
    public int slotIndex;

    public InventoryItemData(string itemName, int count, int slotIndex)
    {
        this.itemName = itemName;
        this.count = count;
        this.slotIndex = slotIndex;
    }
}