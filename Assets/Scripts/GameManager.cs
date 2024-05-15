using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

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
    MusicManager musicManager;
    [SerializeField]
    PostProcessProfile normalProfile, shortOnTimeProfile;
    [SerializeField]
    PostProcessVolume volume;
    bool isLessTimeEffectOn = false;
    const int LESS_TIME_EFFECT_THREASHOLD = 20;

    [SerializeField]
    Text timeText, pointsText, redKeysText, greenKeysText, goldKeysText, infoText;
    [SerializeField]
    GameObject gamePanel, snowFlakeImage, reloadText, infoPanel;

    private void Start()
    {
        if (instance == null)
            instance = this;

        if (timeToEnd <= 0)
            timeToEnd = 90;
        
        InitUI();

        volume.profile = normalProfile;

        audioSource = GetComponent<AudioSource>();
        musicManager = FindObjectOfType<MusicManager>();
        InvokeRepeating("Timer", 1f, 1f);
    }

    private void InitUI()
    {
        UpdateTimeToEndOnCanvas();
        pointsText.text = points.ToString();
        redKeysText.text = redKey.ToString();
        greenKeysText.text = greenKey.ToString();
        goldKeysText.text = goldKey.ToString();
        infoText.text = "GAME PAUSED";
        gamePanel.SetActive(false);
        snowFlakeImage.SetActive(false);
        reloadText.SetActive(false);
        infoPanel.SetActive(false);
    }

    private void Timer()
    {
        timeToEnd--;
        UpdateTimeToEndOnCanvas();
        Debug.Log($"Time to end: {timeToEnd}s");
        if (timeToEnd <= 0)
        {
            endGame = true;
            EndGame();
        }

        if(timeToEnd <= LESS_TIME_EFFECT_THREASHOLD && !isLessTimeEffectOn)
        {
            LessTimeOn();
            isLessTimeEffectOn = true;
        }
        if(timeToEnd > LESS_TIME_EFFECT_THREASHOLD && isLessTimeEffectOn)
        {
            LessTimeOff();
            isLessTimeEffectOn = false;
        }
    }
    private void EndGame()
    { 
        CancelInvoke("Timer");
        if (win)
        {
            Debug.Log("You win!");
            infoText.text = "You win!";
            PlayClip(winClip);
        }
        else { 
            Debug.Log("You lose!");
            infoText.text = "You lose!";
            PlayClip(loseClip);
        }
        infoPanel.SetActive(true);
        reloadText.SetActive(true);
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
        infoPanel.SetActive(true);
        PlayClip(pauseClip);
        Time.timeScale = 0f;
        isGamePaused = true;
        musicManager.OnPauseGame();
    }
    private void ResumeGame()
    {
        Debug.Log("Game resumed");
        infoPanel.SetActive(false);
        PlayClip(resumeClip);
        Time.timeScale = 1f;
        isGamePaused =  false;
        musicManager.OnResumeGame();
    }
    public void AddPoints(int point = 1)
    {
        points += point;
        pointsText.text = points.ToString();
    }
    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
        UpdateTimeToEndOnCanvas();
    }
    public void FreezeTime(int freezeTime)
    {
        CancelInvoke("Timer");
        snowFlakeImage.SetActive(true);
        Invoke(nameof(DeactiveSnowflake), freezeTime);
        InvokeRepeating("Timer", freezeTime, 1);
    }
    public void DeactiveSnowflake()
    {
        snowFlakeImage.SetActive(false);
    }
    public void AddKey(KeyColor keyColor)
    {
        switch (keyColor)
        {
            case KeyColor.Red:
                redKey++;
                redKeysText.text = redKey.ToString();
                break;
            case KeyColor.Gold:
                goldKey++;
                goldKeysText.text = goldKey.ToString();
                break;
            case KeyColor.Green:
                greenKey++;
                greenKeysText.text = greenKey.ToString();
                break;
            default:
                Debug.LogWarning($"KeyColor: {keyColor} is not supported;");
                break;
        }
    }
    public void PlayClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    private void LessTimeOn()
    {
        musicManager.ChangePitch(1.6f);
        volume.profile = shortOnTimeProfile;
    }
    private void LessTimeOff()
    {
        musicManager.ChangePitch(1f);
        volume.profile = normalProfile;
    }
    public void SetGamePanelActiveness(bool isActive)
    {
        gamePanel.SetActive(isActive);
    }
    public void UpdateKeysText(KeyColor keyColor)
    {
        switch (keyColor)
        {
            case KeyColor.Red:
                redKeysText.text = redKey.ToString();
                break;
            case KeyColor.Green:
                greenKeysText.text = greenKey.ToString();
                break;
            case KeyColor.Gold:
                goldKeysText.text = goldKey.ToString();
                break;
        }
    }
    public void UpdateTimeToEndOnCanvas()
    {
        int seconds = timeToEnd % 60;
        int minutes = (timeToEnd - seconds) / 60;
        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}