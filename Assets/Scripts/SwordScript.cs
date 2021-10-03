using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    private float shootTimer = 2; 
    private Rigidbody2D rb;
    void Start()
    {
        //rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer < 0){
            //rb.AddForce(new Vector2(100,0));
        }
        if(shootTimer < -1){
            //Destroy(gameObject);
        }
    }
}
