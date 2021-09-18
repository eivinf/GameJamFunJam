using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameFileManagement : MonoBehaviour
{
    // Start is called before the first frame update


    public static void SaveFile(string currentName, float currentTimePlayed)
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;
        List < SaveData > prev = new List<SaveData>();
        if (File.Exists(destination)) {
            prev = LoadFile();
        }
            
        

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);
        
        SaveData data = new SaveData( currentName, currentTimePlayed);
        Debug.Log(currentName);
        prev.Add(data);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, prev);
        file.Close();
    }

    public static List<SaveData> LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            throw new System.Exception();
        }

        BinaryFormatter bf = new BinaryFormatter();
        List<SaveData> data = (List<SaveData>)bf.Deserialize(file);
        file.Close();
        return data;
    }
}
