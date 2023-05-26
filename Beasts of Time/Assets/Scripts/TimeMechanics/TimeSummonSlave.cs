using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSummonSlave : MonoBehaviour
{
    private List<GameObject> objectsToTimeSummon;

    private void Start()
    {
        objectsToTimeSummon = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject + "in time summon area");
            objectsToTimeSummon.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject + "no longer in time summon area");
            objectsToTimeSummon.Remove(collision.gameObject);
        }
    }

    public List<GameObject> ReturnObjectsToBeTimeSummoned()
    {
        return objectsToTimeSummon;
    }
}
