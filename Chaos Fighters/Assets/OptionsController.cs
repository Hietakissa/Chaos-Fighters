using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider soundSlider, musicSlider;

    public void SoundVolume()
    {
        AudioManagerScript.instance.SoundVolume(soundSlider.value);
        AudioManagerScript.instance.PlaySound("MenuOk");
    }

    public void MusicVolume()
    {
        AudioManagerScript.instance.MusicVolume(musicSlider.value);

    }
}
