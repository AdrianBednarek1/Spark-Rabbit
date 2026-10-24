using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField]
    AudioClip ultraBrzZvuk;

    [SerializeField]
    AudioClip OuchZvuk;

    [SerializeField]
    AudioClip Dead;

    [SerializeField]
    AudioClip Metak;

    [SerializeField]
    AudioClip Attack1;

    [SerializeField]
    AudioClip Attack2;

    [SerializeField]
    AudioClip Attack3;

    [SerializeField]
    AudioClip Zivot;

    PlayerHP hpManager;

    private AudioSource sourceZvuk;
    private bool mozePustiti = true;
    private void Start()
    {
        hpManager = GetComponent<PlayerHP>();
    }
    private void Awake()
    {
        sourceZvuk = gameObject.AddComponent<AudioSource>();
        sourceZvuk.volume = .7f;
    }
    private void PustiultraBrzZvuk()
    {
        sourceZvuk.PlayOneShot(ultraBrzZvuk);
    }
    private void PustiOuchZvuk()
    {
        if (hpManager.AkoImaHP())
        {
            sourceZvuk.PlayOneShot(OuchZvuk);
        }
        else
        {
            sourceZvuk.PlayOneShot(Dead);
        }   
    }
    private void PustiMetakZvuk()
    {
        if (mozePustiti)
        {
            mozePustiti=false;
            sourceZvuk.PlayOneShot(Metak);
            Invoke("ZvukCD", .12f);
        }      
    }
    private void ZvukCD()
    {
        mozePustiti = true;
    }
    private void PustiAttack1Zvuk()
    {
        sourceZvuk.PlayOneShot(Attack1);
    }
    private void PustiAttack2Zvuk()
    {
        sourceZvuk.PlayOneShot(Attack2);
    }
    private void PustiAttack3Zvuk()
    {
        sourceZvuk.PlayOneShot(Attack3);
    }
    public void PustiZivotZvuk()
    {
        sourceZvuk.PlayOneShot(Zivot);
    }
}
