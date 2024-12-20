using UnityEngine;

/*
    This class is responsible for storing the data of the player stats.
    It is used by the GameData class to serialize and deserialize the player stats.
*/

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject, IDataPersistence
{
    public string playerName;
    public int health;
    public int maxHealth;
    public int mana;
    public int maxMana;
    public int damage;
    public int defense;
    public int speed;
    public int intelligence;
    public int precision;
    public int experience;
    public int level;

    private int initialHealth;
    private int initialMana;
    private int initialDamage;
    private int initialDefense;
    private int initialSpeed;
    private int initialIntelligence;
    private int initialPrecision;
    private int initialExperience;
    private int initialLevel;
    // Method to cache the initial values of the player stats
    public void CacheInitialValues()
    {
        initialHealth = health;
        initialMana = mana;
        initialDamage = damage;
        initialDefense = defense;
        initialSpeed = speed;
        initialIntelligence = intelligence;
        initialPrecision = precision;
        initialExperience = experience;
        initialLevel = level;
    }
    // Method to reset the player stats to their initial values
    public void ResetToInitialValues()
    {
        health = initialHealth;
        mana = initialMana;
        damage = initialDamage;
        defense = initialDefense;
        speed = initialSpeed;
        intelligence = initialIntelligence;
        precision = initialPrecision;
        experience = initialExperience;
        level = initialLevel;
    }
    // Method to load the player stats from the saved data
    public void LoadData(GameData data)
    {
        if (data.playerStats.TryGetValue(playerName, out PlayerStatsData savedStats))
        {
            health = savedStats.health;
            maxHealth = savedStats.maxHealth;
            mana = savedStats.mana;
            maxMana = savedStats.maxMana;
            damage = savedStats.damage;
            defense = savedStats.defense;
            speed = savedStats.speed;
            intelligence = savedStats.intelligence;
            precision = savedStats.precision;
            experience = savedStats.experience;
            level = savedStats.level;
        }
        else
        {
            Debug.LogWarning($"No saved stats found for player: {playerName}");
        }
    }
    // Method to save the player stats to the game data
    public void SaveData(ref GameData data)
    {
        if (!data.playerStats.ContainsKey(playerName))
        {
            data.playerStats[playerName] = new PlayerStatsData();
        }

        data.playerStats[playerName] = new PlayerStatsData
        {
            health = health,
            maxHealth = maxHealth,
            mana = mana,
            maxMana = maxMana,
            damage = damage,
            defense = defense,
            speed = speed,
            intelligence = intelligence,
            precision = precision,
            experience = experience,
            level = level
        };
    }
}