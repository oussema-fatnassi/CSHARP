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

    public void GetSelectedItem(){
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if(receivedItem != null){
            Debug.Log($"Selected item is {receivedItem.image.name}");
        } else {
            Debug.Log("No item selected");
        }
    }

        public void UseSelectedItem(){
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if(receivedItem != null){
            Debug.Log($"Selected item is {receivedItem.image.name}");
        } else {
            Debug.Log("No item selected");
        }
    }
}
