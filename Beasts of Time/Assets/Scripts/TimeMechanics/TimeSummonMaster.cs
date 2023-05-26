using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSummonMaster : MonoBehaviour
{
    [SerializeField] private Altar altar;
    [SerializeField] private Transform summoningPoint;
    private TimeSummonSlave timeSummonSlave;

    private void Start()
    {
        timeSummonSlave = FindObjectOfType<TimeSummonSlave>();
        altar.OnTriggerAltarInteraction += Altar_OnTriggerAltarInteraction;
    }

    private void Altar_OnTriggerAltarInteraction(object sender, System.EventArgs e)
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
}
