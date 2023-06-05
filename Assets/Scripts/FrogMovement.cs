using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    public Transform player;
    public float jumpHeight = 3f;
    public bool jumping, grounded;
    public LayerMask groundCheckMask;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!player)
        {
            player = FindObjectOfType<PlayerMovement>().transform;
        }
        jumping = grounded = false;
    }

    void Update()
    {
        checkGrounded(0.05f);

        if (grounded && player.position.y > transform.position.y)
        {
            Jump();
        }

        if (player.position.x > transform.position.x)
    {
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    else
    {
    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }


        if (jumping && rb.velocity.y < 0)
        {
            jumping = false;
        }
    }

    void Jump()
    {
        jumping = true;
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(-2 * Physics2D.gravity.y * jumpHeight));
    }

    private void checkGrounded(float dist)
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Vector3 ray1Pos = transform.position + new Vector3(collider.offset.x - collider.size.x / 2 + 0.05f, collider.offset.y - collider.size.y / 2 + 0.005f) * transform.localScale.y;
        Vector3 ray2Pos = transform.position + new Vector3(collider.offset.x, collider.offset.y - collider.size.y / 2 + 0.005f) * transform.localScale.y;
        Vector3 ray3Pos = transform.position + new Vector3(collider.offset.x + collider.size.x / 2 - 0.05f, collider.offset.y - collider.size.y / 2 + 0.005f) * transform.localScale.y;

        Debug.DrawRay(ray1Pos, Vector2.down * dist, Color.red);
        Debug.DrawRay(ray2Pos, Vector2.down * dist, Color.red);
        Debug.DrawRay(ray3Pos, Vector2.down * dist, Color.red);
        if (jumping)
        {
            grounded = false;
            return;
        }
        RaycastHit2D groundCheckHit1 = Physics2D.Raycast(ray1Pos, Vector3.down, dist, groundCheckMask);
        RaycastHit2D groundCheckHit2 = Physics2D.Raycast(ray2Pos, Vector3.down, dist, groundCheckMask);
        RaycastHit2D groundCheckHit3 = Physics2D.Raycast(ray3Pos, Vector3.down, dist, groundCheckMask);
        if (groundCheckHit1.collider != null || groundCheckHit2.collider != null || groundCheckHit3.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
