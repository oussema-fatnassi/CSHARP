using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public Vector3Int tilePosition;

    public TileData(Vector3Int position)
    {
        tilePosition = position;
    }
}
