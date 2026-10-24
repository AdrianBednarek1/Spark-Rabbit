using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
    public Slider GameSlider;
    float glasnocaGame ;

    private void Awake()
    {
        Ucitaj();
    }
    private void PromijeniGlasnocu()
    {
        glasnocaGame = GameSlider.value;
        
        PlayerPrefs.SetFloat("GameVolume", glasnocaGame);
        Ucitaj();
    }
    public void Ucitaj()
    {
        glasnocaGame = PlayerPrefs.GetFloat("GameVolume");
        GameSlider.value = glasnocaGame;
        AudioListener.volume = glasnocaGame;
    }

}
