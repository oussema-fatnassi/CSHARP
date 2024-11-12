using UnityEngine;
using TMPro;

/*
    This class is responsible for managing the currency in the game.
    It handles the total money the player has and updates the UI accordingly.
*/

public class CurrencyManager : MonoBehaviour, IDataPersistence
{
    public static CurrencyManager instance;

    [SerializeField] private int totalMoney = 0; 
    [SerializeField] private TMP_Text moneyText;    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Property to get and set the total money
    public int TotalMoney
    {
        get { return totalMoney; }
        set 
        { 
            totalMoney = Mathf.Max(0, value); 
            UpdateMoneyUI(); 
        }
    }
    // Check if the player can afford the cost
    public bool CanAfford(int cost)
    {
        return totalMoney >= cost;
    }
    // Spend money from the player's total money
    public void SpendMoney(int amount)
    {
        if (CanAfford(amount))
        {
            TotalMoney -= amount;
            UpdateMoneyUI(); 
        }
    }
    // Add money to the player's total money
    public void AddMoney(int amount)
    {
        TotalMoney += amount;
        UpdateMoneyUI(); 
    }
    // Update the money UI
    private void Start()
    {
        UpdateMoneyUI(); 
    }
    // Update the money UI
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text =  totalMoney.ToString();
        }
    }
    // Load the currency data
    public void LoadData(GameData data)
    {
        totalMoney = data.totalCurrency;
        UpdateMoneyUI();
    }
    // Save the currency data
    public void SaveData(ref GameData data)
    {
        data.totalCurrency = totalMoney;
    }
}
