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
    [SerializeField] private List<Item> items;

    private bool playerInRange = false; 

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!shopPanel.activeInHierarchy)
            {
                OpenShop();
            }
            else
            {
                CloseShop(); 
            }
        }
    }

    public void SetPlayerInRange(bool inRange)
    {
        playerInRange = inRange;
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }
}
