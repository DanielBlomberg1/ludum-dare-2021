using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DarkLabyrinthScript : MonoBehaviour
{
    [SerializeField] private GameObject darknessEffect;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            darknessEffect.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            darknessEffect.SetActive(false);
        }
    }
}
