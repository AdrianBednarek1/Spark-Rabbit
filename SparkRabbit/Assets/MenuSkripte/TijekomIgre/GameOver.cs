using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    GameObject gameOver;
    GameObject areYouSure;
    TextMeshProUGUI score;
    UpravljanjeScenom upraviteljScena;

    public static bool Gameover = false;
    private void Start()
    {
        upraviteljScena = GetComponent<UpravljanjeScenom>();
        DohvatiDijecu();
        score = DohvatiScore();
    }

    private TextMeshProUGUI DohvatiScore()
    {
        return gameOver.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>(); 
    }

    private void DohvatiDijecu()
    {
        gameOver = transform.Find("GameOverCanvas").gameObject;
        areYouSure = transform.Find("YesOrNoCanvas").gameObject;
        gameOver.SetActive(false);
        areYouSure.SetActive(false);
    }

    public void PrikaziGameOver()
    {
        Gameover = true;
        score.text = "Score: "+Manager.bodovi.ToString();
        gameOver.SetActive(true);
    }
    public void PrikaziAreYouSure()
    {
        gameOver.SetActive(false);
        areYouSure.SetActive(true);
    }
    public void VratiSeNaGameOver()
    {
        gameOver.SetActive(true);
        areYouSure.SetActive(false);
    }
    public void RestartLevel()
    {
        gameOver.SetActive(false);
        areYouSure.SetActive(false);
        upraviteljScena.NaPocetniLevel();
        
    }
    public void Menu()
    {
        gameOver.SetActive(false);
        areYouSure.SetActive(false);
        upraviteljScena.NaMenu();
    }
}
