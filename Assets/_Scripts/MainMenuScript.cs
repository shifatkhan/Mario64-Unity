using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu screen manager
/// 
/// @author ShifatKhan
/// </summary>
public class MainMenuScript : MonoBehaviour
{
    public void PlayClicked()
    {
        SceneManager.LoadScene("BobombBattlefield");
    }

    public void ExitClicked()
    {
        Application.Quit();
    }
}
