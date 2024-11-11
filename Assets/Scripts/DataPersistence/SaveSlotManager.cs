using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSlotManager
{
    private static string SaveFileExtension = ".json";

    public static string GetSaveFilePath(string saveName)
{
    saveName = Path.GetFileNameWithoutExtension(saveName);
    string fileName =  saveName + SaveFileExtension;
    string fullPath = Path.Combine(Application.persistentDataPath, fileName);
    
    Debug.Log($"Generated save file path: {fullPath}");
    return fullPath;
}

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

    public static bool DoesSaveExist(string saveName)
    {
        return File.Exists(GetSaveFilePath(saveName));
    }

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