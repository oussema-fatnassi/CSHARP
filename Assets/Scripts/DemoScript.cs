using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;

    public void PickUpItem(int id){
        bool result = inventoryManager.AddItem(itemsToPickUp[id]);
        if(result){
            Debug.Log("Item added to inventory");
        } else {
            Debug.Log("Inventory is full");
        }
    }
}
