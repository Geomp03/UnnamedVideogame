using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBubble : MonoBehaviour
{
    [SerializeField] private Altar altar;
    [SerializeField] private GameObject timeBubbleVisual;

    public enum TimeBubbleType { SlowDown, SpeedUp }
    public TimeBubbleType timeBubbleType = new TimeBubbleType();

    // Start is called before the first frame update
    void Start()
    {
        altar.OnBoolAltarInteraction += Altar_OnBoolAltarInteraction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        ITimeBubble timeBubble = collision.GetComponent<ITimeBubble>();
        if (timeBubble != null)
        {
            if (timeBubbleType == TimeBubbleType.SlowDown)
                timeBubble.SlowDownTimePerception();
            else if (timeBubbleType == TimeBubbleType.SpeedUp)
                timeBubble.SpeedUpTimePerception();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ITimeBubble timeBubble = collision.GetComponent<ITimeBubble>();
        if (timeBubble != null)
        {
            timeBubble.ResetTimePerception();
        }
    }

    private void Altar_OnBoolAltarInteraction(bool obj)
    {
        Debug.Log("Boolean altar event received at " + this + " as " + obj);
        // Enable/Disable time bubble collider and visual
        timeBubbleVisual.SetActive(obj);
        this.GetComponent<Collider2D>().enabled = obj;
    }

}

public interface ITimeBubble
{
    void SlowDownTimePerception();
    void SpeedUpTimePerception();
    void ResetTimePerception();
}