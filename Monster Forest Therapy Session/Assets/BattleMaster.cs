using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMaster : MonoBehaviour {

    public int currentHappyStat;
    public int currentSadStat;
    public int currentAngerStat;
    public int currentAnxiousStat;
    public int currentDesireStat;

    public int playerDefenseStat;

    public int currentEnemyDamageMinimum;
    public int currentEnemyDamageMaximum;
    public string currentEnemyName;
    public int currentEnemyHealth;

    public int currentInfluence;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadMonstertats(NPCBattleStats _monsterStats)
    {
        currentEnemyDamageMinimum = _monsterStats.enemyDamageMinimum;
        currentEnemyDamageMaximum = _monsterStats.enemyDamageMaximum;
        currentEnemyName = _monsterStats.enemyName;
        currentEnemyHealth = _monsterStats.enemyHealth;
        currentHappyStat = _monsterStats.happyStat;
        currentSadStat = _monsterStats.sadStat;
        currentAngerStat = _monsterStats.angerStat;
        currentAnxiousStat = _monsterStats.anxiousStat;
        currentDesireStat = _monsterStats.desireStat;
    }

    public int GetInfluenceChange(int stat)
    {
        int influence = 0;
        if (stat >= 90)
            influence = 6;
        else if (stat >= 60)
            influence = 10;
        else if (stat >= 50)
            influence = 16;
        else if (stat < 50)
            influence = 30;

        return influence;
    }

    public void GetMove(int emotionalState)
    {
        switch (emotionalState)
        {
            case (1):
                playerDefenseStat += currentInfluence;
                break;
            case (2):
                currentInfluence = GetInfluenceChange(currentHappyStat);
                currentHappyStat += currentInfluence;
                currentSadStat -= (currentInfluence / 4);
                currentAngerStat -= (currentInfluence / 4);
                currentAnxiousStat -= (currentInfluence / 4);
                currentDesireStat -= (currentInfluence / 4);
                break;
            case (3):
                currentInfluence = GetInfluenceChange(currentSadStat);
                currentSadStat += currentInfluence;
                currentHappyStat -= (currentInfluence / 4);
                currentAngerStat -= (currentInfluence / 4);
                currentAnxiousStat -= (currentInfluence / 4);
                currentDesireStat -= (currentInfluence / 4);
                break;
            case (4):
                currentInfluence = GetInfluenceChange(currentAngerStat);
                currentAngerStat += currentInfluence;
                currentSadStat -= (currentInfluence / 4);
                currentHappyStat -= (currentInfluence / 4);
                currentAnxiousStat -= (currentInfluence / 4);
                currentDesireStat -= (currentInfluence / 4);
                break;
            case (5):
                currentInfluence = GetInfluenceChange(currentAnxiousStat);
                currentAnxiousStat += currentInfluence;
                currentSadStat -= (currentInfluence / 4);
                currentAngerStat -= (currentInfluence / 4);
                currentHappyStat -= (currentInfluence / 4);
                currentDesireStat -= (currentInfluence / 4);
                break;
            case (6):
                currentInfluence = GetInfluenceChange(currentDesireStat);
                currentDesireStat += currentInfluence;
                currentSadStat -= (currentInfluence / 4);
                currentAngerStat -= (currentInfluence / 4);
                currentAnxiousStat -= (currentInfluence / 4);
                currentHappyStat -= (currentInfluence / 4);
                break;
        }

        Debug.Log(currentAngerStat + currentAnxiousStat + currentDesireStat + currentSadStat + currentHappyStat);
    }

    
}
