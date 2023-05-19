using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    public float speed, jumpHeight;
    public float gravity;

    public LayerMask groundCheckMask;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private float jumpSpeed;
    private Vector2 inputs;
    private bool grounded, jumping;
    private bool jumpTrigger;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grounded = true;
        jumping = jumpTrigger = false;
        jumpSpeed = Mathf.Sqrt(2*gravity*jumpHeight);
    }

    // Update is called once per frame
    void Update()
    {
        inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log("JumpTrigger");
            jumpTrigger = true;
        }
    }

    void FixedUpdate()
    {

        checkGrounded(0.05f);

        float velocityX = inputs.x * speed;
        float velocityY;
        if(!grounded || jumping)
        {
            velocityY = velocity.y - gravity * Time.deltaTime;
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
                Debug.Log("Jumping");
                velocityY = jumpSpeed;
                jumping = true;
            }
        }
        velocity = new Vector2(velocityX, velocityY);

        rb.velocity = velocity;
        resetTriggers();
    }

    private void checkGrounded(float dist)
    {
        if(jumping) return;
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
