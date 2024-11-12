using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
    This class is responsible for managing the inventory items in the game.
    It handles the drag and drop functionality of the inventory items.
*/

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TMP_Text countText;
    [HideInInspector]public Transform parentToReturnTo;
    [HideInInspector]public Item item;
    [HideInInspector]public int count = 1;
    public Image image;
    // Initialize the inventory item with the given item
    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }
    // Increase the count of the inventory item
    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
    // Handle the beginning of the drag event
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentToReturnTo = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }
    // Handle the dragging event
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    // Handle the end of the drag event
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentToReturnTo);
        image.raycastTarget = true;
    }
}
