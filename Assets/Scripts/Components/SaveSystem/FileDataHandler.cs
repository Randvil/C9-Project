using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler 
{
    private string dirName = "";
    private string fileName = "";

    public FileDataHandler(string dirName, string fileName)
    {
        this.dirName = dirName;
        this.fileName = fileName;
    }

    public void Save(GameData data)
    {
        string path = Path.Combine(dirName, fileName);

        try
        {
            //create directory if it doesn't exist
            Directory.CreateDirectory(dirName);

            string dataToSave = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToSave);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Saving data to file " + path + " failed \n" + e);
        }

    }

    public GameData Load()
    {
        string path = Path.Combine(dirName, fileName);
        GameData loadedData = new();

        if (File.Exists(path))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Loading data from file " + path + " failed \n" + e);
            }
        }
        return loadedData;
    }
}
