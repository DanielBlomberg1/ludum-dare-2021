using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnstableController : MonoBehaviour
{
    public float currentSpeed;
    public float unstableSpeed;
    public float unstableTimer = 10;

    PlayerMovement playerMovement;

    public TextMeshProUGUI timer;

    [SerializeField]
    GameObject player;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        unstableTimer -= Time.deltaTime;

        if (unstableTimer <= 0)
        {
            currentSpeed = playerMovement.baseMovementSpeed;
            unstableTimer = 10;
        }

        else if (unstableTimer < 2)
        {
            currentSpeed = unstableSpeed;
        }

        playerMovement.movementSpeed = currentSpeed;

        if (unstableTimer < 2)
        {
            timer.color = Color.red;
        }
        else
        {
            timer.color = Color.green;
        }

        timer.text = Mathf.Round(unstableTimer).ToString();
    }
}
