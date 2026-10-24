using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject Score;

    [SerializeField]
    GameObject Menu;

    [SerializeField]
    GameObject Naslov;

    [SerializeField]
    Options osvjeziVolume;
    private void Start()
    {
        osvjeziVolume.Ucitaj();
    }
    void Update()
    {
        OtvoriScore();
    }
    private void OtvoriScore()
    {
        if (Manager.gameFinished)
        {
            Manager.gameFinished = false;
            Naslov.SetActive(false);
            Score.SetActive(true);
            Menu.SetActive(false);
        }
    }
}
