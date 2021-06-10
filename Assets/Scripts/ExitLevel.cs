using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundtrackPlayer;
using TMPro;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> AliveEnemies;
    [SerializeField]
    private GameObject MessageObject;

    [SerializeField]
    private EndMenu EndMenu;
    [SerializeField]
    private TextMeshProUGUI resultLabel;

    private IEnumerator ShowMessage(string message)
    {
        TextMeshProUGUI label = MessageObject.GetComponent<TextMeshProUGUI>();
        label.text = message;
        MessageObject.SetActive(true);
        yield return new WaitForSeconds(5);
        MessageObject.SetActive(false);
    }

    private void Win()
    {
        float time = FindObjectOfType<Timer>().Stop();
        resultLabel.text = "Your time: " + (int)time + " seconds.";
        string scoreName = "score" + (SceneManager.GetActiveScene().buildIndex).ToString();

        if (PlayerPrefs.GetInt(scoreName) > (int)time || PlayerPrefs.GetInt(scoreName) <= 0)
        {
            PlayerPrefs.SetInt(scoreName, (int)time);
        }

        Debug.Log(scoreName + " = " + PlayerPrefs.GetInt(scoreName) + " nickname = " + PlayerPrefs.GetString("nickname"));
        EndMenu.Win();
    }

    void Start()
    {
        AliveEnemies.AddRange(FindObjectsOfType<Enemy>());
        SoundtrackPlayerInstance.ChangeClip("Main theme");
        Debug.Log("score = " + PlayerPrefs.GetInt("score" + (SceneManager.GetActiveScene().buildIndex ).ToString()) + " nickname = " + PlayerPrefs.GetString("nickname"));
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(AliveEnemies.FindAll(enemy => enemy != null).Count == 0)
            {
                Win();
            }
            else
            {
                StartCoroutine(ShowMessage("Kill all enemies first"));
            }
        }
    }
}
