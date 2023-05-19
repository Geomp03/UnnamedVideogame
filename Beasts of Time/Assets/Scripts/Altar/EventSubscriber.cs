using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    [SerializeField] Altar altar;

    // Start is called before the first frame update
    void Start()
    {
        altar.OnBoolAltarInteraction += OnBoolAltarInteraction;
        altar.OnTriggerAltarInteraction += OnTriggerAltarInteraction;
    }

    private void OnTriggerAltarInteraction(object sender, System.EventArgs e)
    {
        Debug.Log("Trigger Event");
    }

    private void OnBoolAltarInteraction(bool obj)
    {
        Debug.Log("Boolean Event: " + obj);
    }
}
