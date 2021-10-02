using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private float baseMovementSpeed;

    //dashing variables
    public float dashSpeed;
    public float dashLength = 0.5f;
    public float dashCooldown = 1f;
    private float dashCounter = 0;
    private float dashCoolCounter = 0;


    private void Start() {
        baseMovementSpeed = movementSpeed;
    }
    // Update is called once per frame
    void Update()
    {        
        print(dashCoolCounter);
        print(dashCounter);
        //dashing
        if(Input.GetKeyDown(KeyCode.Space)){
            if(dashCoolCounter <= 0 && dashCounter <= 0){
                movementSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }
        if(dashCounter > 0){
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0){
                movementSpeed = baseMovementSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if(dashCoolCounter > 0){
            dashCoolCounter -= Time.deltaTime;
        }

        Vector3 mov = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized * movementSpeed * Time.deltaTime;
        transform.Translate(mov);
    }
}
