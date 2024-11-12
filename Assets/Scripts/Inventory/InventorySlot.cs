using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
    This class is responsible for managing the inventory slots in the game.
    It handles the selection and deselection of the inventory slots.
*/

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;

    private void Awake() {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            GameObject droppedItem = eventData.pointerDrag;
            InventoryItem inventoryItem = droppedItem.GetComponent<InventoryItem>();
            inventoryItem.parentToReturnTo = transform;
        }
    }
}
