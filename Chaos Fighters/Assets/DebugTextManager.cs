using System.Collections.Generic;
using HietakissaUtils.QOL;
using System.Collections;
using UnityEngine;
using System.Text;
using TMPro;

public class DebugTextManager : MonoBehaviour
{
    public static DebugTextManager Instance;

    [SerializeField] TextMeshProUGUI text;

    StringBuilder sb = new();
    Dictionary<string, string> variables = new();
    bool requireReBake;

    void Awake()
    {
        Instance = this;
    }

    void LateUpdate()
    {
        if (!requireReBake) return;

        sb.Clear();
        foreach (KeyValuePair<string, string> pair in variables)
        {
            sb.AppendLine($"{pair.Key}: {pair.Value}");
        }

        text.text = sb.ToString();
        requireReBake = false;
    }


    public void SetVariable(string key, string value)
    {
        variables[key] = value;
        requireReBake = true;
    }

    public void SetVariableFor(string key, string value, float time)
    {
        SetVariable(key, value);
        StartCoroutine(RemoveVariableAfter(key, time));
    }

    public void RemoveVariable(string key)
    {
        variables.Remove(key);
        requireReBake = true;
    }


    IEnumerator RemoveVariableAfter(string key, float time)
    {
        yield return QOL.WaitForSeconds.Get(time);
        RemoveVariable(key);
    }
}