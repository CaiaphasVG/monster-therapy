using UnityEngine;

public class SavePoint : Interactable {

    public GameObject saveUI;

    public override void Interact()
    {
        Save();

        base.Interact();

        //hasInteracted = false;
    }

    public void Save()
    {
        saveUI.SetActive(true);
    }
}
