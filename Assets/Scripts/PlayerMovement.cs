using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    public float maxSpeed, groundedAcceleration, groundedDeceleration, airAcceleration, airDeceleration, jumpHeight, minPositionDelta;
    public float IdleGravity, jumpingGravity;

    public LayerMask groundCheckMask;

    public float standingPlayerHeight, crouchPlayerHeight;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private Vector2 lastPosition;
    private Vector2 inputs;
    public bool grounded, jumping;
    private bool jumpTrigger;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grounded = true;
        jumping = jumpTrigger = false;
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

        checkGrounded(0.05f);

        #region Gravity and Jumping
        float velocityY = velocity.y;
        if(!grounded)
        {
            if(jumping)
            {
                velocityY -= jumpingGravity * Time.deltaTime;
            }
            else
            {
                velocityY -= IdleGravity * Time.deltaTime;
            }
            if(velocityY <= 0)
            {
                jumping = false;
            }
        }
        else
        {
            velocityY = 0f;
            if(jumpTrigger)
            {
                velocityY = Mathf.Sqrt(2*jumpingGravity*jumpHeight);
                jumping = true;
            }
        }
        #endregion

        #region Horizontal Movement
        float velocityX = velocity.x;
        if(inputs.x != 0)
        {
            Vector2 positionDelta = (Vector2)transform.position - lastPosition;
            if(Mathf.Abs(positionDelta.x) <= minPositionDelta)
            {
                velocityX = 0f;
            }
            if(grounded)
            {
                velocityX += inputs.x * groundedAcceleration * Time.deltaTime;
            }
            else
            {
                velocityX += inputs.x * airAcceleration * Time.deltaTime;
            }
        }
        else
        {
            float sign = Mathf.Sign(velocityX);
            if(grounded)
            {
                velocityX -= sign * groundedDeceleration * Time.deltaTime;
            }
            else
            {
                velocityX -= sign * airDeceleration * Time.deltaTime;
            }
            if(sign != Mathf.Sign(velocityX)) velocityX = 0f;
        }
        if(velocityX > maxSpeed) velocityX = maxSpeed;
        if(velocityX < -maxSpeed) velocityX = -maxSpeed;
        #endregion

        velocity = new Vector2(velocityX, velocityY);

        rb.velocity = velocity;
        lastPosition = transform.position;
        resetTriggers();
    }

    private void checkGrounded(float dist)
    {
        if(jumping)
        {
            grounded = false;
            return;
        }
        RaycastHit2D groundCheckHit = Physics2D.Raycast(transform.position + Vector3.down*1.01f, Vector3.down, dist, groundCheckMask);
        if(groundCheckHit.collider != null)
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
