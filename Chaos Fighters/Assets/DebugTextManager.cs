using UnityEngine;
using System;
using TMPro;

public class DebugTextManager : MonoBehaviour
{
    public static DebugTextManager Instance;

    [SerializeField] TextMeshProUGUI text;
    string textString;

    void Awake()
    {
        Instance = this;
    }

    void LateUpdate()
    {
        text.text = textString;
        textString = string.Empty;
    }


    public void Add(string text)
    {
        textString += text;
    }
    public void AddLine(string text)
    {
        textString += $"{text}{Environment.NewLine}";
    }
}