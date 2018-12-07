using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {

    public Text statsObject;
    public GameObject emotionChoicePrompt;
    private bool isChoosingOption = false;
    public int emotionOption;

    public BattleMaster BM;

    // Use this for initialization
    void Awake () {
        BM = FindObjectOfType<BattleMaster>();
	}
	
	// Update is called once per frame
	void Update () {
        statsObject.text = ("Defense: " + BM.playerDefenseStat + "\ndamage min: " + BM.currentEnemyDamageMinimum + "\ndamage max: " + BM.currentEnemyDamageMaximum + "\nname: " + BM.currentEnemyName + "\ndamage min: " + BM.currentEnemyHealth + "\n\nhappy: " + BM.currentHappyStat + "\nsad: " + BM.currentSadStat + "\nanger: " + BM.currentAngerStat + "\nanxious: " + BM.currentAnxiousStat + "\ndesire: " + BM.currentDesireStat);
        if(isChoosingOption == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                emotionOption = 1;
                TalkOptionClose();
                isChoosingOption = false;
            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                emotionOption = 2;
                TalkOptionClose();
                isChoosingOption = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                emotionOption = 3;
                TalkOptionClose();
                isChoosingOption = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                emotionOption = 4;
                TalkOptionClose();
                isChoosingOption = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                emotionOption = 5;
                TalkOptionClose();
                isChoosingOption = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                emotionOption = 6;
                TalkOptionClose();
                isChoosingOption = false;
            }
        }
	}



    public void TalkOption()
    {
        this.transform.Find("Options").gameObject.SetActive(false);
        emotionChoicePrompt.SetActive(true);
        isChoosingOption = true;
    }

    public void TalkOptionClose()
    {
        this.transform.Find("Options").gameObject.SetActive(true);
        BM.GetMove(emotionOption);
        emotionChoicePrompt.SetActive(false);
    }
}
