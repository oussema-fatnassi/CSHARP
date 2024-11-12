using System;
using System.IO;
using UnityEngine;

/* 
    This class is responsible for handling the loading and saving of game data to and from files.
    It is used by the DataPersistenceManager class to save and load the game data.
 */

public class FileDataHandler 
{
    private string dataDirPath;
    private string defaultFileName;
    // Constructor for the FileDataHandler class
    public FileDataHandler(string dataDirPath, string defaultFileName)
    {
        this.dataDirPath = dataDirPath;
        this.defaultFileName = defaultFileName;
    }

    // Load a specific save file by name
    public GameData Load(string fileName = null)
    {
        string targetFileName = fileName ?? defaultFileName;
        string fullPath = Path.Combine(dataDirPath, targetFileName + ".json");
        GameData loadedData = null;
        
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = File.ReadAllText(fullPath);
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                loadedData.OnAfterDeserialize();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading data from file {fullPath}:\n{e}");
            }
        }
        return loadedData;
    }

    // Save to a specific file name
    public void Save(GameData data, string fileName = null)
    {
        string targetFileName = fileName ?? defaultFileName;
        string fullPath = Path.Combine(dataDirPath, targetFileName + ".json");

        try
        {
            Debug.Log($"Attempting to save to path: {fullPath}");
            Debug.Log($"Directory exists: {Directory.Exists(Path.GetDirectoryName(fullPath))}");
            
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            data.OnBeforeSerialize();
            string dataToStore = JsonUtility.ToJson(data, true);
            
            Debug.Log($"Data being saved: {dataToStore}");
            
            File.WriteAllText(fullPath, dataToStore);
            Debug.Log($"Data successfully saved to {fullPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving data to file {fullPath}:\n{e}");
        }
    }

    // Get all available save files
    public string[] GetAllSaveFiles()
    {
        if (!Directory.Exists(dataDirPath))
        {
            Directory.CreateDirectory(dataDirPath);
        }
        string[] files = Directory.GetFiles(dataDirPath, "*.json");
        
        for (int i = 0; i < files.Length; i++)
        {
            files[i] = Path.GetFileNameWithoutExtension(files[i]);
        }

        return files;
    }
}
