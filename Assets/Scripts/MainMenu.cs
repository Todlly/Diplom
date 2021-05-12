using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundtrackPlayer;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public GameObject MainMenuObj, SettingsMenuObj;

    public Slider SoundsSlider, MusicSlider, MasterSlider;


    private void Start()
    {
        AudioMixer = Resources.Load<AudioMixer>("MainAudioMixer");

        AudioMixer.GetFloat("Volume of Sounds", out float soundsV);
        AudioMixer.GetFloat("Volume of Music", out float musicV);
        AudioMixer.GetFloat("Volume of Master", out float masterV);
        SoundsSlider.value = Mathf.Pow(10, soundsV / 20);
        MusicSlider.value = Mathf.Pow(10, musicV / 20);
        MasterSlider.value = Mathf.Pow(10, masterV / 20);

        SoundtrackPlayerInstance.ChangeClip("Menu theme");
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
