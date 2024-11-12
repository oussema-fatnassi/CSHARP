using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script is the base class for all enemies in the game.
    It contains the stats of the enemy and a method to initialize the enemy with new stats.
*/

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyStats stats;

    // Properties
    public string EnemyName { get => stats.enemyName; }
    public int Health { get => stats.health; set => stats.health = value; }
    public int Damage { get => stats.damage; set => stats.damage = value; }
    public int Speed { get => stats.speed; set => stats.speed = value; }
    public int Intelligence { get => stats.intelligence; set => stats.intelligence = value; }
    public int Precision { get => stats.precision; set => stats.precision = value; }
    public int ExperienceToGive { get => stats.experienceToGive; set => stats.experienceToGive = value; }
    public int Level { get => stats.level; set => stats.level = value; }

    // Initialize the enemy with new stats
    public void InitializeEnemy(EnemyStats newStats)
    {
        stats = newStats;
    }
}
