using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider GameSlider;
    float glasnocaGame;
    static bool prviPut = true;
    private void Awake()
    {
        postaviZvukJednom();
        Ucitaj();
    }

    private void postaviZvukJednom()
    {
        if (prviPut)
        {
            prviPut = false;
            PlayerPrefs.GetFloat("GameVolume",.7f);
        }
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
