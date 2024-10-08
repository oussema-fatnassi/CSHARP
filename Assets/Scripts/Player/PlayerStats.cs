using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    public string playerName;
    public int health;
    public int mana;
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
}
