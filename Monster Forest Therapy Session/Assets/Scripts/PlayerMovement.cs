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
    public float interactionRadius = 0.5f;
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

        if (Input.GetKey(KeyCode.LeftShift))
            speed = 7;
        else
            speed = 5;

        rb.MovePosition(rb.position + movement * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gm.CheckForNearbyNPC();
            //gm.CheckForNearbyInteracterble();
        }
    }

    public void UpdatePlayerPosLocal(float xPos, float yPos)
    {
        Vector2 playerPos = new Vector2(xPos, yPos);
        transform.position = playerPos;
    }
}
