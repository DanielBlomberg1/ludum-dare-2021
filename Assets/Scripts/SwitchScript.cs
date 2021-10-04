using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    [SerializeField] private Sprite switch0;
    [SerializeField] private Sprite switch1;
    [SerializeField] private GameObject door;
    private int currentSprite = 0;
    private SpriteRenderer spriteRenderer;
    // Update is called once per frame

    private void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(currentSprite==0){
            spriteRenderer.sprite = switch1;
            currentSprite += 1;
            door.SetActive(false);
        }
        else{
            spriteRenderer.sprite = switch0;
            currentSprite -= 1;
            door.SetActive(true);
        }
    }
}
