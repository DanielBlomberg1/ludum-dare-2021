using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerMovement>().areBootsOn = true;
            Destroy(gameObject);
        }    
    }
}
