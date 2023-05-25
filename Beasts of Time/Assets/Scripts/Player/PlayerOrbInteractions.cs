using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrbInteractions : MonoBehaviour
{
    private TimeOrbSO timeOrbSO;

    public void SetTimeOrbSO(TimeOrbSO timeOrbSO)
    {
        this.timeOrbSO = timeOrbSO;
    }

    public TimeOrbSO GetTimeOrbSO()
    {
        return timeOrbSO;
    }

    public bool HoldingTimeOrb()
    {
        if (timeOrbSO != null)
            return true;
        else
            return false;
    }

    public void ClearTimeOrbSO()
    {
        timeOrbSO = null;
    }
}
