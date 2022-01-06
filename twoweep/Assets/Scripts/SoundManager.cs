using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioSource btnSource;

    private void Start()
    {
        musicSource.volume = 0.1f;
        btnSource.volume = 0.1f;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetButtonVolume(float volume)
    {
        btnSource.volume = volume;
    }

    public void OnSfx()
    {
        btnSource.Play();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(musicSource);
        DontDestroyOnLoad(btnSource);
    }


}
