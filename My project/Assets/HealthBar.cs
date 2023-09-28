using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Health health;
    Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponentInParent<Health>();
        healthSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health.health / health.maxHealth;
    }
}
