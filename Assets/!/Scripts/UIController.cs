using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public bool gameRunning = true;

    void Update()
    {
        PauseGame();
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton7) && gameRunning == true)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
            gameRunning = false;
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton7) && gameRunning == false)
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1f;
            gameRunning = true;
        }
    }
}
