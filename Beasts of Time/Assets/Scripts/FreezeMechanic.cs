using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMechanic : MonoBehaviour
{
    private Rigidbody2D rb;

    public float freezeDuration;
    public float useRate;
    private float nextUse = 0f;
    private float angVel;
    private Vector2 vel;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time > nextUse)
        {
            nextUse = Time.time + useRate;
            StartCoroutine(TimedFreeze(freezeDuration));
        }
    }
    
    public IEnumerator TimedFreeze(float freezeDuration)
    {
        FreezeObject();
        yield return new WaitForSeconds(freezeDuration);
        UnFreezeObject();
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
