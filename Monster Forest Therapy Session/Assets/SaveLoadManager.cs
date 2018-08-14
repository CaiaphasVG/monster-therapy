﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class SaveLoadManager : MonoBehaviour {

    public static SaveLoadManager instance;
    public List<SaveSlot> saveSlots;
    public List<Button> loadSlotsButtons;
    public List<Button> newSlotsButtons;
    public SaveSlot currentSaveSlot;
    [HideInInspector]
    public bool filesAreEmpty;
    public bool isInGame;
    public bool hasLoaded = false;
    public Button newGame;
    public Button loadGame;
    public GameObject loadGameUI;
    public GameObject newGameUI;
    public GameObject nameInputPanel;

    // Use this for initialization
    void Awake () {

        TimeCheck();

        try
        {
            nameInputPanel.GetComponentInChildren<InputField>().onEndEdit.AddListener(AcceptStringInput);
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
                loadSlotsButtons[i].transform.Find("Name").GetComponent<Text>().text = "Name: " + saveSlot.saveName;
                loadSlotsButtons[i].transform.Find("Time").GetComponent<Text>().text = "Time played (seconds): " + saveSlot.timePlayed.ToString();
                loadSlotsButtons[i].transform.Find("Emotion").GetComponent<Text>().text = "Emotion: " + SaveManager.LoadEmotion(saveSlot);

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
        hasLoaded = true;
        SceneManager.LoadScene(SaveManager.LoadScene());
    }

    public void LoadGameSelect2()
    {
        currentSaveSlot = saveSlots[1];
        hasLoaded = true;
        SceneManager.LoadScene(SaveManager.LoadScene());
    }

    public void LoadGameSelect3()
    {
        currentSaveSlot = saveSlots[2];
        hasLoaded = true;
        SceneManager.LoadScene(SaveManager.LoadScene());
    }

    public void LoadGameSelect4()
    {
        currentSaveSlot = saveSlots[3];
        hasLoaded = true;
        SceneManager.LoadScene(SaveManager.LoadScene());

    }

    #endregion

    #region New Game Select

    public void NewGame()
    {
        if (filesAreEmpty == true)
        {
            nameInputPanel.SetActive(true);
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
                {
                    newSlotsButtons[i].transform.Find("Name").GetComponent<Text>().text = "Name: " + saveSlot.name + " Override";
                    newSlotsButtons[i].transform.Find("Time").GetComponent<Text>().text = "Time played (seconds): " + saveSlot.timePlayed.ToString();
                    newSlotsButtons[i].transform.Find("Emotion").GetComponent<Text>().text = "Emotion: " + SaveManager.LoadEmotion(saveSlot);

                }
                else
                {
                    newSlotsButtons[i].transform.Find("Name").GetComponent<Text>().text = "Name: Empty";
                    newSlotsButtons[i].transform.Find("Time").GetComponent<Text>().text = "Time played (seconds): 0" + saveSlot.timePlayed.ToString();
                    newSlotsButtons[i].transform.Find("Emotion").GetComponent<Text>().text = "Emotion: None" + SaveManager.LoadEmotion(saveSlot);
                }
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
        currentSaveSlot = saveSlots[0];
        SaveManager.DeleteAll(1);
        saveSlots[0].timePlayed = 0;
        nameInputPanel.SetActive(true);
    }

    public void NewGameSelect2()
    {
        currentSaveSlot = saveSlots[1];
        SaveManager.DeleteAll(2);
        saveSlots[1].timePlayed = 0;
        nameInputPanel.SetActive(true);
    }

    public void NewGameSelect3()
    {
        currentSaveSlot = saveSlots[2];
        SaveManager.DeleteAll(3);
        saveSlots[2].timePlayed = 0;
        nameInputPanel.SetActive(true);
    }

    public void NewGameSelect4()
    {
        currentSaveSlot = saveSlots[3];
        SaveManager.DeleteAll(4);
        saveSlots[3].timePlayed = 0;
        nameInputPanel.SetActive(true);
    }

    #endregion

    public void DeleteAllSaves()
    {
        SaveManager.DeleteAll(0);
        SaveManager.DeleteAll(1);
        SaveManager.DeleteAll(2);
        SaveManager.DeleteAll(3);
        AssetDatabase.Refresh();

        //loadGame.interactable = false;
    }

    public void TimeCheck()
    {
        int totalTimePlayed = 0;

        for (int i = 0; i < saveSlots.Count; i++)
        {
            saveSlots[i].timePlayed = SaveManager.LoadSaveTime(saveSlots[i]);
            //saveSlots[i].saveName = SaveManager.LoadName(saveSlots[i]);
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
            if (filesAreEmpty == true)
                loadGame.interactable = false;
            else if (filesAreEmpty == false)
                loadGame.interactable = true;
        }
        catch
        {
            Debug.Log("Main Scene Loaded");
        }

    }

    void AcceptStringInput(string userInput)
    {
        currentSaveSlot.saveName = userInput;
        SceneManager.LoadScene(1);
        Debug.Log(currentSaveSlot);
    }

}