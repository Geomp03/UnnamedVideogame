using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShift : MonoBehaviour
{
    private Player player;
    private PlayerClone playerClone;
    [SerializeField] private Altar altar;

    void Start()
    {
        player = Player.Instance;
        playerClone = PlayerClone.Instance;
        altar.OnTriggerAltarInteraction += Altar_OnTriggerAltarInteraction;
    }

    private void Altar_OnTriggerAltarInteraction(object sender, System.EventArgs e)
    {
        SwapPlayerAndPlayerClonePosition();
    }

    private void SwapPlayerAndPlayerClonePosition()
    {
        player.transform.position = playerClone.transform.position;
        playerClone.ChangePlayerTimeline();
    }
}
