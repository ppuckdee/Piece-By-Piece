using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public PlayerHealth playerHealth;

    private Collider2D levelCollider; //collide with level
    private Collider2D damageCollider; //damage player

    private void Start()
    {
        levelCollider = GetComponent<Collider2D>();
        damageCollider = transform.Find("DamageCollider").GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(levelCollider, playerHealth.GetComponent<Collider2D>());
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
        }
    }
}

