using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

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

    public static void SavePlayerPosition(GM gameMaster)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/pos.pos", FileMode.Create);

        PlayerPositionData data = new PlayerPositionData(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static float[] LoadPlayerPosition()
    {

        if (File.Exists(Application.persistentDataPath + "/pos.pos"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/pos.pos", FileMode.Open);


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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/inv.inv", FileMode.Create);

        PlayerInventoryData data = new PlayerInventoryData(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int[] LoadInventory()
    {

        if (File.Exists(Application.persistentDataPath + "/inv.inv"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/inv.inv", FileMode.Open);


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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/emo.emo", FileMode.Create);

        GameEmotion data = new GameEmotion(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static string LoadEmotion()
    {
        if (File.Exists(Application.persistentDataPath + "/emo.emo"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/emo.emo", FileMode.Open);


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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/sce.sce", FileMode.Create);

        GameScene data = new GameScene(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadScene()
    {
        Debug.Log("/sce" + ".sav0");
        if (File.Exists(Application.persistentDataPath + ("/sce.sce").ToString()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/sce.sce", FileMode.Open);


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

    public static void HasLoadedSceneCheck(GM gameMaster)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/chk.chk", FileMode.Create);

        CheckLoadedScene data = new CheckLoadedScene(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static bool HasLoadedSceneReturn()
    {
        if (File.Exists(Application.persistentDataPath + "/chk.chk"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/chk.chk", FileMode.Open);


            CheckLoadedScene data = bf.Deserialize(stream) as CheckLoadedScene;

            stream.Close();
            return data.loaded;
        }
        else
        {
            Debug.LogError("No save file found");
            return false;
        }
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
public class CheckLoadedScene
{
    public bool loaded;

    public CheckLoadedScene(GM gamemaster)
    {
        loaded = gamemaster.hasLoaded;
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
