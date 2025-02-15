using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    void Start()
    {
        AudioManagerScript.instance.PlaySound("MenuMusic");
    }

    public void PlayButton()
    {
        //AudioManagerScript.instance.PlaySound("MenuOk");
        SceneManager.LoadSceneAsync(1);
        
    }

    public void OptionsButton()
    {
        AudioManagerScript.instance.PlaySound("MenuOk");
    }

    public void QuitButton()
    {
        AudioManagerScript.instance.PlaySound("MenuNegative");
        Application.Quit();
    }
}
