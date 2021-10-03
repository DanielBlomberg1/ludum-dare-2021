using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLabyrinthScript : MonoBehaviour
{
    [SerializeField] private GameObject darknessEffect;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            darknessEffect.SetActive(true);
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            darknessEffect.SetActive(false);
        }
    }
}
