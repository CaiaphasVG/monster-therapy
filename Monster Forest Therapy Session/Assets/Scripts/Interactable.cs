using UnityEngine;

public class Interactable : MonoBehaviour {

    public Item item;
    [HideInInspector]
    public bool hasInteracted = false;

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + item.name);
    }
}
