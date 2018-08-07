using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour {

    public static SaveLoadManager instance;
    public GM gm;
    public bool hasLoadedScene = false;
    public List<SaveSlot> saveSlots;
    public List<Button> buttons;
    public SaveSlot currentSave;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this);

        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
        }

        for (int i = 0; i < 4; i++)
        {
            saveSlots.Add(new SaveSlot());
            saveSlots[i].fileExtension = ".sav" + i;
            buttons[i].GetComponentInChildren<Text>().text = saveSlots[i].fileExtension.ToString();
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadSave1()
    {
        SceneManager.LoadScene(SaveManager.LoadScene());
        hasLoadedScene = true;
        currentSave = saveSlots[0];
    }

    public void LoadSave2()
    {

        SceneManager.LoadScene(SaveManager.LoadScene());
        hasLoadedScene = true;
        currentSave = saveSlots[1];

    }

    public void LoadSave3()
    {
        SceneManager.LoadScene(SaveManager.LoadScene());
        hasLoadedScene = true;
        currentSave = saveSlots[2];
    }

    public void LoadSave4()
    {

        SceneManager.LoadScene(SaveManager.LoadScene());
        hasLoadedScene = true;
        currentSave = saveSlots[3];
        Debug.Log(saveSlots[3].fileExtension);
    }
}

[CreateAssetMenu(fileName = "New Save", menuName = "Item/Save")]
public class SaveSlot : ScriptableObject {
    public int number;
    public int timePLayed;
    public string fileExtension;
    public string emotion;
}