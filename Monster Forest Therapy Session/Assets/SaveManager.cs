using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveManager {

#region Save Player Position

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

#endregion

