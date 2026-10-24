using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pauk : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animataor;
    EnemyHP hpManager;
    
    private bool Lijevo = true; 
    private float pocetnaPozicija;
    private bool napad = false;
    private bool MozeNapasti;
    private float NasumicniBroj;
    private bool SinkronizacijaNapadanja = true;
    private UnityEngine.Object prahReferenca;

    [SerializeField] 
    GameObject bodovi;

    [SerializeField] 
    string brBodova;

    [SerializeField] 
    Manager manager;

    [SerializeField]
    float duljinaHodanja;

    [SerializeField]
    float brzinaHodanja;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animataor = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>(); 
        pocetnaPozicija = transform.position.x;
        prahReferenca = Resources.Load("Prah");
        hpManager = GetComponent<EnemyHP>();
    }

    void Update()
    {
        NasumicniBroj = UnityEngine.Random.Range(1,5);
        AnimacijaKretanje();
        AnimacijaNapadanje();
        AnimacijaPrimanjeStete();
    }
    private void AnimacijaNapadanje()
    {
        Napadni();
        if (SinkronizacijaNapadanja)
        {
            SinkronizacijaNapadanja = false;
            StartCoroutine(OdrediVrijemeNapadanja(NasumicniBroj));
        }
    }
    private IEnumerator OdrediVrijemeNapadanja(float broj)
    {
        yield return new WaitForSeconds(broj);
        MozeNapasti = true;
        SinkronizacijaNapadanja = true;
    }
    private void AnimacijaKretanje()
    {
        if (!napad)
        {
            KreciSe();
        }
        OdluciSmjer();
    }
    private void Napadni()
    {
        if (MozeNapasti)
        {
            rb.velocity = Vector2.zero;
            IzvediNapad();           
        }
    }
    private void IzvediNapad()
    {
        MozeNapasti = false;
        napad = true;
        animataor.SetTrigger("Napad");
        Invoke("NastaviSeKretati", 0.56f);
    }
    private void NastaviSeKretati()
    {
        napad = false;
    }
    private void OdluciSmjer()
    {
        if (transform.position.x + duljinaHodanja < pocetnaPozicija )
        {
            Lijevo = false;
        }
        else if (transform.position.x - duljinaHodanja > pocetnaPozicija )
        {
            Lijevo = true;
        }
    }
    private void KreciSe()
    {
        if (Lijevo)
        {
            IdiLijevo();
        }
        else
        {
            IdiDesno();
        }
    }
    private void IdiDesno()
    {
        rb.velocity = new Vector2(brzinaHodanja, 0);
    }
    private void IdiLijevo()
    {
        rb.velocity = new Vector2(-brzinaHodanja, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("metak"))
        {
            hpManager.SmanjiHP(0.2f);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerKontroler>().ozljedzen();
        }
    }
    private void AnimacijaPrimanjeStete()
    {
        if (hpManager.MozeAnimiratiStetu)
        {
            hpManager.MozeAnimiratiStetu = false;
            PostajeCrn();
            Smrt();
        }      
    }
    private void Smrt()
    {
        if (!hpManager.ImaLiHP())
        {
            GameObject prah = (GameObject)Instantiate(prahReferenca);
            prah.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Destroy(gameObject);
            DobivanjeBodova();
            hpManager.UnistiHPSlider();
        }
    }
    private void DobivanjeBodova()
    {
        manager.PlayerDobivaBodove(int.Parse(brBodova));
        GameObject bod = Instantiate(bodovi, transform.position, Quaternion.identity);
        bod.transform.GetChild(0).GetComponent<TextMeshPro>().text = brBodova;
        bod.SetActive(true);
    }
    private void PostajeCrn()
    {
        sprite.color = Color.black;
        Invoke("NijeCrn", 0.2f);
    }
    private void NijeCrn()
    {
        sprite.color = Color.white;
    }
}
