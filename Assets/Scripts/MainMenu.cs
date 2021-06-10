using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundtrackPlayer;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public GameObject MainMenuObj, SettingsMenuObj, LeaderboardsObj, NicknameInputObj;

    public Slider SoundsSlider, MusicSlider, MasterSlider;
    public GameObject AdminPanel;

    [SerializeField]
    private TMP_InputField inputField;

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

    public void SetNickname()
    {
        if (inputField.text != "")
        {
            PlayerPrefs.SetString("nickname", inputField.text);
            FindObjectOfType<LeaderBoards>().UpdateDatabase();
            SwitchToLeaderboards();
        }
    }

    public void SwitchToLeaderboards()
    {
        MainMenuObj.SetActive(false);
        LeaderboardsObj.SetActive(true);
        NicknameInputObj.SetActive(false);
    }

    public void SwitchToMainMenu()
    {
        SettingsMenuObj.SetActive(false);
        LeaderboardsObj.SetActive(false);
        MainMenuObj.SetActive(true);
    }

    public void SwitchToSettingsMenu()
    {
        MainMenuObj.SetActive(false);
        SettingsMenuObj.SetActive(true);
    }

    public void SwitchToSetNickname()
    {
        NicknameInputObj.SetActive(true);
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

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.O)
        {
            AdminPanel.SetActive(true);
        }
    }
}
