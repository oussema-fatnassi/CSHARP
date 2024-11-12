using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/*
    This class is responsible for testing the CurrencyManager class.
    It tests the AddMoney, SpendMoney, SaveData, and LoadData methods of the CurrencyManager class.
*/

public class CurrencyManagerTest
{
    private GameObject currencyManagerObject;
    private CurrencyManager currencyManager;
    // Set up the test environment by creating a new CurrencyManager object and setting the moneyText field to null.
    [SetUp]
    public void Setup()
    {
        currencyManagerObject = new GameObject("CurrencyManager");
        currencyManager = currencyManagerObject.AddComponent<CurrencyManager>();

        currencyManager.GetType()
            .GetField("moneyText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(currencyManager, null);
    }
    // Tear down the test environment by destroying the CurrencyManager object.
    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(currencyManagerObject);
    }
    // Test the AddMoney method of the CurrencyManager class.
    [UnityTest]
    public IEnumerator AddMoney_UpdatesTotalMoneyCorrectly()
    {
        currencyManager.TotalMoney = 0;

        currencyManager.AddMoney(100);

        yield return null;

        Assert.AreEqual(100, currencyManager.TotalMoney, "Total money did not update correctly after adding.");
    }
    // Test the SpendMoney method of the CurrencyManager class.
    [UnityTest]
    public IEnumerator SpendMoney_UpdatesTotalMoneyCorrectly()
    {
        currencyManager.TotalMoney = 100;

        currencyManager.SpendMoney(50);

        yield return null;

        Assert.AreEqual(50, currencyManager.TotalMoney, "Total money did not update correctly after spending.");
    }
    // Test the SpendMoney method of the CurrencyManager class when the player has insufficient funds.
    [UnityTest]
    public IEnumerator SpendMoney_WithInsufficientFunds_DoesNotUpdateTotalMoney()
    {
        currencyManager.TotalMoney = 50;

        currencyManager.SpendMoney(100);

        yield return null;

        Assert.AreEqual(50, currencyManager.TotalMoney, "Total money should not change when spending exceeds available funds.");
    }
    // Test the SaveData and LoadData methods of the CurrencyManager class.
    [UnityTest]
    public IEnumerator SaveAndLoadMoney_DataPersistsCorrectly()
    {
        currencyManager.TotalMoney = 200;
        GameData gameData = new GameData();
        currencyManager.SaveData(ref gameData);

        currencyManager.TotalMoney = 0;
        currencyManager.LoadData(gameData);

        yield return null;

        Assert.AreEqual(200, currencyManager.TotalMoney, "Total money did not load correctly from saved data.");
    }
}
