using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveManager {


    public static void SavePlayerPosition(GM gameMaster)
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/pos" + saveLoadManager.currentSave.fileExtension, FileMode.Create);

        PlayerPositionData data = new PlayerPositionData(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static float[] LoadPlayerPosition()
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        if (File.Exists(Application.persistentDataPath + "/pos" + saveLoadManager.currentSave.fileExtension))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/pos" + saveLoadManager.currentSave.fileExtension, FileMode.Open);


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
        FileStream stream = new FileStream(Application.persistentDataPath + "/inv" + saveLoadManager.currentSave.fileExtension, FileMode.Create);

        PlayerInventoryData data = new PlayerInventoryData(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int[] LoadInventory()
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        if (File.Exists(Application.persistentDataPath + "/inv" + saveLoadManager.currentSave.fileExtension))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/inv" + saveLoadManager.currentSave.fileExtension, FileMode.Open);


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
        FileStream stream = new FileStream(Application.persistentDataPath + "/emo" + saveLoadManager.currentSave.fileExtension, FileMode.Create);

        GameEmotion data = new GameEmotion(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static string LoadEmotion()
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        if (File.Exists(Application.persistentDataPath + "/emo" + saveLoadManager.currentSave.fileExtension))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/emo" + saveLoadManager.currentSave.fileExtension, FileMode.Open);


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
        FileStream stream = new FileStream(Application.persistentDataPath + "/sce" + saveLoadManager.currentSave.fileExtension, FileMode.Create);

        GameScene data = new GameScene(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadScene()
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;
        Debug.Log("/sce" + ".sav0");
        if (File.Exists(Application.persistentDataPath + ("/sce" + saveLoadManager.currentSave.fileExtension).ToString()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + ("/sce" + saveLoadManager.currentSave.fileExtension).ToString(), FileMode.Open);


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
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/chk" + saveLoadManager.currentSave.fileExtension, FileMode.Create);

        CheckLoadedScene data = new CheckLoadedScene(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static bool HasLoadedSceneReturn()
    {
        SaveLoadManager saveLoadManager = SaveLoadManager.instance;

        if (File.Exists(Application.persistentDataPath + "/chk" + saveLoadManager.currentSave.fileExtension))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/chk" + saveLoadManager.currentSave.fileExtension, FileMode.Open);


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
