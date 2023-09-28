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

    // Scene name to check if the main game is active. Change if scene name is changed. 
    public static string sceneName = "SampleScene";

    void Start()
    {
        // Ran at the start - hides everything that isn't needed to be shown to begin with.

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

            // Checks if the wave text is not active and the upgrades menu isn't active,
            // if conditions are met it sets the waves to visable.
            if (!waveText.IsActive())
            {
                if (!upgradesMenuUI.activeSelf)
                {
                    ShowWaves();
                }
            }

        }
    }

    // Main pause screen. \\

    public void Resume()
    {
        // Resumes the game.

        pauseMenuUI.SetActive(false);
        upgradesMenuUI.SetActive(false);
        ShowWaves();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        // Pauses the game.

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void loadMenu()
    {
        // Loads the main menu.

        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");

    }

    public void QuitGame()
    {
        // Quits the game.

        Debug.Log("Quit");
        Application.Quit();
    }


    // Waves \\

    public void ShowWaves()
    {
        // Shows the waves text.

        waveText.enabled = true;
    }

    public void RemoveWaves()
    {
        // Hides the waves text.

        waveText.enabled = false;
    }

    // upgrades \\

    public void UpgradeChange(int upgradeID )
    {
        // Changes the upgrade slider alongside the text beside the number 

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
        // Makes sure the player has enough points avaliable to update then changes 
        // the slider and avaliable points text displayed to the user.

        int currentPointsAvaliable = getUpgradePointsAvaliable();
        if (currentPointsAvaliable > 9)
        {
            UpgradeChange(buttonID);
            updateUpgradePointsText(false, 10);

        } 
    }

    public int getUpgradePointsAvaliable()
    {
        // Get current points avaliable and returns an int. 

        int currentPointsAvaliable;
        int.TryParse(upgradePoints.text.Substring(upgradePoints.text.Length - 2).ToString(), out currentPointsAvaliable);

        return currentPointsAvaliable;
    }


    public void updateUpgradePointsText(bool addition, int number)
    {
        // Updates the text on the upgrades screen. Addition: true = add on the number | Addition: false = remove the number\\

        int currentPointsAvaliable;
        int upgradeNewNumber;
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
