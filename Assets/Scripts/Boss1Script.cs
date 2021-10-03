using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Script : MonoBehaviour
{
    private bool awake = false;
    Vector3 pos1 = new Vector3(230, 83, 2.5f);
    Vector3 pos2 = new Vector3(318, 83, 2.5f);
    Vector3 moveDes;

    [SerializeField] private GameObject swordPf;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            awake = true;
            print("Beast awoken");
        }
    }

    private void SwordStorm(int n, float r)
    {
        for (int i = 0; i < n; i++)
        {
            float rad = 2 * Mathf.PI / n * i;
            float xaxis = Mathf.Cos(rad);
            float yaxis = Mathf.Sin(rad);

            Vector3 spawnDir = new Vector3(yaxis, xaxis, 0);
            Vector3 spawnPos = gameObject.transform.position + spawnDir * r;

            GameObject sword = Instantiate(swordPf, spawnPos, Quaternion.identity) as GameObject;
            //sword.transform.rotation = Quaternion.AngleAxis(i*r*Mathf.PI, Vector3.forward);

            //magic happens here
            Vector3 direction = sword.transform.position - gameObject.transform.position;
            sword.transform.rotation = Quaternion.LookRotation(direction);
            float angle = Mathf.Atan2(sword.transform.rotation.y, sword.transform.rotation.x) * Mathf.Rad2Deg;
            sword.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        }
    }


    private void Start()
    {

    }


    private void Update()
    {
        if (awake)
        {
            int seed = Random.Range(0, 10000);
            int rare = Random.Range(0, 5);
            if (seed <= 20)
            {
                SwordStorm(12, 6);
            }
            else if (seed <= 100)
            {
                transform.Translate(new Vector3(0, -5, 0));
            }
            else if (seed <= 200)
            {
                transform.Translate(new Vector3(5, 0, 0));
            }
            else if (seed <= 300)
            {
                transform.Translate(new Vector3(-5, 0, 0));
            }
            else if (seed <= 400)
            {
                transform.Translate(new Vector3(0, 5, 0));
            }

        }
    }
}
