using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    //Positive values heal and negetive values hurt
    public void ChangeHealth(float amount)
    {
        health += amount;
        if(health <= 0f)
        {
            Destroy(this.gameObject);
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
