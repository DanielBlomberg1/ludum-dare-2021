using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    private float distanceToPlayer;
    GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    bool lineOfSight(){
        return true;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 7 && lineOfSight() == true){
            //do move
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else{
            //do idle
            int rare = Random.Range(0, 1000);
            if(rare < 5){
                int seed = Random.Range(0,4);
                switch(seed){
                    case 0: 
                        transform.Translate(new Vector3(0,1,0));
                    break;
                    case 1:
                        transform.Translate(new Vector3(0,-1,0));
                    break;
                    case 2:
                        transform.Translate(new Vector3(1,0,0));
                    break;
                    case 3:
                        transform.Translate(new Vector3(-1,0,0));
                    break;
                }
            }
        }
    }
}
