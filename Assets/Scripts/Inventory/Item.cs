using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
    This class is responsible for storing the data of the inventory items.
    It is used by the GameData class to serialize and deserialize the inventory items.
*/

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory/Consumable Item")]
public class Item : ScriptableObject
{
    public ConsumableType type;
    public int value; 
    public TileBase tile;
    public Sprite image;
    public bool stackable = true;
    public int cost;
    [TextArea]
    public string description;
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
