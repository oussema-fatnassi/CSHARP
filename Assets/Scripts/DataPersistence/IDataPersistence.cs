using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This interface is used to define the methods that need to be implemented by the classes that will handle the data persistence of the game.
*/

public interface IDataPersistence 
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
