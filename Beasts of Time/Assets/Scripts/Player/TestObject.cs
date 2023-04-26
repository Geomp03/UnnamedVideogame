using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    private Rigidbody2D rb;

    public float Force = 100f;
    private bool addForce;

    public enum TestObjectMode {addForce, SomethingElse}
    public TestObjectMode testObjectMode = new TestObjectMode();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // Check for input
        if (Input.GetKey(KeyCode.P))
            addForce = true;
    }

    private void FixedUpdate()
    {
        if (addForce && testObjectMode.ToString() == "addForce")
        {
            rb.AddForce(new Vector2(0, Force));
            addForce = false;
        }
    }
}
