using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Save", menuName = "Save/Save Slots")]
public class SaveSlot : ScriptableObject {

    public string saveName = "";
    public int timePlayed = 0;
    public int saveNumber;
}
