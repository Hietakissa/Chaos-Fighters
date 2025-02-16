using HietakissaUtils;
using HietakissaUtils.QOL;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
    }

    public void OptionsButton()
    {
        //AudioManagerScript.instance.PlaySound("MenuOk");
        AudioManagerScript.instance.PlayRandomSound("Hit");
    }

    public void QuitButton()
    {
        AudioManagerScript.instance.PlaySound("MenuNegative");
        QOL.Quit();
    }
}
