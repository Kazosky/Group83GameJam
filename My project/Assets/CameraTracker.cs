using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public GameObject target;
    public Camera camera;
    Rect bounds;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        BoxCollider2D box = GetComponentInParent<BoxCollider2D>();
        bounds = new Rect(box.offset - box.size * 0.5f, box.size);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position + Vector3.back * 10f, Time.deltaTime);
        if(camera.ViewportToWorldPoint(Vector3.zero).x < bounds.position.x)
        {
            this.transform.position -= Vector3.right * (camera.ViewportToWorldPoint(Vector3.zero).x - bounds.position.x);
        }
        else if (camera.ViewportToWorldPoint(Vector3.right).x > bounds.position.x + bounds.size.x)
        {
            this.transform.position -= Vector3.right * (camera.ViewportToWorldPoint(Vector3.right).x - (bounds.position.x + bounds.size.x));
        }

        if (camera.ViewportToWorldPoint(Vector3.zero).y < bounds.position.y)
        {
            this.transform.position -= Vector3.up * (camera.ViewportToWorldPoint(Vector3.zero).y - bounds.position.y);
        }
        else if (camera.ViewportToWorldPoint(Vector3.up).y > bounds.position.y + bounds.size.y)
        {
            this.transform.position -= Vector3.up * (camera.ViewportToWorldPoint(Vector3.up).y - (bounds.position.y + bounds.size.y));
        }
    }
}
