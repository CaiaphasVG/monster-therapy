using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Save", menuName = "Save/Save Slots")]
public class SaveSlot : ScriptableObject {

    public int timePlayed = 0;
    public int saveNumber;

    private void Awake()
    {

    }
}
