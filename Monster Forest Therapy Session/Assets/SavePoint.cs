using UnityEngine;

public class SavePoint : Interactable {

    private GM gm;

    public override void Interact()
    {
        base.Interact();
        hasInteracted = true;
        Save();
    }

    public void Save()
    {
        Debug.Log("Save game");
        gm = FindObjectOfType<GM>();
        gm.currentSavePoint = this;
        gm.saveUI.SetActive(true);
    }
}
