using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChangeOverTime : MonoBehaviour
{
    Health health;
    public float healthChangePerSecond = -10f;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        health.ChangeHealth(healthChangePerSecond * Time.fixedDeltaTime);
    }
}
