using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public event Action<bool> OnBoolAltarInteraction;
    public event EventHandler OnTriggerAltarInteraction;

    private PlayerMovement player;
    private AltarUI altarUI;

    [SerializeField] private float distanceThreshold = 2f;
    private bool playerInRange;
    private bool altarActivated = false;
    public enum AltarType { TriggerAltar, BoolAltar, TimedAltar }
    public AltarType altarType = new AltarType();
    public enum AltarState { Complete, Incomplete }
    public AltarState altarState = new AltarState();

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        altarUI = FindObjectOfType<AltarUI>();

        InputManager.Instance.OnInteractAction += InputManager_OnInteractAction;
    }

    private void FixedUpdate()
    {
        // Calculate distance to player
        if ( player.GroundedCheck() )
        {
            if ( !playerInRange && PlayerInRange() )
            {
                playerInRange = true;
                if (altarState == AltarState.Complete)
                {
                    altarUI.DisplayCompleteAltarMessage(altarActivated);
                }
                else if (altarState == AltarState.Incomplete)
                {
                    altarUI.DisplayIncompleteAltarMessage();
                }
            }
            if ( playerInRange && !PlayerInRange() )
            {
                playerInRange = false;
                altarUI.HideMessage();
            }
        }
        
    }

    private void InputManager_OnInteractAction(object sender, System.EventArgs e)
    {
        if (playerInRange)
        {
            if (altarState == AltarState.Incomplete) // Add condition that check if the player currently holds an orb
            {
                altarState = AltarState.Complete;
            }
            else if (altarState == AltarState.Complete)
            {
                switch (altarType)
                {
                    case AltarType.TriggerAltar:
                        OnTriggerAltarInteraction?.Invoke(this, EventArgs.Empty);
                        break;

                    case AltarType.BoolAltar:
                        altarActivated = !altarActivated;
                        OnBoolAltarInteraction?.Invoke(altarActivated);
                        break;

                    case AltarType.TimedAltar:
                        Debug.Log("Timed altar");
                        break;
                }
            }
        }

    }

    private bool PlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= distanceThreshold)
            return true;
        else
            return false;
    }
}
