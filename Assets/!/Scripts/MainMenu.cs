using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsFirstButton, optionsClosedButton, powerUpFirstButton, powerUpClosedButton;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenPowerUp() 
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(powerUpFirstButton);
    }

    public void ClosePowerUp()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(powerUpClosedButton);
    }

    public void OpenOptions() 
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
