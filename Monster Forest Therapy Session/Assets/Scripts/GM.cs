using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GM : MonoBehaviour {

#region Variables 

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
    public GameObject firstAreaBarrier;
    public string gameEmotion = "";
    public ExampleVariableStorage variableStorage;
    public GameObject gate;
    public GameObject saveUI;
    public int sceneIndex;
    public bool hasLoaded;
    public SaveLoadManager saveLoadManager;

    #endregion

    private void Awake()
    {
        saveLoadManager = SaveLoadManager.instance;
        saveLoadManager.gm = this;
        if(saveLoadManager.hasLoadedScene == true)
        {
            OnSceneLoaded();
        } else if (saveLoadManager.hasLoadedScene == false)
        {
            if (SceneManager.GetActiveScene().buildIndex == SaveManager.LoadScene())
            {
                if (SaveManager.HasLoadedSceneReturn() == true)
                    OnSceneLoaded();
            }
        } 
    }

    void Update () {
        xPos = player.transform.position.x;
        yPos = player.transform.position.y;

        itemSerials = inventory.itemSerials.ToArray();
	}

#region Saving and Loading

    public void Save()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SaveManager.SaveScene(this);
        SaveManager.SavePlayerPosition(this);
        SaveManager.SaveInventory(this);
        SaveManager.SaveEmotion(this);
        saveUI.SetActive(false);
    }

    public void Load()
    {
        hasLoaded = true;
        SaveManager.HasLoadedSceneCheck(this);
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SaveManager.LoadScene());

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }

    void OnSceneLoaded()
    {
        ChangeEmotion(SaveManager.LoadEmotion());
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
        saveUI.SetActive(false);
        hasLoaded = false;
        saveLoadManager.hasLoadedScene = false;
        SaveManager.HasLoadedSceneCheck(this);
        
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
        var True = new Yarn.Value(true);
        var False = new Yarn.Value(false);
        variableStorage.SetValue("$Happy", False);
        variableStorage.SetValue("$Sad", False);
        variableStorage.SetValue("$Angry", False);
    }

    public void Happy()
    {
        cam.backgroundColor = Color.yellow;
        firstAreaBarrier.SetActive(false);
        gameEmotion = "happy";
        ResetEmotions();
        variableStorage.SetValue("$Happy", new Yarn.Value(true));
    }

    public void Sad()
    {
        cam.backgroundColor = Color.blue;
        gameEmotion = "sad";
        ResetEmotions();
        variableStorage.SetValue("$Sad", new Yarn.Value(true));
    }

    public void Angry()
    {
        cam.backgroundColor = Color.red;
        gameEmotion = "angry";
        ResetEmotions();
        variableStorage.SetValue("$Angry", new Yarn.Value(true));
    }

    public void OpenGate()
    {
        gate.SetActive(false);
    }
}