using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed, groundedAcceleration, groundedDeceleration, airAcceleration, airDeceleration, jumpHeight, groundCheckDist;
    public float IdleGravity, jumpingGravity;

    public LayerMask groundCheckMask;

    public float standingPlayerHeight, crouchPlayerHeight;

    private Rigidbody2D rb;
    private Vector2 lastPosition;
    private Vector2 inputs;
    public bool grounded, jumping;
    private bool jumpTrigger;
    public bool freeBody;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grounded = true;
        jumping = jumpTrigger = freeBody = false;
    }

    // Update is called once per frame
    void Update()
    {
        inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(Input.GetButtonDown("Jump"))
        {
            jumpTrigger = true;
        }
    }

    void FixedUpdate()
    {
        checkGrounded(groundCheckDist);

        #region Gravity and Jumping
        float velocityY = rb.velocity.y;
        if(!grounded)
        {
            if(jumping)
            {
                velocityY -= jumpingGravity * Time.fixedDeltaTime;
            }
            else
            {
                velocityY -= IdleGravity * Time.fixedDeltaTime;
            }
            if(velocityY <= 0)
            {
                jumping = false;
            }
        }
        else
        {
            if(velocityY < 0.25f) velocityY = -0.25f;
            if(jumpTrigger)
            {
                velocityY = Mathf.Sqrt(2*jumpingGravity*jumpHeight);
                jumping = true;
            }
        }
        #endregion

        #region Horizontal Movement
        float velocityX = rb.velocity.x;
        if(inputs.x != 0)
        {
            if(Mathf.Sign(velocityX) != Mathf.Sign(inputs.x) || (velocityX < maxSpeed && velocityX > -maxSpeed))
            {
                if(grounded)
                {
                    velocityX += inputs.x * groundedAcceleration * Time.fixedDeltaTime;
                }
                else
                {
                    velocityX += inputs.x * airAcceleration * Time.fixedDeltaTime;
                }
            }
            else
            {
                velocityX -= Mathf.Sign(velocityX) * 7f * Time.fixedDeltaTime;
            }
        }
        else if(velocityX != 0 && !freeBody)
        {
            Vector2 positionDelta = (Vector2)transform.position - lastPosition;
            float sign = Mathf.Sign(velocityX);
            if(grounded)
            {
                velocityX -= sign * groundedDeceleration * Time.fixedDeltaTime;
            }
            else
            {
                velocityX -= sign * airDeceleration * Time.fixedDeltaTime;
            }
            if(sign != Mathf.Sign(velocityX)) velocityX = 0f;
        }
        #endregion

        rb.velocity = new Vector2(velocityX, velocityY);

        lastPosition = transform.position;
        resetTriggers();
    }

    private void checkGrounded(float dist)
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Vector3 ray1Pos = transform.position + new Vector3(collider.offset.x - collider.size.x/2 + 0.05f, collider.offset.y - collider.size.y/2 + 0.005f) * transform.localScale.y;
        Vector3 ray2Pos = transform.position + new Vector3(collider.offset.x, collider.offset.y - collider.size.y/2 + 0.005f) * transform.localScale.y;
        Vector3 ray3Pos = transform.position + new Vector3(collider.offset.x + collider.size.x/2 - 0.05f, collider.offset.y - collider.size.y/2 + 0.005f) * transform.localScale.y;

        Debug.DrawRay(ray1Pos, Vector2.down*dist, Color.red);
        Debug.DrawRay(ray2Pos, Vector2.down*dist, Color.red);
        Debug.DrawRay(ray3Pos, Vector2.down*dist, Color.red);
        if(jumping)
        {
            grounded = false;
            return;
        }
        RaycastHit2D groundCheckHit1 = Physics2D.Raycast(ray1Pos, Vector3.down, dist, groundCheckMask);
        RaycastHit2D groundCheckHit2 = Physics2D.Raycast(ray2Pos, Vector3.down, dist, groundCheckMask);
        RaycastHit2D groundCheckHit3 = Physics2D.Raycast(ray3Pos, Vector3.down, dist, groundCheckMask);
        if(groundCheckHit1.collider != null || groundCheckHit2.collider != null || groundCheckHit3.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void resetTriggers()
    {
        jumpTrigger = false;
    }
}
