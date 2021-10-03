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
        currentStage += 1;
        if(currentStage <= objStates.Length){
            sr.sprite = objStates[currentStage];
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            Break();
        }
    }
}
