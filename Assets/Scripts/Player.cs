using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected PlayerStats stats;

    public string PlayerName { get => stats.playerName; }
    public int Level { get => stats.level; set => stats.level = value; }
    public int Health { get => stats.health; set => stats.health = value; }
    public int MaxHealth { get => stats.maxHealth; set => stats.maxHealth = value; }
    public int Mana { get => stats.mana; set => stats.mana = value; }
    public int MaxMana { get => stats.maxMana; set => stats.maxMana = value; }
    public int Damage { get => stats.damage; set => stats.damage = value; }
    public int Defense { get => stats.defense; set => stats.defense = value; }
    public int Speed { get => stats.speed; set => stats.speed = value; }
    public int Intelligence { get => stats.intelligence; set => stats.intelligence = value; }
    public int Precision { get => stats.precision; set => stats.precision = value; }
    public int Experience { get => stats.experience; set => stats.experience = value; }

    public void InitializePlayer(PlayerStats newStats)
    {
        stats = newStats;
    }

    public abstract void lightAttack();
    public abstract void heavyAttack();
    public abstract void specialAttack();
    public abstract void ultimateAttack();
    public abstract void takeDamage(int damage);
    public abstract void levelUp();
    public abstract void gainExperience(int experience);
    public abstract void die();
    public abstract void defend();
}