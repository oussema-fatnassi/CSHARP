using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Player
{
   
    private void Awake()
    {
        PlayerName = "Dragon";
        Level = 1;
        Health = 100;
        Damage = 20;
        Defense = 10;
        Speed = 10;
        Intelligence = 5;
        Precision = 5;
        Experience = 54;
    }

    public override void defend()
    {
        throw new System.NotImplementedException();
    }

    public override void die()
    {
        throw new System.NotImplementedException();
    }

    public override void gainExperience(int experience)
    {
        throw new System.NotImplementedException();
    }

    public override void heavyAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void levelUp()
    {
        throw new System.NotImplementedException();
    }

    public override void lightAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void specialAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void takeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override void ultimateAttack()
    {
        throw new System.NotImplementedException();
    }
}
