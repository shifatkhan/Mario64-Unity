using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Pause screen manager
/// 
/// @author ShifatKhan
/// </summary>
public class PauseScript : MonoBehaviour
{
    public GameObject pauseScreen;
    public static bool isPaused;

    public Text yellowCoinTxt;
    public Text redCoinTxt;
    public Text totalCoinTxt;

    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseScreen.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;

        yellowCoinTxt.text = GameManager.normalCoins.ToString();
        redCoinTxt.text = GameManager.redCoins.ToString();
        totalCoinTxt.text = GameManager.coins.ToString();
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void TryAgainClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
