using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;
    Animator anim;
    public int speed = 5;
    private int runningSpeed = 2;
    public float interactionRadius = 2f;
    private string npcNameTalkingTo;
    private string npcSpeech;
    public GM gm;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    // Update is called once per frame
    void Update () {

        if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            return;

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement != Vector2.zero)
        {
                anim.SetBool("isWalking", true);
                anim.SetFloat("input_x", movement.x);
                anim.SetFloat("input_y", movement.y);
        } else
           anim.SetBool("isWalking", false);

        rb.MovePosition(rb.position + movement * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckForNearbyNPC();
            CheckForNearbyInteracterble();
        }
    }

    public void CheckForNearbyNPC()
    {
        var allParticipants = new List<NPC>(FindObjectsOfType<NPC>());
        var targetNPC = allParticipants.Find(delegate (NPC p) {
            npcNameTalkingTo = p.characterName;
            npcSpeech = p.dialougeSpeech;
            FindObjectOfType<ExampleDialogueUI>().LoadNPCSprites(p.sprites);
            return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
            (p.transform.position - this.transform.position)// is in range?
            .magnitude <= interactionRadius;
        });
        if (targetNPC != null)
        {
            // Kick off the dialogue at this node.
            FindObjectOfType<DialogueRunner>().StartDialogue(targetNPC.talkToNode);
            FindObjectOfType<DialogueRunner>().AddName(npcNameTalkingTo);
            FindObjectOfType<ExampleDialogueUI>().AddDialougeSpeech(npcSpeech);
        }
    }

    public void CheckForNearbyInteracterble()
    {
        var allItems = new List<Interactable>(FindObjectsOfType<Interactable>());
        var targetItem = allItems.Find(delegate (Interactable i)
        {
            return string.IsNullOrEmpty(i.item.name) == false &&
            (i.transform.position - this.transform.position)
            .magnitude <= interactionRadius;
        });
        if (targetItem != null && targetItem.hasInteracted == false
            )
            targetItem.Interact();
    }

    public void UpdatePlayerPosLocal()
    {
        Vector2 playerPos = new Vector2(gm.playerPositionX, gm.playerPositionY);
        transform.position = playerPos;
    }
}
