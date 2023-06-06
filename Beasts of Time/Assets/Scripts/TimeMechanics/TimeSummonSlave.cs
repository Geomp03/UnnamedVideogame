using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSummonSlave : MonoBehaviour
{
    [SerializeField] private int TimeSummonID;

    private List<GameObject> objectsToTimeSummon = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject + " is in the time summon area");
            objectsToTimeSummon.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject + " is no longer in the time summon area");
            objectsToTimeSummon.Remove(collision.gameObject);
        }
    }

    public List<GameObject> ReturnObjectsToBeTimeSummoned()
    {
        return objectsToTimeSummon;
    }

    public int ReturnTimeSummonID()
    {
        return TimeSummonID;
    }
}
