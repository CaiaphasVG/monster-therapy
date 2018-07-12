using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour {

    public static Inventory instance;

    public int space = 20;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of the inventory found!");
            return;
        }

        instance = this;
    }

    public List<Item> items = new List<Item>();
    public List<int> itemSerials = new List<int>();

    public bool Add (Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Not enough room");
            return false;
        }

        items.Add(item);
        itemSerials.Add(item.itemSerial);

        if(onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();

        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);
        itemSerials.Remove(item.itemSerial);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

}
