using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;
    public float invincibilityTime = 2f; 
    private float lastDamageTime; 

    void Start()
    {
        health = maxHealth;
        lastDamageTime = -invincibilityTime; 
    }

    public void TakeDamage(int damage)
    {
        if (Time.time - lastDamageTime >= invincibilityTime)
        {
            health -= damage;
            lastDamageTime = Time.time;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
