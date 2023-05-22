using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;
    }

    public bool PlayerInRange(float distanceThreshold)
    {
        if (player.GroundedCheck() )
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
