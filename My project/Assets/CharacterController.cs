using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rigidbod;
    private Vector2 targetVelocity = Vector2.zero;
    public float speed = 20f;
    public float maxForce = 20f;
    public GameObject projectilePrefab;
    public float fireForce = 2000;
    public float fireCooldown = 0.2f;
    float currentFireCooldown = 0;

    public GameObject character;
    bool facing = false; // False left, true right

    // Start is called before the first frame update
    void Start()
    {
        rigidbod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentFireCooldown -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && !PauseMenu.GameIsPaused && currentFireCooldown < 0f)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 fireDirection = ((Vector3)mousePosition - transform.position).normalized;
            Vector2 projectilePosition = transform.position + fireDirection * 1;
            GameObject projectile = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
            projectile.GetComponent<Projectile>().player = this.gameObject;
            projectile.GetComponent<Rigidbody2D>().AddForce(fireDirection * fireForce);
            projectile.GetComponent<SpringJoint2D>().connectedBody = rigidbod;
            currentFireCooldown = fireCooldown;
        }

        if (Input.GetKeyDown(KeyCode.D) && !facing)
        {
            // If the "D" key is pressed and the facing variable is false (facing left)
            character.transform.localRotation = Quaternion.Euler(0, 180, 0);
            facing = !facing;

        } else if (Input.GetKeyDown(KeyCode.A) && facing)
        {
            // If the "E" key is pressed and the facing variable is true (facing right)
            character.transform.localRotation = Quaternion.Euler(0, 0, 0);
            facing = !facing;
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 inputVector = new Vector2((float)horizontalInput, (float)verticalInput).normalized;
        targetVelocity = inputVector * speed;
        Vector2 appliedImpulse = Vector2.ClampMagnitude((targetVelocity - rigidbod.velocity) * rigidbod.mass, maxForce * Time.fixedDeltaTime);
        rigidbod.AddForce(appliedImpulse, ForceMode2D.Impulse);
    }
}
