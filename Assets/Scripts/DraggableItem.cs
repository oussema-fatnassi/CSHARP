using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]public Transform parentToReturnTo = null;
    public Image image;
    [HideInInspector]public ConsumableItem consumableItem;

    private void Start()
    {
        InitializeConsumableItem(consumableItem);
    }
    public void InitializeConsumableItem(ConsumableItem item)
    {
        image.sprite = item.image;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentToReturnTo = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(parentToReturnTo);
        image.raycastTarget = true;
    }

}
