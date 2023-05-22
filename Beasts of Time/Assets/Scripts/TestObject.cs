using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour, ITimeBubbleAffectable
{
    public enum TestObjectMode { moveAB, addVerticalForce, addHorizontalForce, }
    public TestObjectMode testObjectMode = new TestObjectMode();

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedModifier = 1f;
    private bool performeAction = false;
    private Transform target;

    private void Start()
    {
        transform.position = pointA.position;
        target = pointB;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for input
        if (Input.GetKeyDown(KeyCode.T))
            performeAction = !performeAction;

        // Move test object from point A to point B or back
        if (performeAction && testObjectMode == TestObjectMode.moveAB)
        {
            if (transform.position == pointA.position) target = pointB;
            if (transform.position == pointB.position) target = pointA;

            float step = speed * speedModifier * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

    public void SlowDownTimePerception()
    {
        Debug.Log("Slow down time perception for " + this);
        speedModifier = 0.25f;
    }
    public void SpeedUpTimePerception()
    {
        Debug.Log("Speed up time perception for " + this);
        speedModifier = 4f;
    }
    public void ResetTimePerception()
    {
        Debug.Log("Reset time perception for " + this);
        speedModifier = 1f;
    }
}
