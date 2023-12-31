using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform Target;
    public GameObject Enemy;
    public GameObject Player;
    private float range;
    private Rigidbody2D rigidbody;
    private Vector2 targetVelocity = Vector2.zero;
    public float speed = 20f;
    public float maxForce = 20f;
    public bool collidedwPlayer;
    public float damagePerSecond = 10f;

    void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        Player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody2D>();
        collidedwPlayer = false;
    }

    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.SetPositions(new Vector3[] { this.transform.position + Vector3.back, Player.transform.position + Vector3.back });
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidedwPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidedwPlayer = false;
        }
    }

    private void FixedUpdate()
    {
        try
        {


            range = Vector2.Distance(Enemy.transform.position, Player.transform.position);
            targetVelocity = (Player.transform.position - transform.position).normalized * speed;
            Vector2 appliedImpulse = Vector2.ClampMagnitude((targetVelocity - rigidbody.velocity) * rigidbody.mass, maxForce * Time.fixedDeltaTime);
            rigidbody.AddForce(appliedImpulse, ForceMode2D.Impulse);

            if (targetVelocity[0] > 0)
            {
                Enemy.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                Enemy.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }




            if (range >= 10f)
            {
                speed = 10f;
            }
            else if (range < 10f)
            {
                speed = 4f;
            }

            if (collidedwPlayer)
            {
                Player.GetComponent<Health>().ChangeHealth(-damagePerSecond * Time.fixedDeltaTime);
            }
        } catch
        {
            Debug.Log("Error");
        }
    }
}