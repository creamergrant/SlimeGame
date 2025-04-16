using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    public GameObject[] hearts = new GameObject[3];
    public GameObject exitButton;
    public GameObject resumeButton;

    int health = 3;
    int score = 0;
    public Text scoreText;
    bool paused = false;

    [System.Obsolete]
    public void OnHit()
    {
        health -= 1;
        if (health < 3)
        {
            hearts[health].SetActive(false);
        }
        if (health == 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void IncreaseScore(int pointIncrease)
    {
        score += pointIncrease;
    }

    private void FixedUpdate()
    {
        scoreText.text = score.ToString();
    }

    [System.Obsolete]
    public void Pause(InputAction.CallbackContext value)
    {
        float val = value.ReadValue<float>();
        if(val == 0)
        {
            if(paused)
            {
                ResumeGame();
            }
            else 
            { 
                PauseGame();
            }
        }
    }

    [System.Obsolete]
    void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
        Screen.lockCursor = false;
        exitButton.SetActive(true);
        resumeButton.SetActive(true);
    }

    [System.Obsolete]
    void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
        Screen.lockCursor = true;
        exitButton.SetActive(false);
        resumeButton.SetActive(false);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ResumeButton()
    {
        ResumeGame();
    }

}
