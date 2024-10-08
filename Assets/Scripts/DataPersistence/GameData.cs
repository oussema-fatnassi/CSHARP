using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public Vector3 playerPosition;

    public GameData()
    {
        playerPosition = new Vector3(-66.3f, .75f, 0);
    }
}
