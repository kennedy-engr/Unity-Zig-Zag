using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public Transform rayStart;
    private Rigidbody rb;
    private bool walkingDirection = true; // right = true | left = false
    private Animator anim;
    private GameManager gameManager;
    public GameObject crystalEffect;
    public AudioSource fallSound;
    private int numTrue = 0;


    void Awake()
    {
        rb = GetComponent<Rigidbody>(); //  Attached to character
        anim = GetComponent<Animator>(); // Attacehd to character
        gameManager = FindObjectOfType<GameManager>(); // only one game manager
    }

    private void FixedUpdate() // continuously moves player
    {
        if (!gameManager.gameStarted)
        {
            return;
        }
        else
            anim.SetTrigger("GameStarted");

        rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;    // tested movement rate for this game
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.gameStarted)
        {
            SwitchDirection();
        }

        RaycastHit hit;

        // for the sake of clarity the following vars are defined below

        // RayStart.position is Vector3 start of ray
        // -transform.up means the Raycast is shot down
        // out hit (see "out, in, and ref c#" on google) // allows the operation to be performed on the parameter
        // Mathf.infinity means the ray is cast downward for infinity

        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            // created so that if the raycast clips off the side of a block the game will not
            // prematurely switch to falling animation
            numTrue++;
        }
        else 
            numTrue = 0;

        if (numTrue > 100)
        {
            fallSound.Play();
            anim.SetTrigger("Falling");
            numTrue = 0;
        }

        if(transform.position.y < -10)
        {
            gameManager.EndGame();
        }

    }

    private void SwitchDirection()
    {
        walkingDirection = !walkingDirection;

        if (walkingDirection) 
        {
            transform.rotation = Quaternion.Euler(0, 45, 0); // right to left
        }
        else
            transform.rotation = Quaternion.Euler(0, -45, 0); // left to right
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "crystal")
        {
            gameManager.IncreaseScore();
            Destroy(other.gameObject);

            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(g, 2);
        }
    }
}
