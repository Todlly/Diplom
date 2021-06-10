using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LeaderBoards;
using UnityEngine.UI;

public class ChangeServer : MonoBehaviour
{
    public InputField field;
    public GameObject AdminPanel;

    public void SwitchServer()
    {
        serverURL = field.text;
        AdminPanel.SetActive(false);
    }
}
