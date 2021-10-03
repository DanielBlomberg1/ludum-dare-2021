using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerMovement playerMovement;

    Animator swordAnimator;

    public GameObject sword;

    public float hitCooldown;

    float lastHit;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        swordAnimator = sword.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastHit > hitCooldown)
        {
            Strike();

            lastHit = Time.time;
        }
    }

    void Strike()
    {
        Vector2 hitDirection = playerMovement.movementDir;

        if (hitDirection == Vector2.zero)
        {
            hitDirection = Vector2.up;
        }

        float angle = Mathf.Atan2(hitDirection.y, hitDirection.x) * Mathf.Rad2Deg;
        sword.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        sword.transform.localPosition = hitDirection;

        swordAnimator.SetTrigger("SwordHit");
    }
}
