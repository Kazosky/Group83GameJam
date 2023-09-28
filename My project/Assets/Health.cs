using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        explosion.SetActive(false);
    }

   

    //Positive values heal and negetive values hurt
    public void ChangeHealth(float amount)
    {
        health += amount;
        if(health <= 0f)
        {
            // Do some sort of explosion

            explosion.SetActive(true);
            Invoke("stopExplosion", 0.9f);
            Invoke("finishDeath", 0.5f);

        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void stopExplosion()
    {
        explosion.SetActive(false);
    }

    private void finishDeath()
    {
        Destroy(this.gameObject);

        if (this.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
