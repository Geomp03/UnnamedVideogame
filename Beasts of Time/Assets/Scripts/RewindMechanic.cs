using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindMechanic : MonoBehaviour
{
    private Rigidbody2D rb;
    private LinkedList<PointInTime> timeMemory;
    /* This is currently implemented with a linked list. To further improve 
     * performance consider using a fixed array (circular array)... */

    private bool hasRB;
    private bool isRewinding = false;
    public float rewindTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        hasRB = TryGetComponent(out Rigidbody2D rigidbody);
        // Get a reference to the rigidbody IF it exists.
        if (hasRB)
            rb = GetComponent<Rigidbody2D>();

        timeMemory = new LinkedList<PointInTime>();
    }

    // Update is called once per frame
    void Update()
    {
        // Temporary control...
        if (Input.GetKeyDown(KeyCode.R))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.R))
            StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void StartRewind()
    {
        isRewinding = true;
        if (hasRB)
            rb.isKinematic = true;
    }

    void StopRewind()
    {
        isRewinding = false;
        if (hasRB)
            rb.isKinematic = false;
    }

    void Rewind()
    {
        // When rewinding always take the first node of the fixed list, then remove it...
        if (timeMemory.Count > 0)
        {
            PointInTime temp = timeMemory.First.Value;
            transform.SetPositionAndRotation(temp.position, temp.rotation);
            timeMemory.RemoveFirst();
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        // When recording, ensure that the list stays within the limit...
        if (timeMemory.Count > Mathf.Round(rewindTime / Time.fixedDeltaTime))
        {
            timeMemory.RemoveLast();
        }

        timeMemory.AddFirst(new PointInTime(transform.position, transform.rotation));
    }
}
