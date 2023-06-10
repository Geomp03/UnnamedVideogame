using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : Interactable
{
    public event Action<bool> OnBoolAltarInteraction;
    public event EventHandler OnTriggerAltarInteraction;

    private Player player;
    private PlayerOrbInteractions playerOrbInteractions;
    private AltarUI altarUI;

    [Header("Base Altar Settings")]
    [SerializeField] private float interactionDistance = 2f;
    public enum AltarState { Complete, Incomplete }
    public AltarState altarState = new AltarState();
    private enum AltarType { TriggerAltar, BoolAltar, TimedAltar }
    [SerializeField] private AltarType altarType = new AltarType();

    [Header("Altar Specific Settings")]
    [SerializeField] private float timedAltarDuration = 3f;
    private float interactionCooldown = 0f;
    private float nextActionTime;

    private bool playerInRange;
    private bool altarActivated = false;

    private TimeOrbSO timeOrbSO;

    // Start is called before the first frame update
    void Start()
    {
        // Get references
        player = Player.Instance;
        playerOrbInteractions = player.GetComponent<PlayerOrbInteractions>();
        altarUI = GetComponentInChildren<AltarUI>();

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
            if (altarState == AltarState.Complete)          altarUI.DisplayCompleteAltarMessage(altarActivated);
            else if (altarState == AltarState.Incomplete)   altarUI.DisplayIncompleteAltarMessage();
        }
    }

    private void FixedUpdate()
    {
        // Equivalent of OnTriggerEnter/OnCollisionEnter but just with distance calculation
        if (!playerInRange && PlayerInRange(player, interactionDistance))
        {
            playerInRange = true;
        }

        // Equivalent of OnTriggerExit/OnCollisionExit but just with distance calculation
        if (playerInRange && !PlayerInRange(player, interactionDistance))
        {
            playerInRange = false;
            altarUI.HideMessageBubble();
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
                if (altarState == AltarState.Incomplete && playerOrbInteractions.HoldingTimeOrb())
                {
                    altarState = AltarState.Complete;
                    timeOrbSO = playerOrbInteractions.GetTimeOrbSO();
                    playerOrbInteractions.ClearTimeOrbSO();
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
