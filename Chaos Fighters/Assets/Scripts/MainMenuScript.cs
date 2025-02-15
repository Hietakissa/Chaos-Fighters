using HietakissaUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    [SerializeField] SceneReference gameScene;

    void Start()
    {
    }

    public void PlayButton()
    {
        AudioManagerScript.instance.PlaySound("MenuOk");
        SceneManager.LoadSceneAsync(gameScene);
        
    }

    public void OptionsButton()
    {
        AudioManagerScript.instance.PlaySound("MenuOk");
    }

    public void QuitButton()
    {
        AudioManagerScript.instance.PlaySound("MenuOk");
        Application.Quit();
    }
}
