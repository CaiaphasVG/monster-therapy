using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour {

    public static SaveLoadManager instance;
    public List<SaveSlot> saveSlots;
    public List<Button> loadSlotsButtons;
    public List<Button> newSlotsButtons;
    public SaveSlot currentSaveSlot;
    [HideInInspector]
    public bool filesAreEmpty;
    public bool isInGame;
    public Button newGame;
    public Button loadGame;
    public GameObject loadGameUI;
    public GameObject newGameUI;

    // Use this for initialization
    void Awake () {
        
        int totalTimePlayed = 0;
        
        for (int i = 0; i < saveSlots.Count; i++)
        {
            saveSlots[i].timePlayed = SaveManager.LoadSaveTime(saveSlots[i]);
            totalTimePlayed += saveSlots[i].timePlayed;
        }

        if (totalTimePlayed == 0)
            filesAreEmpty = true;
        else
            filesAreEmpty = false;


        if (SceneManager.GetActiveScene().name == "Load")
            isInGame = false;

        try
        {
            if (filesAreEmpty == true && isInGame == false)
                loadGame.interactable = false;
            else if (filesAreEmpty == false && isInGame == false)
                loadGame.interactable = true;
        } catch
        {
            Debug.Log("Main Scene Loaded");
        }


        DontDestroyOnLoad(this);

        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
                Debug.Log("Destroy");
            }
        }
        else
        {
            instance = this;
        }
    }

    #region Load Game Select

    public void LoadGame()
    {
        loadGameUI.SetActive(true);
        int i = 0;
        foreach (SaveSlot saveSlot in saveSlots)
        {
            if(saveSlot.timePlayed > 0)
            {
                loadSlotsButtons[i].gameObject.SetActive(true);
                loadSlotsButtons[i].GetComponentInChildren<Text>().text = saveSlot.name;
            }
            i++;
        }
    }

    public void CloseLoadWindow()
    {
        loadGameUI.SetActive(false);
        int i = 0; 
        foreach (SaveSlot saveSlot in saveSlots)
        {
            loadSlotsButtons[i].gameObject.SetActive(false);
        }
    }

    public void LoadGameSelect1()
    {
        currentSaveSlot = saveSlots[0];
        SceneManager.LoadScene(1);
    }

    public void LoadGameSelect2()
    {
        currentSaveSlot = saveSlots[1];
        SceneManager.LoadScene(1);
    }

    public void LoadGameSelect3()
    {
        currentSaveSlot = saveSlots[2];
        SceneManager.LoadScene(1);
    }

    public void LoadGameSelect4()
    {
        currentSaveSlot = saveSlots[3];
        SceneManager.LoadScene(1);
    }

    #endregion

    #region New Game Select

    public void NewGame()
    {
        if (filesAreEmpty == true)
        {
            SceneManager.LoadScene(1);
            currentSaveSlot = saveSlots[0];
            isInGame = true;
        }
        else
        {
            newGameUI.SetActive(true);
            int i = 0;
            foreach (SaveSlot saveSlot in saveSlots)
            {
                newSlotsButtons[i].gameObject.SetActive(true);
                if (saveSlot.timePlayed > 0)
                    newSlotsButtons[i].GetComponentInChildren<Text>().text = saveSlot.name + " Override";
                else
                    newSlotsButtons[i].GetComponentInChildren<Text>().text = saveSlot.name;
                i++;
            }
        }
    }

    public void CloseNewWindow()
    {
        newGameUI.SetActive(false);
        int i = 0;
        foreach (SaveSlot saveSlot in saveSlots)
        {
            newSlotsButtons[i].gameObject.SetActive(false);
        }
    }

    public void NewGameSelect1()
    {
        //todo erase save files
        currentSaveSlot = saveSlots[0];
        saveSlots[0].timePlayed = 0;
        SceneManager.LoadScene(1);

    }

    public void NewGameSelect2()
    {
        currentSaveSlot = saveSlots[1];
        saveSlots[1].timePlayed = 0;
        SceneManager.LoadScene(1);
    }

    public void NewGameSelect3()
    {
        currentSaveSlot = saveSlots[2];
        saveSlots[2].timePlayed = 0;
        SceneManager.LoadScene(1);
    }

    public void NewGameSelect4()
    {
        currentSaveSlot = saveSlots[3];
        saveSlots[3].timePlayed = 0;
        SceneManager.LoadScene(1);
    }

#endregion

}