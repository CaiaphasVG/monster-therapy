using UnityEngine;

public class ItemPickup : Interactable
{

    public override void Interact()
    {
        base.Interact();
        hasInteracted = true;
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = FindObjectOfType<Inventory>().Add(item);
        FindObjectOfType<Inventory>().DisplayPickupCall(item);
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
