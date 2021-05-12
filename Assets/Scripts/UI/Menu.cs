using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SoundtrackPlayer;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public AudioMixer GlobalAudioMixer;

    [SerializeField]
    private GameObject MainMenu;
    [SerializeField]
    private GameObject SettingsMenu;

    public Slider SoundsSlider, MusicSlider, MasterSlider;


    private bool IsMenuOpened = false;

    private void Start()
    {
        AudioMixer AudioMixer = Resources.Load<AudioMixer>("MainAudioMixer");
        AudioMixer.GetFloat("Volume of Sounds", out float soundsV);
        AudioMixer.GetFloat("Volume of Music", out float musicV);
        AudioMixer.GetFloat("Volume of Master", out float masterV);
        AdjustSoundsVolume(Mathf.Pow(10, soundsV / 20));
        MusicSlider.value = Mathf.Pow(10, musicV / 20);
        MasterSlider.value = Mathf.Pow(10, masterV / 20);
    }

    public void OpenSettingsMenu()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        IsMenuOpened = true;
    }

    public void CloseMainMenu()
    {
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(false);
        IsMenuOpened = false;
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (IsMenuOpened)
                CloseMainMenu();
            else
                OpenMainMenu();
        }
    }

    public void AdjustSoundsVolume(float value)
    {
        GlobalAudioMixer.SetFloat("Volume of Sounds", Mathf.Log10(value) * 20);
    }

    public void AdjustMusicVolume(float value)
    {
        GlobalAudioMixer.SetFloat("Volume of Music", Mathf.Log10(value) * 20);
    }

    public void AdjustMasterVolume(float value)
    {
        GlobalAudioMixer.SetFloat("Volume of Master", Mathf.Log10(value) * 20);
    }
}
