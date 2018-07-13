﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GM : MonoBehaviour {

    public Inventory inventory;
    public PlayerMovement player;
    [HideInInspector]
    public float xPos;
    [HideInInspector]
    public float yPos;
    [HideInInspector]
    public float playerPositionX;
    [HideInInspector]
    public float playerPositionY;
    [HideInInspector]
    public int[] itemSerials;
    [HideInInspector]
    public List<int> itemSerialList = new List<int>();
    [HideInInspector]
    public List<Item> items = new List<Item>();
    public FindItem findItem;
    public GameObject[] itemsInScene;
    bool t = true;
    object value = true;
    public Camera cam;
    Color happyColour = new Vector4(253, 253, 128, 255);
    Color sadColour = new Vector4(70, 77, 148, 255);
    Color angryColour = new Vector4(255, 64, 64, 255);


    public ExampleVariableStorage variableStorage; 

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
        FindItem();
        itemsInScene = findItem.FindItemsInScene(itemsInScene);
        ClearItems();

        float[] loadedStats = SaveManager.LoadPlayerPosition();

        playerPositionX = loadedStats[0];
        playerPositionY = loadedStats[1];

        player.UpdatePlayerPosLocal();
    }

    public void FindItem()
    {
        for (int i = 0; i < itemSerialList.Count; i++)
        {
            for (int j = 0; j < findItem.items.Count; j++)
            {
                if (itemSerialList[i] == findItem.items[j].itemSerial)
                    inventory.Add(findItem.items[j]);
            }
        }
    }

    public void ClearItems()
    {
        itemsInScene = findItem.FindItemsInScene(itemsInScene);
        for (int i = 0; i < itemsInScene.Length; i++)
        {
            for (int j = 0; j < inventory.items.Count; j++)
            {
                if (itemsInScene[i].GetComponent<Interactable>().item.name == inventory.items[j].name)
                    Destroy(itemsInScene[i]);
            }
        }
    }

    [YarnCommand("change")]
    public void ChangeEmotion(string emotion)
    {
        if (emotion == "happy")
            Happy();
        else if (emotion == "sad")
            Sad();
        else if (emotion == "angry")
            Angry();
    }

    public void SetVariable()
    {
        var v = new Yarn.Value(true);
        variableStorage.SetValue("$TestTrue", v);
    }

    public void Happy()
    {
        cam.backgroundColor = Color.yellow;
    }

    public void Sad()
    {
        cam.backgroundColor = sadColour;
    }

    public void Angry()
    {
        cam.backgroundColor = angryColour;
    }
}