using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject player;
    public float onHitDamage = 10;
    public float damagePerSecond = 10;
    public float pullDistance = 7;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.SetPositions(new Vector3[] { this.transform.position + Vector3.back, player.transform.position + Vector3.back });
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<SpringJoint2D>() == null) // second part checks if the enemy has already been hit
        {

            GameObject enemy = collision.gameObject;
            enemy.GetComponent<Health>().ChangeHealth(-onHitDamage);
            SpringJoint2D spring = enemy.AddComponent<SpringJoint2D>();
            spring.connectedBody = player.GetComponent<Rigidbody2D>();
            spring.autoConfigureDistance = false;
            spring.distance = pullDistance;
            spring.frequency = 3f;
            spring.enabled = false;

            SpringLimiter springLimiter = enemy.AddComponent<SpringLimiter>();
            springLimiter.minDistance = pullDistance;
            springLimiter.spring = spring;
            springLimiter.preAttachVelocity = enemy.GetComponent<Rigidbody2D>().velocity;

            HealthChangeOverTime healthChangeOverTime = enemy.AddComponent<HealthChangeOverTime>();
            healthChangeOverTime.healthChangePerSecond = -damagePerSecond;

            enemy.GetComponent<Rigidbody2D>().AddForce(this.GetComponent<Rigidbody2D>().velocity * this.GetComponent<Rigidbody2D>().mass, ForceMode2D.Impulse);

            LineRenderer line = enemy.AddComponent<LineRenderer>();
            line.SetWidth(0.2f, 0.2f);
            line.material = GetComponent<LineRenderer>().material;

            Destroy(this.gameObject);
        }
    }
}
