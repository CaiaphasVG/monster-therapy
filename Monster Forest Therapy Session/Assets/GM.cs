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
        float[] loadedStats = SaveManager.LoadPlayerPosition();

        playerPositionX = loadedStats[0];
        playerPositionY = loadedStats[1];

        player.UpdatePlayerPosLocal();

        int[] loadedItemSerials = SaveManager.LoadInventory();

        itemSerialList.AddRange(loadedItemSerials);
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