using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEditor;

public static class SaveManager {

    public static void SaveTime(GM gameMaster)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + ("/saveTime" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString(), FileMode.Create);

        TimePlayed data = new TimePlayed(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadSaveTime(SaveSlot saveSlot)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        if (File.Exists(Application.persistentDataPath + ("/saveTime" + saveSlot.saveNumber + ".sav").ToString()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + ("/saveTime" + saveSlot.saveNumber + ".sav").ToString(), FileMode.Open);


            TimePlayed data = bf.Deserialize(stream) as TimePlayed;

            stream.Close();
            return data.timePlayed;
        }
        else
        {
            Debug.LogError("No save file found");
            return 0;
        }
    }

    public static void SaveName()
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + ("/saveNam" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString(), FileMode.Create);

        Name data = new Name(saveLoadManager.currentSaveSlot);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static string LoadName(SaveSlot saveSlot)
    {
        if (File.Exists(Application.persistentDataPath + ("/saveNam" + saveSlot.saveNumber + ".sav").ToString()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + ("/saveNam" + saveSlot.saveNumber + ".sav").ToString(), FileMode.Open);


            Name data = bf.Deserialize(stream) as Name;

            stream.Close();
            return data.currentName;
        }
        else
        {
            Debug.LogError("No save file found");
            return "Empty";
        }
    }

    public static void SavePlayerPosition(GM gameMaster)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + ("/savePos" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString(), FileMode.Create);

        PlayerPositionData data = new PlayerPositionData(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();

        Debug.Log("Saved position to: " + saveLoadManager.currentSaveSlot.saveNumber + ".sav".ToString());
    }

    public static float[] LoadPlayerPosition()
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        if (File.Exists(Application.persistentDataPath + ("/savePos" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + ("/savePos" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString(), FileMode.Open);


            PlayerPositionData data = bf.Deserialize(stream) as PlayerPositionData;

            stream.Close();
            return data.positions;
        }
        else
        {
            Debug.LogError("No save file found");
            return new float[1];
        }
    }

    public static void SaveInventory(GM gameMaster)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + ("/saveInv" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString(), FileMode.Create);

        PlayerInventoryData data = new PlayerInventoryData(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int[] LoadInventory()
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        if (File.Exists(Application.persistentDataPath + ("/saveInv" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + ("/saveInv" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString(), FileMode.Open);


            PlayerInventoryData data = bf.Deserialize(stream) as PlayerInventoryData;

            stream.Close();
            return data.itemSerials;
        }
        else
        {
            Debug.LogError("No save file found");
            return new int[1];
        }
    }

    public static void SaveEmotion(GM gameMaster)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + ("/saveEmo" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString(), FileMode.Create);

        GameEmotion data = new GameEmotion(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static string LoadEmotion(SaveSlot saveSlot)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        if (File.Exists(Application.persistentDataPath + ("/saveEmo" + saveSlot.saveNumber + ".sav").ToString()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + ("/saveEmo" + saveSlot.saveNumber + ".sav").ToString(), FileMode.Open);


            GameEmotion data = bf.Deserialize(stream) as GameEmotion;

            stream.Close();
            return data.gameEmotion;
        }
        else
        {
            Debug.LogError("No save file found");
            return null;
        }
    }

    public static void SaveScene(GM gameMaster)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + ("/saveSce" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString(), FileMode.Create);

        GameScene data = new GameScene(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadScene()
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        if (File.Exists(Application.persistentDataPath + ("/saveSce" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + ("/saveSce" + saveLoadManager.currentSaveSlot.saveNumber + ".sav").ToString(), FileMode.Open);


            GameScene data = bf.Deserialize(stream) as GameScene;

            stream.Close();
            return data.sceneIndex;
        }
        else
        {
            Debug.LogError("No save file found");
            return 1;
        }
    }

    public static void DeleteAll(int saveNumber)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        File.Delete(Application.persistentDataPath + ("/savePos" + saveNumber + ".sav").ToString());
        File.Delete(Application.persistentDataPath + ("/saveInv" + saveNumber + ".sav").ToString());
        File.Delete(Application.persistentDataPath + ("/savePos" + saveNumber + ".sav").ToString());
        File.Delete(Application.persistentDataPath + ("/saveEmo" + saveNumber + ".sav").ToString());
        File.Delete(Application.persistentDataPath + ("/saveSce" + saveNumber + ".sav").ToString());
        File.Delete(Application.persistentDataPath + ("/saveTime" + saveNumber + ".sav").ToString());

        AssetDatabase.Refresh();
    }

}

[System.Serializable]
public class PlayerPositionData {

    [HideInInspector]
    public float[] positions;

    public PlayerPositionData(GM gamemaster)
    {
        positions = new float[2];
        positions[0] = gamemaster.xPos;
        positions[1] = gamemaster.yPos;
    }
}

[System.Serializable]
public class PlayerInventoryData {

    [HideInInspector]
    public int[] itemSerials;

    public PlayerInventoryData(GM gamemaster)
    {
        itemSerials = new int[gamemaster.itemSerials.Length];
        for (int i = 0; i < gamemaster.itemSerials.Length; i++)
        {
            itemSerials[i] = gamemaster.itemSerials[i];
        }
    }
}

[System.Serializable]
public class GameEmotion
{
    [HideInInspector]
    public string gameEmotion;

    public GameEmotion(GM gamemaster)
    {
        gameEmotion = gamemaster.gameEmotion;
    }
}

[System.Serializable]
public class GameScene
{
    [HideInInspector]
    public int sceneIndex;

    public GameScene(GM gamemaster)
    {
        sceneIndex = gamemaster.sceneIndex;
    }
}

[System.Serializable]
public class TimePlayed
{
    public int timePlayed;

    public TimePlayed(GM gamemaster)
    {
        timePlayed = gamemaster.timePlayed;
    }
}

[System.Serializable]
public class Name
{
    public string currentName;

    public Name(SaveSlot saveSlot)
    {
        currentName = saveSlot.saveName;
    }
}
