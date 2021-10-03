using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerMovement playerMovement;

    Animator swordAnimator;

    public GameObject sword;

    Vector2 hitDirection = Vector2.up;

    public int damage;
    public float hitCooldown;
    public float hitDistance;
    public float hitAngle;

    float lastHit;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        swordAnimator = sword.GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerMovement.movementDir != Vector2.zero)
        {
            hitDirection = playerMovement.movementDir;
        }

        if (Input.GetMouseButtonDown(0) && Time.time - lastHit > hitCooldown)
        {
            Strike();

            lastHit = Time.time;
        }
    }

    void Strike()
    {
        float angle = Mathf.Atan2(hitDirection.y, hitDirection.x) * Mathf.Rad2Deg;
        sword.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        sword.transform.localPosition = hitDirection;

        swordAnimator.SetTrigger("SwordHit");

        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, hitDistance))
        {
            if (!coll.CompareTag("Player"))
            {
                if (Vector2.Angle(hitDirection, coll.transform.position - transform.position) < hitAngle / 2)
                {
                    print("hit " + coll.name + " angle: " + Vector2.Angle(hitDirection, coll.transform.position - transform.position));

                    if (coll.GetComponent<Health>() != null)
                    {
                        coll.GetComponent<Health>().TakeDamage(damage);
                    }
                }
            }
        }

    }
}
