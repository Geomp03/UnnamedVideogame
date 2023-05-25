using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool PlayerInRange(Player player, float interactionDistance)
    {
        if (player.GroundedCheck())
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= interactionDistance)
                return true;
            else
                return false;
        }
        else
            return false;
    }
}
