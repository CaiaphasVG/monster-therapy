using UnityEngine;

public class Interactable : MonoBehaviour {

    public Item item;
    [HideInInspector]
    public bool hasInteracted = false;

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + item.name);
        hasInteracted = true;

        //PickUp();
    }

    //void PickUp()
    //{
    //    Debug.Log("Picking up " + item.name);
    //    bool wasPickedUp = FindObjectOfType<Inventory>().Add(item);

    //    if (wasPickedUp)
    //        Destroy(gameObject);
    //}
}
