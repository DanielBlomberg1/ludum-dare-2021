using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [HideInInspector]
    public int health;

    public int baseHealth;

    public Slider healthBar;

    void Start()
    {
        health = baseHealth;

        if (healthBar)
        {
            healthBar.value = health / baseHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (healthBar)
        {
            healthBar.value = health / baseHealth;
        }

        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
