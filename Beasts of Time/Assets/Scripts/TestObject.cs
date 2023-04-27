using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    private Rigidbody2D rb;

    public float force = 500f;
    private bool addForce;

    public enum TestObjectMode {addVerticalForce, addHorizontalForce, Something}
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
        if (Input.GetKeyDown(KeyCode.T))
            addForce = true;
    }

    private void FixedUpdate()
    {
        if (addForce && testObjectMode == TestObjectMode.addVerticalForce)
        {
            rb.AddForce(new Vector2(0, force));
            addForce = false;
        }

        if (addForce && testObjectMode == TestObjectMode.addHorizontalForce)
        {
            rb.AddForce(new Vector2(force, 0));
            addForce = false;
        }
    }
}
