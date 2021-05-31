using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject WinPanel;
    [SerializeField]
    private GameObject DefeatPanel;
    [SerializeField]
    private GameObject EnterNamePanel;
    [SerializeField]
    private TextMeshProUGUI ResultText;

    public void Win()
    {
        WinPanel.SetActive(true);
    }

    public void Defeat()
    {
        DefeatPanel.SetActive(true);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
