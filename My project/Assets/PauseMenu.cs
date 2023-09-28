using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject upgradesMenuUI;

    // Upgrade screen parts
    // Upgrade 1:
    public Slider slider1;
    public TMP_Text slider1Text;

    // Upgrade 2:
    public Slider slider2;
    public TMP_Text slider2Text;

    // Upgrade 3:
    public Slider slider3;
    public TMP_Text slider3Text;

    // Wave UI
    public TMP_Text waveText;

    public static string sceneName = "SampleScene";

    void Start()
    {
        pauseMenuUI.SetActive(false);
        upgradesMenuUI.SetActive(false);
        slider1.enabled = false;
        waveText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }

        if (upgradesMenuUI.activeSelf)
        {
            // Sets the text variables to the value of the slider corrisponding to that variable.
            slider1Text.text = slider1.value.ToString();
            slider2Text.text = slider2.value.ToString();
            slider3Text.text = slider3.value.ToString();
        }

        if (SceneManager.GetActiveScene().name == sceneName && GameIsPaused == false) 
        {
            waveText.enabled = true;
        } else
        {
            waveText.enabled = false;
        }
    }

    // Main pause screen. \\

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }



    public void loadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");

    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


    // Waves \\

    public void ShowWaves()
    {
        waveText.enabled = true;
    }

    public void RemoveWaves()
    {
        waveText.enabled = false;
    }


    public void WaveCounterUpdate()
    {
        int currentWaveNumber;
        // Gets the last 2 digits of the wave to reduce the amount of selection required.
        int.TryParse(waveText.text.Substring(waveText.text.Length - 2).ToString(), out currentWaveNumber);

        waveText.text = "WAVE: "+((currentWaveNumber+1).ToString());
    }
}
