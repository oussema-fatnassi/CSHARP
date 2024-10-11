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
    [SerializeField] private TMP_Text itemQuantity;

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
        itemQuantity.text = "0";
    }

    public void IncreaseQuantity()
    {
        itemQuantity.text = (int.Parse(itemQuantity.text) + 1).ToString();
    }

    public void DecreaseQuantity()
    {
        itemQuantity.text = (int.Parse(itemQuantity.text) - 1).ToString();
        if (int.Parse(itemQuantity.text) < 1)
        {
            itemQuantity.text = "0";
        }
    }

    public void BuyItem()
    {
        int quantity = int.Parse(itemQuantity.text); 
        int totalCost = currentItem.cost * quantity; 

        if (CurrencyManager.instance.CanAfford(totalCost))
        {
            CurrencyManager.instance.SpendMoney(totalCost); 
            Debug.Log("Purchased " + quantity + " of " + currentItem.type + " for " + totalCost + " currency.");

            for (int i = 0; i < quantity; i++)
            {
                InventoryManager.instance.AddItem(currentItem); 
            }
            
            CloseShop();
        }
        else
        {
            Debug.Log("Not enough money to buy " + quantity + " of " + currentItem.type);
        }
    }

}
