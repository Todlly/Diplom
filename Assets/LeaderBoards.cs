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

    public static string serverURL = "http://93.100.45.246:18080/leaderboard";
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

    public void UpdateDatabase()
    {
        StartCoroutine(UpdateDatabaseCoroutine());
    }

    private IEnumerator UpdateDatabaseCoroutine() // Обновление базы
    {

        string nickname = "";
        if (PlayerPrefs.HasKey("nickname"))
            nickname = PlayerPrefs.GetString("nickname");

        string score = "";
        if (PlayerPrefs.HasKey("score" + CurrentLevel))
            score = PlayerPrefs.GetInt("score" + CurrentLevel).ToString();

        if (nickname == "")
        {
            Debug.Log("no nickname");
        }
        else if (score == "")
        {
            Debug.Log("no local score");
        }
        else
        {
            Debug.Log("Current nickname is: " + nickname + ", current score for level " + CurrentLevel + " is: " + score);
            string level = CurrentLevel.ToString();

            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            form.Add(new MultipartFormDataSection("nickname", nickname));
            form.Add(new MultipartFormDataSection("score", score));
            form.Add(new MultipartFormDataSection("level", level));

            UnityWebRequest request = UnityWebRequest.Post(serverURL + "/set_score.php", form);

            yield return request.SendWebRequest();

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError("Error : " + request.error);
                foreach(TextMeshProUGUI label in fields)
                {
                    label.text = "???";
                }
                yield break;
            }
            else
            {
                Debug.Log("Got from setting: " + request.downloadHandler.text);
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

        GetLeaderboards();
    }

    public void GetLeaderboards()
    {
        StartCoroutine(GetMainLeaderboard(CurrentLevel + "")); // Получение всей базы
        GetPlayerHighscore(); // Получение данных для текущего игрока
    }

    public void GetPlayerHighscore() // Получение данных об игроке
    {
        if (PlayerPrefs.HasKey("nickname")) // Если задан никнейм
        {
            StartCoroutine(GetPlayerCoroutine(PlayerPrefs.GetString("nickname"))); // Отправить запрос
        }
        else
        {
            Debug.Log("Nickname not set"); // Если никнейм не занят, предупредить об этом
        }
    }

    private IEnumerator GetPlayerCoroutine(string nickname) // Запрос на данные об игроке
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("nickname", nickname));
        form.Add(new MultipartFormDataSection("level", CurrentLevel.ToString()));

        UnityWebRequest request = UnityWebRequest.Post(serverURL + "/get_scores.php", form); // Сборка запроса на сервер

        yield return request.SendWebRequest(); // Отправление запроса на сервер

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        else // Обработка ответа сервера
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

    private IEnumerator GetMainLeaderboard(string level) // Запрос данных об уровне
    {

        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("level", level));

        UnityWebRequest request = UnityWebRequest.Post(serverURL + "/get_scores.php", form); // Сборка запроса на сервер

        yield return request.SendWebRequest(); // Запрос на сервер

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Got from getting: " + request.downloadHandler.text);
            PaintLeaderboard(request.downloadHandler.text); // Обновление таблицы
        }
    }

    private void PaintLeaderboard(string answer) // Обновление таблицы из ответа сервера
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
            StartCoroutine(UpdateDatabaseCoroutine());
        }
    }

    public void PrevLevel()
    {
        if (CurrentLevel - 1 > 0)
        {
            CurrentLevel--;
            StartCoroutine(UpdateDatabaseCoroutine());
        }
    }

    private void OnEnable()
    {
        CurrentLevel = 1;
        StartCoroutine(UpdateDatabaseCoroutine());
    }
}
