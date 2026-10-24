using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    
    [SerializeField]
    GameOver gameOver;

    [SerializeField]
    CinemachineVirtualCamera trenutnaKamera;

    [SerializeField]
    TextMeshProUGUI BodoviTekst;

    [SerializeField]
    UpravljanjePjesmom songManager;

    private bool restart = false;
    
    public static int bodovi = 0;
    static private int bodoviZaZivot = 0;
    public static int ekstraZivoti= 0;
    public static bool gameFinished = false;

    PlayerHP hpManager;
    PlayerRespawn respawn;
    void Start()
    {
        DohvatiSkriptePlayera();
    }
    private void Update()
    {
        BodoviTekst.text = bodovi.ToString();
        ProvjeriStanjePlayera();
    }
    private void DohvatiSkriptePlayera()
    {
        hpManager = player.GetComponent<PlayerHP>();
        respawn = player.GetComponent<PlayerRespawn>();
    }
    private void ProvjeriStanjePlayera()
    {
        if (AkoJePlayerIzgubio())
        {
            restart = true;
            ProvjeriDaliImaZivote();
        }
        else if(hpManager.AkoImaHP())
        {
            restart = false;
        }
    }
    private void ProvjeriDaliImaZivote()
    {
        if (hpManager.NemaZivote())
        {
            PostaviGameOver();
        }
        else
        {
            Invoke("VracanjeNaZadnjiCheckPoint", 1.3f);
        }
    }
    private void PostaviGameOver()
    {
        gameOver.PrikaziGameOver();
        UpravljanjePjesmom.BossGlazba = false;
    }
    private bool AkoJePlayerIzgubio()
    {
        if (!hpManager.AkoImaHP() && PlayerIzvanKamere() && !restart)
        {
            return true;
        }
        return false;
    }
    public void PlayerDobivaBodove(int item)
    {
        bodovi += item;
        bodoviZaZivot += item;
        ProvjeriMoguLiDobitiZivot();
    }

    private void ProvjeriMoguLiDobitiZivot()
    {
        if (bodoviZaZivot>=10000)
        {
            bodoviZaZivot = 0;
            hpManager.DobivaZivot();
        }
    }
    private void VracanjeNaZadnjiCheckPoint()
    {
        respawn.VratiSeNaCheckPoint();
        songManager.ResetPjesme();
    }
    private bool PlayerIzvanKamere()
    {
        Vector2 pozicijaPlayera = Camera.main.WorldToScreenPoint(player.transform.position);
        return !Screen.safeArea.Contains(pozicijaPlayera);
    }
}
