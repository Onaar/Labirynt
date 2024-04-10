using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    public int timeToEnd, points = 0, redKey = 0, greenKey = 0, goldKey = 0;
    [SerializeField]
    KeyCode pauseKey = KeyCode.P;
    bool isGamePaused, endGame, win; // bool isGamePaused = false; // the same
    AudioSource audioSource;
    public AudioClip pauseClip, resumeClip, winClip, loseClip, pickedClip;

    private void Start()
    {
        if (instance == null)
            instance = this;

        if (timeToEnd <= 0)
            timeToEnd = 90;

        audioSource = GetComponent<AudioSource>();
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
    public void AddPoints(int point = 1)
    {
        points += point;
    }
    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
    }
    public void FreezeTime(int freezeTime)
    {
        CancelInvoke("Timer");
        InvokeRepeating("Timer", freezeTime, 1);
    }
    public void AddKey(KeyColor keyColor)
    {
        switch (keyColor)
        {
            case KeyColor.Red:
                redKey++;
                break;
            case KeyColor.Gold:
                goldKey++;
                break;
            case KeyColor.Green:
                greenKey++;
                break;
            default:
                Debug.LogWarning($"KeyColor: {keyColor} is not supported;");
                break;
        }
    }
}
