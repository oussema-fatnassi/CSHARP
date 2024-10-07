using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
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
}
