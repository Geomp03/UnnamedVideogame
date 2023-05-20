using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public event Action<bool> OnBoolAltarInteraction;
    public event EventHandler OnTriggerAltarInteraction;

    private PlayerMovement player;
    private AltarText altarText;

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
        altarText = GetComponent<AltarText>();

        InputManager.Instance.OnInteractAction += InputManager_OnInteractAction;
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (altarState == AltarState.Complete)          altarText.DisplayCompleteAltarMessage(altarActivated);
            else if (altarState == AltarState.Incomplete)   altarText.DisplayIncompleteAltarMessage();
        }
    }

    private void FixedUpdate()
    {
        if (!playerInRange && PlayerInRange())
        {
            playerInRange = true;
        }
        if (playerInRange && !PlayerInRange())
        {
            playerInRange = false;
            altarText.HideMessageBubble();
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
        if (player.GroundedCheck())
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= distanceThreshold)
                return true;
            else
                return false;
        }
        else 
            return false;
    }
}
