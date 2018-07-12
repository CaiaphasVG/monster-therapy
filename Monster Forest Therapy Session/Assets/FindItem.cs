using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindItem : MonoBehaviour {

    [HideInInspector]
    public List<Item> items = new List<Item>();
    public GameObject[] itemsInScene;

    public GameObject[] FindItemsInScene(GameObject[] itemsInScene)
    {
        itemsInScene = GameObject.FindGameObjectsWithTag("Item");
        return itemsInScene;
    }
}
