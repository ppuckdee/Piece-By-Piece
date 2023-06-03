using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float speed;
    public float[] targetPoints;
    private Vector2 dir;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = Vector2.right;
        Debug.Log(dir);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = dir * speed;
        rb.velocity = new Vector2(movementVector.x, rb.velocity.y);
        if((dir == Vector2.right && transform.position.x > targetPoints[1]) || (dir == Vector2.left && transform.position.x < targetPoints[0]))
        {
            Debug.Log("Turning Around");
            dir *= -1;
        }

        if(Random.Range(0, 600) == 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2*Physics.gravity.y*0.5f));
        }
    }
}
