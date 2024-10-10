using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemPrice;
    [SerializeField] private TMP_Text itemDescription;

    private bool playerInRange = false;
    private Item currentItem; 

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!shopPanel.activeInHierarchy)
            {
                OpenShop();
            }
        }
        else if (shopPanel.activeInHierarchy && !playerInRange)
        {
            CloseShop();
        }
    }

    public void SetPlayerInRange(bool inRange)
    {
        playerInRange = inRange;
    }

    public void SetCurrentItem(Item item)
    {
        currentItem = item;
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        FillShop();

    }

    private void FillShop()
    {
        if (currentItem != null)
        {
            itemName.text = currentItem.type.ToString(); 
            itemDescription.text = currentItem.description;
            itemPrice.text = currentItem.cost.ToString();
            itemImage.sprite = currentItem.image;
        }
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }
}
