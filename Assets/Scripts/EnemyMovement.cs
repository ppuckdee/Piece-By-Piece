using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDest;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    private void Start()
    {
        if(!playerTransform)
        {
            playerTransform = FindObjectOfType<PlayerMovement>().transform;
        }

        patrolDest = 0;
        isChasing = false;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            else if (transform.position.x < playerTransform.position.x)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {

            if (patrolPoints.Length > 0)
            {
                Transform destination = patrolPoints[patrolDest];
                Vector3 patrolDirection = (destination.position - transform.position).normalized;
                transform.position += patrolDirection * moveSpeed * Time.deltaTime;

                if (Vector2.Distance(transform.position, destination.position) < 0.1f)
                {

                    patrolDest = (patrolDest + 1) % patrolPoints.Length;
                }
            }
        }
    }
}

