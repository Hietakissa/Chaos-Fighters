using UnityEngine.SceneManagement;
using HietakissaUtils.QOL;
using HietakissaUtils;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{

    //[SerializeField] SceneReference gameScene;

    void Start()
    {
        AudioManagerScript.instance.PlaySound("MenuMusic");
    }

    public void PlayButton()
    {
        AudioManagerScript.instance.PlaySound("MenuOk");
        SceneManager.LoadSceneAsync(1);
        RoastResult.RoastComplete = false;
        RoastResult.RoastThisRound = false;
    }

    public void OptionsButton()
    {
        //AudioManagerScript.instance.PlaySound("MenuOk");
        AudioManagerScript.instance.PlayRandomSound("Hit");
        AudioManagerScript.instance.PlayRandomSound("LionPain");
        AudioManagerScript.instance.PlayRandomSound("WomanPain");
    }

    public void QuitButton()
    {
        AudioManagerScript.instance.PlaySound("MenuNegative");
        QOL.Quit();
    }
}
