using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSummonMaster : MonoBehaviour
{
    [SerializeField] private Altar altar;
    [SerializeField] private Transform summoningPoint;
    [SerializeField] private int TimeSummonID;
    private TimeSummonSlave timeSummonSlave;

    private void Start()
    {
        timeSummonSlave = FindMatchingTimeSummonSlave(TimeSummonID);

        altar.OnTriggerAltarInteraction += Altar_OnTriggerAltarInteraction;
    }

    private void Altar_OnTriggerAltarInteraction(object sender, System.EventArgs e)
    {        
        if (timeSummonSlave != null)
        {
            List<GameObject> objectsToTimeSummon = timeSummonSlave.ReturnObjectsToBeTimeSummoned();

            if (objectsToTimeSummon.Count == 0)
            {
                Debug.Log("There are no objects in the appropriate zone to time summon");
            }
            else
            {
                foreach (GameObject objectToTimeSummon in objectsToTimeSummon)
                {
                    objectToTimeSummon.transform.position = summoningPoint.position;
                }
            }
        }
        else
        {
            Debug.LogError(this + " is not attached to any TimeSummonSlave");
        }
    }

    private TimeSummonSlave FindMatchingTimeSummonSlave(int masterTimeSummonID)
    {
        TimeSummonSlave[] timeSummonSlaves = FindObjectsOfType<TimeSummonSlave>();

        foreach (TimeSummonSlave timeSummonSlave in timeSummonSlaves)
        {
            // Get the TimeSummonID of the slave and compare it to the master's ID
            int slaveTimeSummonID = timeSummonSlave.ReturnTimeSummonID();

            if (slaveTimeSummonID == masterTimeSummonID)
            {
                return timeSummonSlave;
            }
        }

        // Didn't find a matching TimeSummonSlave
        Debug.Log("Didn't find a matching TimeSummonSlave");
        return null;
    }
}
