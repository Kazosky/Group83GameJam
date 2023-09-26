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


    void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        Player = GameObject.FindGameObjectWithTag("Player");
        enemySpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        range = Vector2.Distance(Enemy.transform.position, Player.transform.position);
        Vector2 velocity = new Vector2((transform.position.x - Player.transform.position.x) * enemySpeed, (transform.position.y - Player.transform.position.y) * enemySpeed);
        GetComponent<Rigidbody2D>().velocity = -velocity;

        if (range >= 10f)
        {
            enemySpeed = 5f;
            
        }

        if (range < 10f)
        {
            enemySpeed = 2f;
        }
    }
}
