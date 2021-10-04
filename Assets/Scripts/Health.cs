using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.SceneManagement;


public class Health : MonoBehaviour
{
    [HideInInspector]
    public int health;

    public int baseHealth;

    public Slider healthBar;

    [SerializeField] private AudioClip[] hurtSound;
    [SerializeField] private AudioSource aS;

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
        print(gameObject.name+" "+health);

        if (healthBar)
        {
            healthBar.value = health / baseHealth;
        }
        if(hurtSound.Length > 0){
            aS.clip=(hurtSound[Random.Range(0, hurtSound.Length + 1)]);
            aS.Play();
        }

        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        if(gameObject.tag=="Player"){
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
        if(gameObject.GetComponent<TreasureHolder>() != null){
            gameObject.GetComponent<TreasureHolder>().OnDeath();
        }
        Destroy(gameObject);
    }
}
