using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyAi : MonoBehaviour
{
    public Transform Target;
    public GameObject Enemy;
    public GameObject Player;
    private float range;
    private Rigidbody2D rigidbod;
    private Vector2 targetVelocity = Vector2.zero;
    public float speed = 20f;
    public float maxForce = 20f;
    public float enemyMaxHealth = 10f;
    public float enemyCurrentHealth;
    public bool collidedwPlayer;

    void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        Player = GameObject.FindGameObjectWithTag("Player");
        rigidbod = GetComponent<Rigidbody2D>();
        enemyCurrentHealth = enemyMaxHealth;
        collidedwPlayer = false;
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidedwPlayer = true;
        }
    }

    private void FixedUpdate()
    {
        range = Vector2.Distance(Enemy.transform.position, Player.transform.position);
        targetVelocity = (Player.transform.position - transform.position).normalized * speed;
        Vector2 appliedImpulse = Vector2.ClampMagnitude((targetVelocity - rigidbod.velocity) * rigidbod.mass, maxForce * Time.fixedDeltaTime);
        rigidbod.AddForce(appliedImpulse, ForceMode2D.Impulse);

        if (targetVelocity[0] > 0)
        {
            Enemy.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            Enemy.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }



        if (range >= 20f)
        {
            speed = 15f;
        }
        else if (range < 10f)
        {
            speed = 10f;
        }

        if (collidedwPlayer)
        {
            enemyCurrentHealth -= 1f;
        }

        if (enemyCurrentHealth <= 0f)
        {
            Destroy(this.gameObject);
        }



    }
}
