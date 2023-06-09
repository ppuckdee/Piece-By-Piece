using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;
    public float invincibilityTime = 2f; 
    private float lastDamageTime; 

    public float healthBarWidth, healthBarHeight;
    public GameObject healthBar, text;


    void Start()
    {
        refillHealth();
        lastDamageTime = -invincibilityTime; 
    }

    public void refillHealth()
    {
            health = maxHealth;
            ((RectTransform)healthBar.transform).sizeDelta = new Vector2(healthBarWidth, healthBarHeight);
            text.GetComponent<Text>().text = health + "/" + maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (Time.time - lastDamageTime >= invincibilityTime)
        {
            health -= damage;
            if(health < 0) health = 0;
            ((RectTransform)healthBar.transform).sizeDelta = new Vector2(healthBarWidth*((float)health/maxHealth), healthBarHeight);
            lastDamageTime = Time.time;
            text.GetComponent<Text>().text = health + "/" + maxHealth;

            if (health <= 0)
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
                //Destroy(gameObject);
            }
        }
    }
}
