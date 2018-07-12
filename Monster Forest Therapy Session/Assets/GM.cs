using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GM : MonoBehaviour {

    public Inventory inventory;
    public PlayerMovement player;
    public float xPos;
    public float yPos;
    public float playerPositionX;
    public float playerPositionY;
    [HideInInspector]
    public int[] itemSerials;
    public List<int> itemSerialList = new List<int>();
    public List<Item> items = new List<Item>();
    public FindItem findItem;

    public void Awake()
    {
        
    }

    void Update () {
        xPos = player.transform.position.x;
        yPos = player.transform.position.y;

        itemSerials = inventory.itemSerials.ToArray();
	}

    public void Save()
    {
        SaveManager.SavePlayerPosition(this);
        SaveManager.SaveInventory(this);
    }

    public void Load()
    {
        itemSerialList.Clear();
        inventory.items.Clear();
        inventory.itemSerials.Clear();
        int[] loadedItemSerials = SaveManager.LoadInventory();

        itemSerialList.AddRange(loadedItemSerials);
        //itemSerialList = inventory.itemSerials;
        FindItem();

        float[] loadedStats = SaveManager.LoadPlayerPosition();

        playerPositionX = loadedStats[0];
        playerPositionY = loadedStats[1];

        player.UpdatePlayerPosLocal();
    }

    public void FindItem()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemSerialList[i] == findItem.items[j].itemSerial)
                    inventory.Add(findItem.items[j]);
            }
        }
    }

    [YarnCommand("change")]
    public void ChangeEmotion(string emotion)
    {
        if (emotion == "happy")
            Debug.Log("We are happy!");
        else if (emotion == "sad")
            Debug.Log("We are sad");
    }
}