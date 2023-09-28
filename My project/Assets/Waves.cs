using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemies;
    public GameObject[] spawnPoints;
    public TMP_Text waveText;
    int currentWave = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.transform.childCount == 0)
        {
            currentWave++;

            WaveCounterUpdate();

            int numEnemies = Mathf.RoundToInt(Mathf.Log(currentWave + 0.2f, 1.2f));
            for (int i = 0; i < numEnemies; i++)
            {
                Vector3 postion = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].transform.position;
                var currentEnemy = Instantiate(enemy, enemies.transform);
                currentEnemy.transform.position = postion;
            }
        }
    }

    private void WaveCounterUpdate()
    {
        // Update the wave counter +1 each time called


        int currentWaveNumber;
        // Gets the last 2 digits of the wave to reduce the amount of selection required.
        int.TryParse(waveText.text.Substring(waveText.text.Length - 2).ToString(), out currentWaveNumber);

        waveText.text = "WAVE: " + ((currentWaveNumber + 1).ToString());
    }
}
