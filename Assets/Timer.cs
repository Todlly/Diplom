using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time = 0;
    private TextMeshProUGUI label;
    private bool IsRunning;

    private void Start()
    {
        label = GetComponent<TextMeshProUGUI>();
        IsRunning = true;
    }

    public float Stop()
    {
        IsRunning = false;
        return time;
    }

    void Update()
    {
        if (IsRunning)
        {
            time += Time.deltaTime;
            label.text = time.ToString("F1") + "s";
        }
    }
}
