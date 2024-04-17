using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] playlist;
    int currentClip = 0;
    float pauseClipTime = -1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = playlist[currentClip];
        audioSource.Play();
    }
    private void Update()
    {
        if(audioSource.time >= playlist[currentClip].length)
        {
            currentClip++;
            if(currentClip > playlist.Length - 1)
            {
                currentClip = 0;
            }
            audioSource.clip = playlist[currentClip];
            audioSource.Play();
        }
    }
    public void OnPauseGame()
    {
        pauseClipTime = audioSource.time;
        audioSource.Pause();
    }
    public void OnResumeGame()
    {
        audioSource.PlayScheduled(pauseClipTime);
        pauseClipTime = -1f;
    }
}
