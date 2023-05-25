using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOrb : Interactable
{
    private Player player;

    [SerializeField] private float interactionDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Get references
        player = Player.Instance;

        // Subscribe to input manager events
        InputManager.Instance.OnInteractAction += InputManager_OnInteractAction;
    }

    private void InputManager_OnInteractAction(object sender, System.EventArgs e)
    {
        if (PlayerInRange(player, interactionDistance))
        {
            Debug.Log("Interact with time orb");
        }
    }
}
