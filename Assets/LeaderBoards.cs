using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;

public class LeaderBoards : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] fields;
    [SerializeField]
    private TextMeshProUGUI levelLabel;

    private string serverURL = "http://localhost/leaderboard";
    private int currentLevel = 1;
    private int levelsCount = 2;
    private int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
            levelLabel.text = "Level " + currentLevel;
        }
    }

    public void SetHighscore()
    {
        string nickname = "";
        if (PlayerPrefs.HasKey("nickname"))
            nickname = PlayerPrefs.GetString("nickname");
        else
            return;
        string score = "";
        if (PlayerPrefs.HasKey("score" + CurrentLevel))
            score = PlayerPrefs.GetInt("score" + CurrentLevel).ToString();
        else
            return;
        Debug.Log("Nickname " + nickname + ", score" + CurrentLevel + " " + score);
        string level = CurrentLevel.ToString();

        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("nickname", nickname));
        form.Add(new MultipartFormDataSection("score", score));
        form.Add(new MultipartFormDataSection("level", level));

        UnityWebRequest request = UnityWebRequest.Post(serverURL + "/set_score.php", form);

        

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string[] pairs = request.downloadHandler.text.Split(';');
            bool foundPlayer = false;

            for (int i = 0; i < pairs.Length; i++)
            {
                if (pairs[i].Split(':')[0] == nickname)
                {
                    fields[9].text = (i + 1).ToString();
                    fields[10].text = nickname;
                    fields[11].text = pairs[i].Split(':')[1];
                    foundPlayer = true;
                    break;
                }
            }
        }
    }

    public void GetLeaderboards()
    {
        StartCoroutine(LeaderboardRequest(CurrentLevel + ""));
        GetPlayerHighscore();
    }

    public void GetPlayerHighscore()
    {
        if (PlayerPrefs.HasKey("nickname"))
        {
            StartCoroutine(GetPlayerCoroutine(PlayerPrefs.GetString("nickname")));
        }
        else
        {
            Debug.Log("Nickname not set");
        }
    }

    private IEnumerator GetPlayerCoroutine(string nickname)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("nickname", nickname));
        form.Add(new MultipartFormDataSection("level", CurrentLevel.ToString()));

        UnityWebRequest request = UnityWebRequest.Post(serverURL + "/get_scores.php", form);

        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string[] pairs = request.downloadHandler.text.Split(';');
            bool foundPlayer = false;

            for (int i = 0; i < pairs.Length; i++)
            {
                if (pairs[i].Split(':')[0] == nickname)
                {
                    fields[9].text = (i + 1).ToString();
                    fields[10].text = nickname;
                    fields[11].text = pairs[i].Split(':')[1];
                    foundPlayer = true;
                    break;
                }
            }

            if (!foundPlayer)
            {
                fields[9].text = "?";
                fields[10].text = PlayerPrefs.HasKey("nickname") ? PlayerPrefs.GetString("nickname") : "?";
                fields[11].text = "?";
            }
        }
    }

    private IEnumerator LeaderboardRequest(string level)
    {
        try
        {
            SetHighscore();
        } catch 
        {

        }

        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("level", level));

        UnityWebRequest request = UnityWebRequest.Post(serverURL + "/get_scores.php", form);

        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            UpdateLeaderboard(request.downloadHandler.text);
        }
    }

    private void UpdateLeaderboard(string answer)
    {
        string[] pairs = answer.Split(';');
        for (int i = 0; i < 3; i++)
        {
            string[] parts = pairs[i].Split(':');
            fields[i * 3].text = (i + 1).ToString();
            fields[i * 3 + 1].text = parts[0];
            fields[i * 3 + 2].text = parts[1];
        }
    }

    public void NextLevel()
    {
        if (CurrentLevel + 1 <= levelsCount)
        {
            CurrentLevel++;
            GetLeaderboards();
        }
    }

    public void PrevLevel()
    {
        if (CurrentLevel - 1 > 0)
        {
            CurrentLevel--;
            GetLeaderboards();
        }
    }



    private void OnEnable()
    {
        CurrentLevel = 1;
        GetLeaderboards();
    }
}
