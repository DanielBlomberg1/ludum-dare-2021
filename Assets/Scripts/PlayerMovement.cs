using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 mov = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized * movementSpeed * Time.deltaTime;

        transform.Translate(mov);
    }
}
