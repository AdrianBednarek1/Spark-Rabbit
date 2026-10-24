using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpravljanjePjesmom : MonoBehaviour
{
    AudioSource[] Glazba;

    private int indeksAktivnePjesme;

    public static bool BossGlazba = false;
    private int stanjeHPa;

    [SerializeField]
    PlayerHP hpManager;
    void Start()
    {
        Glazba = GetComponentsInChildren<AudioSource>();
        stanjeHPa = hpManager.StanjeHPa();
        indeksAktivnePjesme = 10;
        NastaviGlazbuPremaHP();
    }
    void Update()
    {
        if (!hpManager.AkoImaHP())
        {
            PrekiniGlazbu();

        }
        else if (MozePromijenitiGlazbu())
        {
            stanjeHPa = hpManager.StanjeHPa();
            NastaviGlazbuPremaHP();
        }
    }

    private bool MozePromijenitiGlazbu()
    {
        if (stanjeHPa != hpManager.StanjeHPa() || BossGlazba)
        {
            return true;
        }
        return false;
    }

    public void NastaviGlazbuPremaHP()
    {
        if (!BossGlazba)
        {
            PromjenaGlazbe();
        }
        else if (BossGlazba && indeksAktivnePjesme != 3)
        {
            indeksAktivnePjesme = 3;
            PustiPjesmu();
        }
    }

    private void PromjenaGlazbe()
    {
        if (hpManager.StanjeHPa() <= 1)
        {
            if (indeksAktivnePjesme != 2)
            {
                indeksAktivnePjesme = 2;
                PustiPjesmu();          
            }
        }
        else if (hpManager.StanjeHPa() > 3)
        {
            if (indeksAktivnePjesme != 0)
            {           
                indeksAktivnePjesme = 0;
                PustiPjesmu();
            }
        }
        else if (hpManager.StanjeHPa() < 4 || hpManager.StanjeHPa() > 1)
        {
            if (indeksAktivnePjesme != 1)
            {
                indeksAktivnePjesme = 1;
                PustiPjesmu();
            }
        }
    }
    private void PustiPjesmu()
    {
        for (int i = 0; i<Glazba.Length;i++)
        {
            if (i == indeksAktivnePjesme)
            {
                Glazba[i].Play();
            }
            else
            {
                Glazba[i].Pause();
            }
        }
    }
    public void PrekiniGlazbu()
    {
        Glazba[indeksAktivnePjesme].Pause();
    }
    public void ResetPjesme()
    {
        NastaviGlazbuPremaHP();
        indeksAktivnePjesme = 10;
    }
}
