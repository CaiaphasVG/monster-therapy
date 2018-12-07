using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveManager {

    public static void SavePlayer(GM player)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + ("/player" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString();
        FileStream stream = new FileStream(filePath, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(SaveSlot saveSlot)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;
        string filePath = Application.persistentDataPath + ("/player" + saveSlot.saveNumber + ".sav").ToString();
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);
            
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save file not found in " + filePath);
            return null;
        }

    }

    public static void DeleteAllSaves(int saveNumber)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        File.Delete(Application.persistentDataPath + ("/player" + saveNumber + ".sav").ToString());

    }
}