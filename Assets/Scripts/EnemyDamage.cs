using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    private PlayerHealth playerHealth;

    public Collider2D levelCollider;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        EnemyDamage[] otherEnemies = FindObjectsOfType<EnemyDamage>();
        if(otherEnemies != null)
        {
            for(int i = 0; i < otherEnemies.Length; i++)
            {
                Physics2D.IgnoreCollision(levelCollider, otherEnemies[i].levelCollider);
            }
        }

        if (playerHealth != null)
        {
            Physics2D.IgnoreCollision(levelCollider, playerHealth.GetComponent<Collider2D>());
        }
        else
        {
            Debug.LogError("PlayerHealth script not found.");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("PlayerHealth script not found.");
            }
        }
    }
}
