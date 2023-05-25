using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOrb : Interactable
{
    private Player player;
    private PlayerOrbInteractions playerOrbInteractions;

    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private TimeOrbSO timeOrbSO;

    // Start is called before the first frame update
    void Start()
    {
        // Get references
        player = Player.Instance;
        playerOrbInteractions = player.GetComponent<PlayerOrbInteractions>();

        // Subscribe to input manager events
        InputManager.Instance.OnInteractAction += InputManager_OnInteractAction;
    }

    private void InputManager_OnInteractAction(object sender, System.EventArgs e)
    {
        if (PlayerInRange(player, interactionDistance))
        {
            TimeOrbSO playerCurrentTimeOrbSO = playerOrbInteractions.GetTimeOrbSO();
            if (playerCurrentTimeOrbSO == null)
            {
                // Player not holding any time orbs
                playerOrbInteractions.SetTimeOrbSO(timeOrbSO);

                // Unsubscribe from events
                InputManager.Instance.OnInteractAction -= InputManager_OnInteractAction;

                // Destroy object
                Destroy(gameObject);
            }
            else
            {
                // Player already holding a time orb
                Debug.Log("Player already holding a time orb");
            }

        }
    }
}
