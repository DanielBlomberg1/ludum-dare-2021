using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    private bool playerDetected = false;
    private float distanceToPlayer;
    GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("player");
    }


    bool lineOfSight(){
        return true;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distanceToPlayer < 25 && lineOfSight() == true){

        }
    }
}
