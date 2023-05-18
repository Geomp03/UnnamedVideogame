using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    //public event EventHandler OnAltarInteraction;

    [SerializeField] private InputManager inputManager;
    private PlayerMovement player;
    private AltarUI altarUI;

    private float distanceToPlayer;
    [SerializeField] private float distanceThreshold = 2f;
    private bool playerInRange;
    private bool altarActivated = false;
    private bool interact = false;
    public enum AltarState { Complete, Incomplete }
    public AltarState altarState = new AltarState();

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        altarUI = FindObjectOfType<AltarUI>();

        inputManager.OnInteractAction += InputManager_OnInteractAction;
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (altarState == AltarState.Complete)
            {
                altarUI.DisplayCompleteAltarMessage(altarActivated);

                if (interact)
                {
                    //OnAltarInteraction?.Invoke(this, altarActivated);
                    altarActivated = !altarActivated;
                    interact = false;
                }
            }
            else if (altarState == AltarState.Incomplete)
            {
                altarUI.DisplayIncompleteAltarMessage();

                if (interact)
                {
                    altarState = AltarState.Complete;
                    interact = false;
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
        // Calculate distance to player
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (player.isGrounded && distanceToPlayer <= distanceThreshold)
            playerInRange = true;
        else
            playerInRange = false;
    }

    private void InputManager_OnInteractAction(object sender, System.EventArgs e)
    {
        interact = true;
    }
}
