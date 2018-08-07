using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Inventory : MonoBehaviour {

    public GameObject itemPickupUI;
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
    [HideInInspector]
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
        StartCoroutine(DisplayPickup(item));

        if (onItemChangedCallBack != null)
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

    IEnumerator DisplayPickup(Item item)
    {
        itemPickupUI.GetComponentInChildren<Text>().text = "You got: " + item.name + "!";
        itemPickupUI.GetComponentInChildren<Image>().sprite = item.icon;
        itemPickupUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        itemPickupUI.SetActive(false);
    }

}
