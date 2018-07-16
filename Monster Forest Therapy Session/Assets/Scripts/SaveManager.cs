using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveManager {

    public static void SavePlayerPosition(GM gameMaster)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/gamemaster.pos", FileMode.Create);

        PlayerPositionData data = new PlayerPositionData(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static float[] LoadPlayerPosition()
    {
        if (File.Exists(Application.persistentDataPath + "/gamemaster.pos"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/gamemaster.pos", FileMode.Open);


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
        FileStream stream = new FileStream(Application.persistentDataPath + "/gamemaster.ivn", FileMode.Create);

        PlayerInventoryData data = new PlayerInventoryData(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int[] LoadInventory()
    {
        if (File.Exists(Application.persistentDataPath + "/gamemaster.ivn"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/gamemaster.ivn", FileMode.Open);


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
        FileStream stream = new FileStream(Application.persistentDataPath + "/gamemaster.emo", FileMode.Create);

        GameEmotion data = new GameEmotion(gameMaster);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static string LoadEmotion()
    {
        if (File.Exists(Application.persistentDataPath + "/gamemaster.emo"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/gamemaster.emo", FileMode.Open);


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