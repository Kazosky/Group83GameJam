using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject upgradesMenuUI;

    // Upgrade screen parts

    public TMP_Text upgradePoints;

    // Upgrade 1:
    public Slider slider1;
    public TMP_Text slider1Text;
    public GameObject upgradeButton1;

    // Upgrade 2:
    public Slider slider2;
    public TMP_Text slider2Text;
    public GameObject upgradeButton2;

    // Upgrade 3:
    public Slider slider3;
    public TMP_Text slider3Text;
    public GameObject upgradeButton3;

    // Wave UI
    public TMP_Text waveText;

    // Scene name to check if the main game is active. Change if scene name is changed. \\
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
        if (SceneManager.GetActiveScene().name == sceneName)
        {

            // Checks for when the escape key is pressed to pause the game.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

            if (!waveText.IsActive() && !upgradesMenuUI.activeSelf)
            {
                ShowWaves();
            }
        }
    }

    // Main pause screen. \\

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        upgradesMenuUI.SetActive(false);
        ShowWaves();
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
        // Update the wave counter +1 each time called


        int currentWaveNumber;
        // Gets the last 2 digits of the wave to reduce the amount of selection required.
        int.TryParse(waveText.text.Substring(waveText.text.Length - 2).ToString(), out currentWaveNumber);

        waveText.text = "WAVE: "+((currentWaveNumber+1).ToString());
    }


    // Update upgrades \\
    // Changes the upgrade slider alongside the text beside the number \\

    public void UpgradeChange(int upgradeID )
    {
        if (upgradeID == 1)
        {
            slider1.value = slider1.value + 10 ;
            slider1Text.text = slider1.value.ToString();
        }
        else if (upgradeID == 2)
        {
            slider2.value = slider2.value + 10;
            slider2Text.text = slider2.value.ToString();
        }
        else if (upgradeID == 3)
        {
            slider3.value = slider3.value + 10;
            slider3Text.text = slider3.value.ToString();
        }
    }

    public void updateUpgrade( int buttonID )
    {

        // Get current points avaliable 

        int currentPointsAvaliable = getUpgradePointsAvaliable();
        if (currentPointsAvaliable > 9)
        {
            UpgradeChange(buttonID);
            updateUpgradePointsText(false, 10);

        } 
    }

    public int getUpgradePointsAvaliable()
    {
        // Get current points avaliable and returns an int. \\

        int currentPointsAvaliable;
        // Gets the last 2 digits of the wave to reduce the amount of selection required.
        int.TryParse(upgradePoints.text.Substring(upgradePoints.text.Length - 2).ToString(), out currentPointsAvaliable);

        return currentPointsAvaliable;
    }


    public void updateUpgradePointsText(bool addition, int number)
    {
        // Updates the text on the upgrades screen. Addition: true = add on the number | Addition: false = remove the number\\

        int currentPointsAvaliable;
        int upgradeNewNumber;

        // Gets the last 2 digits of the wave to reduce the amount of selection required.
        int.TryParse(upgradePoints.text.Substring(upgradePoints.text.Length - 2).ToString(), out currentPointsAvaliable);

        if (addition)
        {
            upgradeNewNumber = currentPointsAvaliable + number;
        } else
        {
            upgradeNewNumber = currentPointsAvaliable - number;
        }
        
        upgradePoints.text = "Points: " + upgradeNewNumber.ToString();

    }
}
