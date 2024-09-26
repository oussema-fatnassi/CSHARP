using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;

    public void PickUpItem(int id){
        inventoryManager.AddItem(itemsToPickUp[id]);
    }
}
