using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private Vector2 inputs;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Debug.Log(inputs);
    }

    void FixedUpdate()
    {
        float velocityX = inputs.x * speed;
        float velocityY = inputs.y * speed;
        velocity = new Vector2(velocityX, velocityY);

        rb.velocity = velocity;
    }
}
