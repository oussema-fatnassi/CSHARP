using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected string playerName;
    [SerializeField] protected int level;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected int defense;
    [SerializeField] protected int speed;
    [SerializeField] protected int intelligence;
    [SerializeField] protected int precision;
    [SerializeField] protected int experience;

    public string PlayerName { get => playerName; set => playerName = value; }
    public int Level { get => level; set => level = value; }
    public int Health { get => health; set => health = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Speed { get => speed; set => speed = value; }
    public int Intelligence { get => intelligence; set => intelligence = value; }
    public int Precision { get => precision; set => precision = value; }
    public int Experience { get => experience; set => experience = value; }

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