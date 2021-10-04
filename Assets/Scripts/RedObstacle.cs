using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedObstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            if(other.gameObject.GetComponent<PlayerMovement>().areBootsOn){
                Destroy(gameObject.GetComponent<BoxCollider2D>());
            }
        }
    }
}
