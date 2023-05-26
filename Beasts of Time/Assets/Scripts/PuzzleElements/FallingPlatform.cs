using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour, IFreezeMechanic
{
    [SerializeField] private Rigidbody2D platformRigidbody;
    [SerializeField] private float fallDelay;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the player collided from above the platform.
            if (CheckIfCollisionFromAbove(collision.GetContact(0).point))
            {
                StartCoroutine(DelayedPlatformFall(fallDelay));
            }
        }
    }

    private IEnumerator DelayedPlatformFall(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        platformRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    private bool CheckIfCollisionFromAbove(Vector2 contactPoint)
    {
        Vector2 colNormal = contactPoint - new Vector2(transform.position.x, transform.position.y);
        float angle = Vector2.Angle(colNormal, Vector2.up);

        if (angle <= 45)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // IFreezeMechanic implementation
    public void UnfreezeObject()
    {
        platformRigidbody.constraints = RigidbodyConstraints2D.None;
    }

    public void FreezeObject()
    {
        platformRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
