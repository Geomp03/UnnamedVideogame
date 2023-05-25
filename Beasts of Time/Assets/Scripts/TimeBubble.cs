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
        altar.OnBoolAltarInteraction += OnBoolAltarInteraction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ITimeBubbleAffectable affectable = collision.GetComponent<ITimeBubbleAffectable>();

        if (affectable != null)
        {
            if (timeBubbleType == TimeBubbleType.SlowDown)      affectable.SlowDownTimePerception();
            else if (timeBubbleType == TimeBubbleType.SpeedUp)  affectable.SpeedUpTimePerception();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ITimeBubbleAffectable affectable = collision.GetComponent<ITimeBubbleAffectable>();

        if (affectable != null)
            affectable.ResetTimePerception();
    }

    private void OnBoolAltarInteraction(bool obj)
    {
        // Enable/Disable time bubble collider and visual
        timeBubbleVisual.SetActive(obj);
        this.GetComponent<Collider2D>().enabled = obj;
    }

}

public interface ITimeBubbleAffectable
{
    void SlowDownTimePerception();
    void SpeedUpTimePerception();
    void ResetTimePerception();
}