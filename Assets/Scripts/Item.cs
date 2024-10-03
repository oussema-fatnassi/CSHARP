using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory/Consumable Item")]
public class Item : ScriptableObject
{
    public string name;
    public ConsumableType type;
    public int value; 
    public TileBase tile;
    public Sprite image;
    public bool stackable = true;
}

public enum ConsumableType
{
    Health,
    Mana,
    Defense,
    Attack,
    Speed,
    Precision,
    Intelligence,
    Level
}
