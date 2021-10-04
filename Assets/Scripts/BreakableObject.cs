using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private Sprite[] objStates;
    private SpriteRenderer sr;
    private int currentStage = 0;

    private void Start() {
        sr=GetComponent<SpriteRenderer>();
    }

    //when player has weapon implement calling;
    public void Break(){
        if(currentStage < objStates.Length){
            sr.sprite = objStates[currentStage];
            if(currentStage + 1 >= objStates.Length){
                Destroy(GetComponent<BoxCollider2D>());
            }
        }
        currentStage += 1;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            Break();
        }
    }
}
