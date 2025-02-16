using HietakissaUtils;
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

    void Start()
    {
        NewStuff();
        Invoke("AnnounceWinner", 5.0f);
    }

    void Update()
    {
            currentTime += Time.deltaTime;
            float joo = Mathf.Floor(currentTime);
            timer.text = joo.ToString();

    }

    void AnnounceWinner()
    {
        left1.text = "";
        left2.text = "Hehehe";
        left3.text = "";

        promptPanel.text = "Player 1 wins!";

        right1.text = "";
        right2.text = "This is not right!";
        right3.text = "";

        leftSelector.SetActive(false);
        rightSelector.SetActive(false);

        leftCharacterIdle.SetActive(false);
        rightCharacterIdle.SetActive(false);
        leftCharacterWin.SetActive(true);
        rightCharacterLose.SetActive(true);

        Invoke("StartGameScene", 3.0f);
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

    void StartGameScene()
    {
        SceneManager.LoadSceneAsync(gameScene);
    }
}
