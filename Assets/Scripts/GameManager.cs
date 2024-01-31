using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    int timeToEnd;
    [SerializeField]
    KeyCode pauseKey = KeyCode.P;
    bool isGamePaused, endGame, win; // bool isGamePaused = false; // the same

    private void Start()
    {
        if (instance == null)
            instance = this;

        if (timeToEnd <= 0)
            timeToEnd = 90;

        InvokeRepeating("Timer", 1f, 1f);
    }
    private void Timer()
    {
        timeToEnd--;
        Debug.Log($"Time to end: {timeToEnd}s");
        if (timeToEnd <= 0)
        {
            endGame = true;
            EndGame();
        }
    }
    private void EndGame()
    { 
        CancelInvoke("Timer");
        if (win) Debug.Log("You win!");
        else Debug.Log("You lose!");
    }
    private void Update()
    {
        CheckGamePause();
    }
    private void CheckGamePause()
    {
        if (!Input.GetKeyDown(pauseKey)) return;
        if (isGamePaused) ResumeGame();
        else PauseGame();
    }
    private void PauseGame()
    {
        Debug.Log("Game paused");
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    private void ResumeGame()
    {
        Debug.Log("Game resumed");
        Time.timeScale = 1f;
        isGamePaused =  false;
    }
}
