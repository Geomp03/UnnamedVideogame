using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    public static PlayerClone Instance { get; private set; }

    private Player player;

    private bool playerInThePresent = true;

    private void Awake()
    {
        // Create PlayerClone singleton instance
        if (Instance != null)
        {
            Debug.LogError("There is more than one PlayerClone instance");
        }
        Instance = this;
    }

    void Start()
    {
        player = Player.Instance;
    }

    void Update()
    {
        FollowPlayerMovement(playerInThePresent);
    }

    private void FollowPlayerMovement(bool playerInThePresent)
    {
        Vector2 playerClonePositionOffset = new Vector2(0f, 0f);
        Vector2 playerPosition = player.transform.position;

        if (playerInThePresent)
        {
            playerClonePositionOffset = new Vector2(0f, -50f);
        }
        else
        {
            playerClonePositionOffset = new Vector2(0f, +50f);
        }

        transform.position = playerPosition + playerClonePositionOffset;
    }

    public void ChangePlayerTimeline()
    {
        playerInThePresent = !playerInThePresent;
    }
}
