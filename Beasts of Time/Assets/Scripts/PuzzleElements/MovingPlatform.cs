using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, ITimeBubbleAffectable, IFreezeMechanic
{
    public enum PlatformStatus { Active, Inactive, Frozen }
    public PlatformStatus platformStatus = new PlatformStatus();

    [SerializeField] private Transform[] pointArray;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float timeBubbleModifier = 1f;

    private enum MovementDir { MoveAB, MoveBA }
    private MovementDir movementDir = new MovementDir();

    private Transform currentTarget;
    private int targetIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Place the moving platform at the first target point
        currentTarget = pointArray[targetIndex];
        transform.position = currentTarget.position;
        movementDir = MovementDir.MoveAB;
    }

    // Update is called once per frame
    void Update()
    {
        // Move test object from point A to point B or back
        if (platformStatus == PlatformStatus.Active)
        {
            MoveFromPointToPoint();
        }
    }

    private void MoveFromPointToPoint()
    {
        // Firstly check if the platform is currently on its target
        if (transform.position == currentTarget.position)
        {
            // Increase/Decrease the target index depending on current movement direction
            if (movementDir == MovementDir.MoveAB)      targetIndex++;
            else if (movementDir == MovementDir.MoveBA) targetIndex--;

            // Assign a new position target
            currentTarget = pointArray[targetIndex];

            // Check if the target index points to extremities
            if (targetIndex >= pointArray.Length - 1)
            {
                // Platform moved to the last point of the array so start moving backwards.
                movementDir = MovementDir.MoveBA;
            }
            else if (targetIndex == 0)
            {
                // Platform returned to the first point of the array.
                movementDir = MovementDir.MoveAB;
            }
        }

        // Movement towards the current target
        float step = speed * timeBubbleModifier * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, step);
    }

    // IFreezeMechanic
    public void FreezeObject()
    {
        platformStatus = PlatformStatus.Frozen;
    }

    public void UnfreezeObject()
    {
        platformStatus = PlatformStatus.Active;
    }

    // ITimeBubbleAffectable implementation
    public void SlowDownTimePerception()
    {
        Debug.Log("Slow down time perception for " + this);
        timeBubbleModifier = 0.25f;
    }
    public void SpeedUpTimePerception()
    {
        Debug.Log("Speed up time perception for " + this);
        timeBubbleModifier = 4f;
    }
    public void ResetTimePerception()
    {
        Debug.Log("Reset time perception for " + this);
        timeBubbleModifier = 1f;
    }
}
