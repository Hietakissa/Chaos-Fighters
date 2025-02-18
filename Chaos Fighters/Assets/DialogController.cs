using HietakissaUtils;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject leftSelector;
    public GameObject rightSelector;

    public TextMeshProUGUI timer;

    public TextMeshProUGUI promptPanel;

    [SerializeField] TextMeshProUGUI[] leftTexts;
    [SerializeField] TextMeshProUGUI[] rightTexts;
    public TextMeshProUGUI left1;
    public TextMeshProUGUI left2;
    public TextMeshProUGUI left3;

    public TextMeshProUGUI right1;
    public TextMeshProUGUI right2;
    public TextMeshProUGUI right3;

    public string[] prompts;
    public string[] answers;

    public float timeToWait;
    float currentTime;

    [SerializeField] SceneReference gameScene;

    public GameObject leftCharacterIdle;
    public GameObject rightCharacterIdle;
    public GameObject leftCharacterWin;
    public GameObject rightCharacterLose;

    [SerializeField] RoastBattleOptionSO[] roasts;
    RoastBattleOptionSO currentRoast;

    [SerializeField] SelectorController p1Selector;
    [SerializeField] SelectorController p2Selector;

    RoastOption[] leftOptions;
    RoastOption[] rightOptions;

    public bool countingDown;


    void Start()
    {
        NewStuff();
        Invoke("AnnounceWinner", 5.0f);
    }

    void Update()
    {
        if (!countingDown) return;

        currentTime -= Time.deltaTime;
        if (currentTime < 0f)
        {
            currentTime = 0f;
            countingDown = false;
        }

        float joo = Mathf.Ceil(currentTime);
        timer.text = joo.ToString();
    }

    void AnnounceWinner()
    {
        int p1Index = p1Selector.selectedNumber;
        int p2Index = p2Selector.selectedNumber;

        int p1Strength = leftOptions[p1Index].Strength;
        int p2Strength = rightOptions[p2Index].Strength;
        bool tie = p1Strength == p2Strength;
        bool p1Win = p1Strength > p2Strength;

        if (tie)
        {
            RoastResult.Player1Damage = 15;
            RoastResult.Player2Damage = 15;
        }
        else
        {
            if (p1Win)
            {
                RoastResult.Player1Damage = 0;
                RoastResult.Player2Damage = 25;
            }
            else
            {
                RoastResult.Player1Damage = 25;
                RoastResult.Player2Damage = 0;
            }
        }

        if (tie) timer.text = $"Tie!{Environment.NewLine}-15 to both!";
        else if (p1Win) timer.text = $"Pawssacre Wins!{Environment.NewLine}-{RoastResult.Player2Damage} to Bitch Star!";
        else timer.text = $"Bitch Star Wins!{Environment.NewLine}-{RoastResult.Player1Damage} to Pawssacre!";

        countingDown = false;

        RoastResult.RoastComplete = true;
        RoastResult.RoastThisRound = true;
        RoastResult.RoastLoads = 0;
        Debug.Log($"tie: {tie}, p1win: {p1Strength > p2Strength}, p1dmg: {RoastResult.Player1Damage}, p2dmg: {RoastResult.Player2Damage}");


        //left1.text = "";
        //left2.text = "Hehehe";
        //left3.text = "";
        //
        //promptPanel.text = "Player 1 wins!";
        //
        //right1.text = "";
        //right2.text = "This is not right!";
        //right3.text = "";
        //
        //leftSelector.SetActive(false);
        //rightSelector.SetActive(false);
        //
        //leftCharacterIdle.SetActive(false);
        //rightCharacterIdle.SetActive(false);
        //leftCharacterWin.SetActive(true);
        //rightCharacterLose.SetActive(true);

        Invoke("StartGameScene", 3.0f);
    }

    void NewStuff()
    {
        countingDown = true;
        currentTime = timeToWait;

        currentRoast = roasts.RandomElement();
        leftOptions = currentRoast.Player1Options;
        rightOptions = currentRoast.Player2Options;

        leftOptions.Shuffle();
        rightOptions.Shuffle();


        promptPanel.text = currentRoast.Sentence;

        for (int i = 0; i < leftTexts.Length; i++)
        {
            leftTexts[i].text = leftOptions[i].Option;
        }

        for (int i = 0; i < rightTexts.Length; i++)
        {
            rightTexts[i].text = rightOptions[i].Option;
        }

        //Debug.Log(promptPanel);
        //promptPanel.text = "hahaha";
        //int x = Random.Range(0, prompts.Length);
        //promptPanel.text = prompts[x];

        /*x = Random.Range(0, answers.Length);
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
        right3.text = answers[x];*/
    }

    public void SetSelectedForSide(bool isLeft, int index)
    {
        if (isLeft)
        {
            for (int i = 0; i < leftTexts.Length; i++)
            {
                if (i == index) leftTexts[i].color = Color.yellow;
                else leftTexts[i].color = Color.white;
            }
        }
        else
        {
            for (int i = 0; i < rightTexts.Length; i++)
            {
                if (i == index) rightTexts[i].color = Color.yellow;
                else rightTexts[i].color = Color.white;
            }
        }
    }

    void StartGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
    
}
