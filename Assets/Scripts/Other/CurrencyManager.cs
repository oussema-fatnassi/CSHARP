using UnityEngine;
using TMPro;

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

    public int TotalMoney
    {
        get { return totalMoney; }
        set 
        { 
            totalMoney = Mathf.Max(0, value); 
            UpdateMoneyUI(); 
        }
    }

    public bool CanAfford(int cost)
    {
        return totalMoney >= cost;
    }

    public void SpendMoney(int amount)
    {
        if (CanAfford(amount))
        {
            TotalMoney -= amount;
            UpdateMoneyUI(); 
        }
    }

    public void AddMoney(int amount)
    {
        TotalMoney += amount;
        UpdateMoneyUI(); 
    }

    private void Start()
    {
        UpdateMoneyUI(); 
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text =  totalMoney.ToString();
        }
    }

    public void LoadData(GameData data)
    {
        totalMoney = data.totalCurrency;
        UpdateMoneyUI();
    }

    public void SaveData(ref GameData data)
    {
        data.totalCurrency = totalMoney;
    }
}
