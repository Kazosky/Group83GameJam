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
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
