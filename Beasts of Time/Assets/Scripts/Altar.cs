using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private PlayerMovement player;
    private AltarUI altarUI;

    private float distanceToPlayer;
    private bool playerInRange;
    private float threshold = 5f;
    public bool altarActivated = false;
    public enum AltarState { Complete, Incomplete }
    public AltarState altarState = new AltarState();

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        altarUI = FindObjectOfType<AltarUI>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            Debug.Log("Player in altar range");

            if (altarState == AltarState.Complete)
            {
                altarUI.DisplayCompleteAltarMessage(altarActivated);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    altarActivated = !altarActivated;
                }
            }
            else if (altarState == AltarState.Incomplete)
            {
                altarUI.DisplayIncompleteAltarMessage();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    altarState = AltarState.Complete;
                }
            }
        }
        else
        {
            altarUI.HideMessage();
        }
    }

    private void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (player.isGrounded && distanceToPlayer <= threshold)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }
}
