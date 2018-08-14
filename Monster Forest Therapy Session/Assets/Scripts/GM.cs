using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using UnityEngine.UI;

public class GM : MonoBehaviour {

#region Variables 

    public PlayerMovement player;

    //Position data for saving and loading
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
    
    [Header("Inventory")]
    public Inventory inventory;
    //List of each items' unique serial
    [HideInInspector]
    public List<int> itemSerialList = new List<int>();
    [HideInInspector]
    public List<Item> items = new List<Item>();
    public FindItem findItem;
    [HideInInspector]
    public GameObject[] itemsInScene;
    [Header("Emotion Colours")]
    public Color happyColour;
    public Color sadColour;
    public Color angryColour;
    bool t = true;
    object value = true;
    public Camera cam;
    public GameObject firstAreaBarrier;
    [HideInInspector]
    public string gameEmotion = "";
    public ExampleVariableStorage variableStorage;
    public GameObject gate;
    public GameObject saveUI;
    public Text debugText;
    [HideInInspector]
    public int sceneIndex;
    public SaveLoadManager saveLoadManager;

    float timer;
    public int timePlayed;

    #endregion

    private void Awake()
    {
        saveLoadManager = SaveLoadManager.instance;
        if (saveLoadManager = null)
            saveUI.SetActive(false);

        if(saveLoadManager.hasLoaded == true && saveLoadManager.currentSaveSlot != null)
        {
            OnSceneLoaded();
        }
        timer = (float)saveLoadManager.currentSaveSlot.timePlayed;
        gameEmotion = "nuetral";
    }

    void Update () {
        timer += Time.deltaTime;
        timePlayed = (int)(timer % 60);

        xPos = player.transform.position.x;
        yPos = player.transform.position.y;

        itemSerials = inventory.itemSerials.ToArray();

	}

#region Saving and Loading

    public void Save()
    {
        SaveManager.SaveTime(this);
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SaveManager.SaveScene(this);
        SaveManager.SavePlayerPosition(this);
        SaveManager.SaveInventory(this);
        SaveManager.SaveEmotion(this);
        SaveManager.SaveName();
    }

    void OnSceneLoaded()
    {
        ChangeEmotion(SaveManager.LoadEmotion(saveLoadManager.currentSaveSlot));

        //Clears inventory and other item lists
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

        saveLoadManager.hasLoaded = false;     
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

    #endregion

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

    public void ResetEmotions()
    {
        gameEmotion = "nuetral";
        var True = new Yarn.Value(true);
        var False = new Yarn.Value(false);
        variableStorage.SetValue("$Happy", False);
        variableStorage.SetValue("$Sad", False);
        variableStorage.SetValue("$Angry", False);
    }

    public void Happy()
    {
        cam.backgroundColor = happyColour;
        firstAreaBarrier.SetActive(false);
        gameEmotion = "happy";
        ResetEmotions();
        variableStorage.SetValue("$Happy", new Yarn.Value(true));
    }

    public void Sad()
    {
        cam.backgroundColor = sadColour;
        gameEmotion = "sad";
        ResetEmotions();
        variableStorage.SetValue("$Sad", new Yarn.Value(true));
    }

    public void Angry()
    {
        cam.backgroundColor = angryColour;
        gameEmotion = "angry";
        ResetEmotions();
        variableStorage.SetValue("$Angry", new Yarn.Value(true));
    }

    public void OpenGate()
    {
        gate.SetActive(false);
    }
}