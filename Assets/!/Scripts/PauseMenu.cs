using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public UIController uIController;

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void Return()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
        uIController.gameRunning = true;
    }
}
