using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Vector2 targetVelocity = Vector2.zero;
    public float speed = 20f;
    public float maxForce = 20f;
    public GameObject projectilePrefab;
    public float fireForce = 2000;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 fireDirection = ((Vector3)mousePosition - transform.position).normalized;
            Vector2 projectilePosition = transform.position + fireDirection * 1;
            GameObject projectile = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
            projectile.GetComponent<Projectile>().player = this.gameObject;
            projectile.GetComponent<Rigidbody2D>().AddForce(fireDirection * fireForce);
            projectile.GetComponent<SpringJoint2D>().connectedBody = rigidbody;
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 inputVector = new Vector2((float)horizontalInput, (float)verticalInput).normalized;
        targetVelocity = inputVector * speed;
        Vector2 appliedImpulse = Vector2.ClampMagnitude((targetVelocity - rigidbody.velocity) * rigidbody.mass, maxForce * Time.fixedDeltaTime);
        rigidbody.AddForce(appliedImpulse, ForceMode2D.Impulse);
    }
}
