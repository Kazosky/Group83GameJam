using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringLimiter : MonoBehaviour
{
    // Start is called before the first frame update

    public SpringJoint2D spring;
    public float minDistance = 0f;
    public float maxDistance = float.PositiveInfinity;
    public Vector2 preAttachVelocity;

    private void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = preAttachVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = (this.gameObject.transform.position - spring.connectedBody.transform.position).magnitude;
        spring.enabled = distance > minDistance && distance < maxDistance;
    }
}
