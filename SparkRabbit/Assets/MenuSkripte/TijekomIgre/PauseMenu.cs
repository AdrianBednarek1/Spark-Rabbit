using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PauseMenu : MonoBehaviour
{
    public static bool pauzirano = false;
    [SerializeField] 
    GameObject PauseUI;

    [SerializeField] 
    GameObject YesNo;

    [SerializeField]
    GameObject tranzicija;

    [SerializeField] 
    GameObject GameOptions;

    [SerializeField]
    GameOptions gameOptions;

    UpravljanjeScenom upravljanjeScenom;
    private void Start()
    {
        upravljanjeScenom = GetComponent<UpravljanjeScenom>();
        Nevidljivo();
        gameOptions.Ucitaj();
    }

    private void Nevidljivo()
    {
        PauseUI.SetActive(false);
        GameOptions.SetActive(false);
        YesNo.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameOver.Gameover)
        {
            if (pauzirano)
            {
                Pokreni();
            }
            else
            {
                Pauziraj();
            }
        }
    }
    void Pauziraj()
    {
        Time.timeScale = 0f;
        PauseUI.SetActive(true);
        pauzirano = true;
    }
    public void Options()
    {
        GameOptions.SetActive(true);
        PauseUI.SetActive(false);
    }
    public void Pokreni()
    {
        Time.timeScale = 1f;
        PauseUI.SetActive(false);
        YesNo.SetActive(false);
        GameOptions.SetActive(false);
        pauzirano =false;
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        YesNo.SetActive(false);
        StartCoroutine(UcitavanjeMenu());
    }
    public void AreYouSure()
    {
        YesNo.SetActive(true);
        PauseUI.SetActive(false);
    }
    public void No()
    {
        YesNo.SetActive(false);
        PauseUI.SetActive(true);
    }
    public IEnumerator UcitavanjeMenu()
    {     
        tranzicija.GetComponent<Tranzicija>().ZapocniDruguTranziciju();       
        yield return new WaitForSeconds(1.2f);
        upravljanjeScenom.NaMenu();
    }
}
