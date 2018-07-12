using System.Collections;
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
        Debug.Log(itemSerialList.Count);
        //itemSerialList = inventory.itemSerials;
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
            Debug.Log("We are happy!");
        else if (emotion == "sad")
            Debug.Log("We are sad");
    }

    public void SetVariable()
    {
        var v = new Yarn.Value(true);
        variableStorage.SetValue("$TestTrue", v);
    }
}