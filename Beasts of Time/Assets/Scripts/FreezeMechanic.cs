using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMechanic : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 vel;
    private float angVel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // FreezeObject;
            vel = rb.velocity;
            angVel = rb.angularVelocity;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            // UnFreezeObject;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.velocity = vel;
            rb.angularVelocity = angVel;
        }
    }
    
    public void FreezeObject()
    {
        vel = rb.velocity;
        angVel = rb.angularVelocity;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnFreezeObject()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = vel;
        rb.angularVelocity = angVel;
    }
}
