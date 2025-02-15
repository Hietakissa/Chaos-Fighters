using HietakissaUtils;

using TMPro;

using UnityEngine;

public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI timer;

    public TextMeshProUGUI promptPanel;

    public TextMeshProUGUI left1;
    public TextMeshProUGUI left2;
    public TextMeshProUGUI left3;

    public TextMeshProUGUI right1;
    public TextMeshProUGUI right2;
    public TextMeshProUGUI right3;

    public string[] prompts;
    public string[] answers;

    public float timeToWait;
    public float currentTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        currentTime += Time.deltaTime;
        float joo = Mathf.Floor(currentTime);
        timer.text = joo.ToString();

        if (currentTime > timeToWait)
        { 
            currentTime = 0;
            NewStuff();
        }
    }

    void NewStuff()
    {
        Debug.Log(promptPanel);
        promptPanel.text = "hahaha";
        int x = Random.Range(0, prompts.Length);
        promptPanel.text = prompts[x];

        x = Random.Range(0, answers.Length);
        left1.text = answers[x];
        x = Random.Range(0, answers.Length);
        left2.text = answers[x];
        x = Random.Range(0, answers.Length);
        left3.text = answers[x];

        x = Random.Range(0, answers.Length);
        right1.text = answers[x];
        x = Random.Range(0, answers.Length);
        right2.text = answers[x];
        x = Random.Range(0, answers.Length);
        right3.text = answers[x];
    }
}
