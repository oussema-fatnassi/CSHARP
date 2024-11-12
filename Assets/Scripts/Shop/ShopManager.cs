using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
    This class is responsible for managing the shop in the game.
    It handles the opening and closing of the shop, as well as the buying of items.
*/

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
    // Open the shop panel if the player is in range
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
    // Set the player in range status
    public void SetPlayerInRange(bool inRange)
    {
        playerInRange = inRange;
    }
    // Set the current item in the shop
    public void SetCurrentItem(Item item)
    {
        currentItem = item;
    }
    // Open the shop panel
    public void OpenShop()
    {
        shopPanel.SetActive(true);
        FillShop();
    }
    // Fill the shop panel with the current item details
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
    // Close the shop panel
    public void CloseShop()
    {
        shopPanel.SetActive(false);
        itemQuantity.text = "0";
    }
    // Increase the quantity of the item
    public void IncreaseQuantity()
    {
        itemQuantity.text = (int.Parse(itemQuantity.text) + 1).ToString();
    }
    // Decrease the quantity of the item
    public void DecreaseQuantity()
    {
        itemQuantity.text = (int.Parse(itemQuantity.text) - 1).ToString();
        if (int.Parse(itemQuantity.text) < 1)
        {
            itemQuantity.text = "0";
        }
    }
    // Buy the item from the shop
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
                InventoryManager.instance.AddItem(currentItem, 1); 
            }
            
            CloseShop();
        }
        else
        {
            Debug.Log("Not enough money to buy " + quantity + " of " + currentItem.type);
        }
        InventoryManager.instance.ConsolidateInventoryItems();
    }

}
