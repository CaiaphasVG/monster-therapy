using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using Yarn.Unity.Example;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    public int sceneIndex;
    public int timePlayed;
    public string playerName;
    public string emotion;
    public int[] itemSerials;
    public float[] playerPosition;

    public PlayerData (GM player)
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        timePlayed = player.timePlayed;
        playerName = player.saveLoadManager.currentSaveSlot.saveName;
        emotion = player.gameEmotion;
        itemSerials = player.itemSerials;

        playerPosition = new float[2];
        playerPosition[0] = player.xPos;
        playerPosition[1] = player.yPos;

    }
}

public class GM : MonoBehaviour {

#region Variables 

    public PlayerMovement player;
    public NPCBattleStats enemyStats;
    public string npcNameTalkingTo;
    public string npcSpeech;

    //Position data for saving and loading
    public float xPos;
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
    [HideInInspector]
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
    public GameObject currentItem;
    public Text debugText;
    [HideInInspector]
    public int sceneIndex;
    public SaveLoadManager saveLoadManager;

    [HideInInspector]
    public float timer = 0;
    public int timePlayed;

    #endregion

    public SavePoint currentSavePoint;

    private void Awake()
    {
        saveLoadManager = SaveLoadManager.instance;
        gameEmotion = "nuetral";
        if (saveLoadManager.hasLoaded == true)
        {
            if(saveLoadManager.currentSaveSlot != null)
            {
                OnSceneLoaded();
            }
        }
    }

    void Update () {
        timer += Time.deltaTime;
        timePlayed = (int)(timer % 60);

        xPos = player.transform.position.x;
        yPos = player.transform.position.y;

        itemSerials = inventory.itemSerials.ToArray();
	}

    public void CheckForNearbyNPC()
    {
        var allParticipants = new List<NPC>(FindObjectsOfType<NPC>());
        var targetNPC = allParticipants.Find(delegate (NPC p) {
            npcNameTalkingTo = p.characterName;
            npcSpeech = p.dialougeSpeech;
            FindObjectOfType<DialogueUI>().LoadNPCSprites(p.sprites);
            return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
            (p.transform.position - player.transform.position)// is in range?
            .magnitude <= player.interactionRadius;
        });
        if (targetNPC != null)
        {
            // Kick off the dialogue at this node.
            FindObjectOfType<DialogueRunner>().StartDialogue(targetNPC.talkToNode);
            FindObjectOfType<DialogueRunner>().AddName(npcNameTalkingTo);
            FindObjectOfType<DialogueUI>().AddDialougeSpeech(npcSpeech);
            FindObjectOfType<DialogueUIBehaviour>().npcTalkingTo = targetNPC;
            if (targetNPC.isEnemy == true)
            {
                enemyStats = targetNPC.gameObject.GetComponent<NPCBattleStats>();
            }
        }
        else
            Debug.LogError("targetNPC is null");
    }

    //public void CheckForNearbyInteracterble()
    //{
    //    var allItems = new List<Interactable>(FindObjectsOfType<Interactable>());
    //    var targetItem = allItems.Find(delegate (Interactable i)
    //    {
    //        return string.IsNullOrEmpty(i.item.name) == false &&
    //        (i.transform.position - player.transform.position)
    //        .magnitude <= player.interactionRadius;
    //    });
    //    if (targetItem != null && targetItem.hasInteracted == false)
    //        targetItem.Interact();
    //        currentItem = targetItem.gameObject;
    //        FindObjectOfType<DialogueRunner>().StartDialogue(targetItem.talkToNode);
    //        FindObjectOfType<DialogueUI>().displayImage.gameObject.SetActive(false);
    //        FindObjectOfType<DialogueRunner>().AddName("");
    //        FindObjectOfType<DialogueUI>().AddDialougeSpeech("rupert");
    //}

    #region Saving and Loading

    public void Save()
    {
        Debug.Log(timePlayed);
        SaveManager.SavePlayer(this);
        //SaveManager.SaveTime(this);
        //sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SaveManager.SaveScene(this);
        //SaveManager.SavePlayerPosition(this);
        //SaveManager.SaveInventory(this);
        //SaveManager.SaveEmotion(this);
        //SaveManager.SaveName();
        saveUI.SetActive(false);
        currentSavePoint.hasInteracted = false;
        currentSavePoint = null;
    }

    void OnSceneLoaded()
    {
        PlayerData data = SaveManager.LoadPlayer(saveLoadManager.currentSaveSlot);

        ChangeEmotion(data.emotion);

        //Clears inventory and other item lists
        itemSerialList.Clear();
        inventory.items.Clear();
        inventory.itemSerials.Clear();
        itemSerialList.AddRange(data.itemSerials);
        FindItem();
        itemsInScene = findItem.FindItemsInScene(itemsInScene);
        ClearItems();

        player.UpdatePlayerPosLocal(data.playerPosition[0], data.playerPosition[1]);

        saveLoadManager.hasLoaded = false;
        timer = (float)data.timePlayed;

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
        ResetEmotions();
        gameEmotion = "happy";
        variableStorage.SetValue("$Happy", new Yarn.Value(true));
    }

    public void Sad()
    {
        cam.backgroundColor = sadColour;
        ResetEmotions();
        gameEmotion = "sad";
        variableStorage.SetValue("$Sad", new Yarn.Value(true));
    }

    public void Angry()
    {
        cam.backgroundColor = angryColour;
        ResetEmotions();
        gameEmotion = "angry";
        variableStorage.SetValue("$Angry", new Yarn.Value(true));
    }

    public void OpenGate()
    {
        gate.SetActive(false);
    }
}