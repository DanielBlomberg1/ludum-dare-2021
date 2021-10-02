using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableController : MonoBehaviour
{
    public float baseSpeed;
    public float currentSpeed;
    public float unstableSpeed;
    public float unstableTimer = 10;

    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unstableTimer -= Time.deltaTime;

        if(unstableTimer <= -1){
            currentSpeed = baseSpeed;
            unstableTimer = 10;
        }
        else if(unstableTimer < 1){
            currentSpeed = unstableSpeed;
        }
        player.GetComponent<PlayerMovement>().movementSpeed = currentSpeed;
        

    }
}
