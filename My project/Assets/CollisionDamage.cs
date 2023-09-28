using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    Health health;
    public float damagePerSpeed = 10f;
    public float minimumDamage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float damage = collision.relativeVelocity.magnitude * damagePerSpeed;
        if (damage > minimumDamage && collision.gameObject.tag == "Environment")
        {
            health.ChangeHealth(-damage);
        }
    }
}
