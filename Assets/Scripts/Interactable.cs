using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public enum InteractableType
    {
        Minion, Loot
    }

    public InteractableType Type;
    private Image SelectionFrame;
    public GameObject SelectionDetector;
    public bool isSelected = false;
    public Sprite Icon;

    void Start()
    {
        SelectionFrame = GameObject.Find("TargetFrame").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectionDetector.SetActive(isSelected);
    }

    public void Select()
    {
        isSelected = true;
        SelectionFrame.sprite = Icon;
        SetFrameAlpha(1f);
    }

    public void Deselect()
    {
        isSelected = false;
        SelectionFrame.sprite = null;
        SetFrameAlpha(0f);
    }

    private void SetFrameAlpha(float value)
    {
        Color tmpColor = SelectionFrame.color;
        tmpColor.a = value;
        SelectionFrame.color = tmpColor;
    }
}
