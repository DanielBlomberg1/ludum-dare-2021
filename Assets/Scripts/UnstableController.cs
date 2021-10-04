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

    public GameObject unstableEffect;

    [SerializeField]
    GameObject player;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        unstableEffect.SetActive(false);
    }

    void Update()
    {
        unstableTimer -= Time.deltaTime;

        if (unstableTimer <= 0)
        {
            currentSpeed = playerMovement.baseMovementSpeed;
            unstableTimer = Random.Range(4, 16);
            unstableEffect.SetActive(false);
            timer.color = Color.green;
        }

        else if (unstableTimer < 2)
        {
            currentSpeed = unstableSpeed;
            unstableEffect.SetActive(true);
            timer.color = Color.red;

        }
        playerMovement.movementSpeed = currentSpeed;

        timer.text = Mathf.Round(unstableTimer).ToString();
    }
}
