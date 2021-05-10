using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundtrackPlayer;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public GameObject MainMenuObj, SettingsMenuObj;

    private void Start()
    {
        AudioMixer = Resources.Load<AudioMixer>("MainAudioMixer");
        AudioSource soundtrack = SoundtrackPlayerInstance.GetComponent<AudioSource>();

        if (soundtrack.isPlaying)
            soundtrack.Stop();

        soundtrack.clip = SoundtrackPlayerInstance.tracks[1];
        soundtrack.Play();
    }

    public void SwitchToMainMenu()
    {
        SettingsMenuObj.SetActive(false);
        MainMenuObj.SetActive(true);
    }

    public void SwitchToSettingsMenu()
    {
        MainMenuObj.SetActive(false);
        SettingsMenuObj.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void AdjustSoundsVolume(float value)
    {
        AudioMixer.SetFloat("Volume of Sounds", Mathf.Log10(value) * 20);
    }

    public void AdjustMusicVolume(float value)
    {
        AudioMixer.SetFloat("Volume of Music", Mathf.Log10(value) * 20);
    }

    public void AdjustMasterVolume(float value)
    {
        AudioMixer.SetFloat("Volume of Master", Mathf.Log10(value) * 20);
    }
}
