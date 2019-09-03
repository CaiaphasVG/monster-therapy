using UnityEngine;
using UnityEngine.Serialization;

public class Interactable : MonoBehaviour
{

    public Item item;
    [HideInInspector]
    public bool hasInteracted = false;
    [FormerlySerializedAs("startNode")]
    public string talkToNode = "";
    public string dialougeSpeech = "";
    [Header("Optional")]
    public TextAsset flavourText;

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + item.name);
    }

}
