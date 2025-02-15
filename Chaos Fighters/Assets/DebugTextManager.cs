using System.Collections.Generic;
using HietakissaUtils.QOL;
using System.Collections;
using UnityEngine;
using System.Text;
using TMPro;

public class DebugTextManager : MonoBehaviour
{
    public static DebugTextManager Instance;

    [SerializeField] TextMeshProUGUI player1Text;
    [SerializeField] TextMeshProUGUI player2Text;

    StringBuilder sb = new();
    Dictionary<string, string> player1Variables = new();
    Dictionary<string, string> player2Variables = new();
    bool p1RequireReBake;
    bool p2RequireReBake;

    void Awake()
    {
        Instance = this;
    }

    void LateUpdate()
    {
        if (p1RequireReBake)
        {
            sb.Clear();
            foreach (KeyValuePair<string, string> pair in player1Variables)
            {
                sb.AppendLine($"{pair.Key}: {pair.Value}");
            }

            player1Text.text = sb.ToString();
            p1RequireReBake = false;
        }

        if (p2RequireReBake)
        {
            sb.Clear();
            foreach (KeyValuePair<string, string> pair in player2Variables)
            {
                sb.AppendLine($"{pair.Key}: {pair.Value}");
            }

            player2Text.text = sb.ToString();
            p1RequireReBake = false;
        }
    }


    public void SetVariable(string key, string value, PlayerController player = null)
    {
        if (!player) player1Variables[key] = value;
        else if (player.IsPlayer1)
        {
            player1Variables["P1" + key] = value;
            p1RequireReBake = true;
        }
        else
        {
            player2Variables["P2" + key] = value;
            p2RequireReBake = true;
        }
    }

    public void SetVariableFor(string key, string value, float time, PlayerController player = null)
    {
        SetVariable(key, value, player);
        StartCoroutine(RemoveVariableAfter(key, time));
    }

    public void RemoveVariable(string key, PlayerController player = null)
    {
        if (!player) player1Variables.Remove(key);
        else if (player.IsPlayer1)
        {
            player1Variables.Remove("P1" + key);
            p1RequireReBake = true;
        }
        else
        {
            player2Variables.Remove("P2" + key);
            p2RequireReBake = true;
        }
    }


    IEnumerator RemoveVariableAfter(string key, float time)
    {
        yield return QOL.WaitForSeconds.Get(time);
        RemoveVariable(key);
    }
}