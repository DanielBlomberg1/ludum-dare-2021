using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidBody;

    [HideInInspector]
    public Vector2 movementDir;

    [HideInInspector]
    public float movementSpeed;


    public float baseMovementSpeed;

    //dashing variables
    public float dashLength;
    public float dashCooldown;
    public float dashSpeed;
    public float dashDuration;
    [SerializeField] private Transform dashAnimationEffect;

    float lastDash;

    Vector3 dashPosition;

    private void Start()
    {
        movementSpeed = baseMovementSpeed;

        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //dashing
        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time - lastDash > dashCooldown)
            {
                lastDash = Time.time;
                dashPosition = rigidBody.position + movementDir * dashLength;

                Transform dashAnimation = Instantiate(dashAnimationEffect, gameObject.transform.position, Quaternion.identity);

                float angle = Mathf.Atan2(movementDir.y, movementDir.x) * Mathf.Rad2Deg;
                dashAnimation.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            }
        }
    }

    void FixedUpdate()
    {
        if (Time.time - lastDash < dashDuration)
        {
            rigidBody.MovePosition(Vector3.Lerp(rigidBody.position, dashPosition, dashSpeed));
        }

        else
        {
            movementDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            Vector2 movement = movementDir * movementSpeed * Time.deltaTime;

            rigidBody.MovePosition(rigidBody.position + movement);
        }
    }
}
