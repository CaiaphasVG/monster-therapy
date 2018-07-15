using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public int itemSerial = 0;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

}
