using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    private float shootTimer = 3;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Health>().TakeDamage(10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer < 2)
        {
            rb.AddForce(transform.up * 10);
        }

        if (shootTimer < -1)
        {
            Destroy(gameObject);
        }
    }
}
