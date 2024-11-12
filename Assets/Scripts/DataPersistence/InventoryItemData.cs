using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is responsible for storing the data of the inventory items.
    It is used by the GameData class to serialize and deserialize the inventory items.
*/

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