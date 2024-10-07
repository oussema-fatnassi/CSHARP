using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public int health;
    public int damage;
    public int speed;
    public int intelligence;
    public int precision;
    public int experienceToGive;
    public int level;
}
