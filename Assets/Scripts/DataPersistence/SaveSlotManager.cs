using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
    This class is responsible for managing the save slots of the game.
    It provides methods to get the save file path, check if a save exists, delete a save, and get all save files.
*/

public class SaveSlotManager
{
    private static string SaveFileExtension = ".json";
    // Get the full path of the save file with the specified name
    public static string GetSaveFilePath(string saveName)
    {
        saveName = Path.GetFileNameWithoutExtension(saveName);
        string fileName =  saveName + SaveFileExtension;
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);
        
        Debug.Log($"Generated save file path: {fullPath}");
        return fullPath;
    }
    // Get a list of all save files in the persistent data path
    public static List<string> GetAllSaveFiles()
    {
        List<string> saveFiles = new List<string>();
        string[] files = Directory.GetFiles(Application.persistentDataPath,  "*" + SaveFileExtension);
        
        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string saveName = fileName; 
            saveFiles.Add(saveName);
        }
        
        return saveFiles;
    }
    // Check if a save file with the specified name exists
    public static bool DoesSaveExist(string saveName)
    {
        return File.Exists(GetSaveFilePath(saveName));
    }
    // Delete the save file with the specified name
    public static void DeleteSave(string saveName)
    {
        string filePath = GetSaveFilePath(saveName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"Deleted save file: {saveName}");
        }
    }
}