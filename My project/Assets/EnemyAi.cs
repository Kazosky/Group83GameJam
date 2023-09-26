using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform Target;
    public GameObject Enemy;
    public GameObject Player;
    private float range;
    public float enemySpeed;
    private Rigidbody2D rigidbody;
    private Vector2 targetVelocity = Vector2.zero;
    public float speed = 20f;
    public float maxForce = 20f;

    void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        Player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody2D>();
        enemySpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        //range = Vector2.Distance(Enemy.transform.position, Player.transform.position);
        //Vector2 velocity = new Vector2((transform.position.x - Player.transform.position.x) * enemySpeed, (transform.position.y - Player.transform.position.y) * enemySpeed);
        //GetComponent<Rigidbody2D>().velocity = -velocity;

        //if (range >= 10f)
        //{
        //    enemySpeed = 5f;
            
        //}

        //if (range < 10f)
        //{
        //    enemySpeed = 2f;
        //}
    }

    private void FixedUpdate()
    {
        targetVelocity = (Player.transform.position - transform.position).normalized * speed;
        Vector2 appliedImpulse = Vector2.ClampMagnitude((targetVelocity - rigidbody.velocity) * rigidbody.mass, maxForce * Time.fixedDeltaTime);
        rigidbody.AddForce(appliedImpulse, ForceMode2D.Impulse);
    }
}
