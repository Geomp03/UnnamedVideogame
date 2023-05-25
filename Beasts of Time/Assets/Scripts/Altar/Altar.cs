using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : Interactable
{
    public event Action<bool> OnBoolAltarInteraction;
    public event EventHandler OnTriggerAltarInteraction;

    private Player player;
    private AltarText altarText;

    [Header("Base Altar Settings")]
    [SerializeField] private float distanceThreshold = 2f;
    public enum AltarState { Complete, Incomplete }
    public AltarState altarState = new AltarState();
    public enum AltarType { TriggerAltar, BoolAltar, TimedAltar }
    public AltarType altarType = new AltarType();

    [Header("Altar Specific Settings")]
    [SerializeField] private float timedAltarDuration = 3f;
    private float interactionCooldown = 0f;
    private float nextActionTime;

    private bool playerInRange;
    private bool altarActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get references
        player = Player.Instance;
        altarText = GetComponent<AltarText>();

        // Subscribe to input manager events
        InputManager.Instance.OnInteractAction += InputManager_OnInteractAction;

        // Set an interaction cooldown if using the timed altar
        if (altarType == AltarType.TimedAltar)
        {
            interactionCooldown = timedAltarDuration;
            Debug.Log("Set interaction cooldown for " + this + " to: " + interactionCooldown);
        }
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
        // Equivalent of OnTriggerEnter/OnCollisionEnter but just with distance calculation
        // (can simplify this if needed...
        if (!playerInRange && PlayerInRange(player, distanceThreshold))
        {
            playerInRange = true;
        }

        // Equivalent of OnTriggerExit/OnCollisionExit but just with distance calculation
        if (playerInRange && !PlayerInRange(player, distanceThreshold))
        {
            playerInRange = false;
            altarText.HideMessageBubble();
        }
    }

    private void InputManager_OnInteractAction(object sender, System.EventArgs e)
    {
        if (Time.time > nextActionTime)
        {
            // Interaction cooldown check logic
            nextActionTime = Time.time + interactionCooldown;

            // Interaction logic
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
                            StartCoroutine(TimedAltarActivation(timedAltarDuration));
                            break;
                    }
                }
            }
        }

    }

    private IEnumerator TimedAltarActivation(float duration)
    {
        altarActivated = true;
        OnBoolAltarInteraction?.Invoke(altarActivated);
        yield return new WaitForSeconds(duration);
        altarActivated = false;
        OnBoolAltarInteraction?.Invoke(altarActivated);
    }
}
