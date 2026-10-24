using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pcela : MonoBehaviour
{
    SpriteRenderer sprite;
    Rigidbody2D rb2;
    Animator animator;
    EnemyHP hpManager;
    UnityEngine.Object metakReferenca;
    private bool gore = true;
    private float pocetnaPozicija;    

    [SerializeField]
    GameObject vidok;

    [SerializeField] 
    float razlika;

    [SerializeField] 
    float brzina;

    [SerializeField]
    float vidik;

    [SerializeField]
    Transform Player;

    [SerializeField]
    Transform Pucanj;

    [SerializeField]
    float pozicijaMetka;

    [SerializeField]
    GameObject bodovi;

    [SerializeField] 
    string brBodova;

    [SerializeField] 
    Manager manager;
    private Vector2 pozicijaPrijeTresenja;
    private bool TreseSe;

    void Start()
    {
        metakReferenca = Resources.Load("metakPcela");
        rb2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        hpManager = GetComponent<EnemyHP>();
        pocetnaPozicija = transform.position.y;
    }
    void Update()
    {   
        if (hpManager.ImaLiHP())
        {
            AnimacijaNapadanje();
            AnimacijaKretanja();           
            OkreniSePremaPLayeru();
            OdrediPutanjuKretanja();
        }
        AnimacijaPrimanjaStete();

    }
    private void OkreniSePremaPLayeru()
    {
        if (Player.position.x > transform.position.x)
        {
            sprite.flipX=true;
        }
        else
        {
            sprite.flipX=false;
        }
    }
    private void AnimacijaNapadanje()
    {
        if (VidimPlayera())
        {
            animator.SetTrigger("Napad"); 
        }
    }
    private void Pucaj()
    {
        GameObject metak = (GameObject)Instantiate(metakReferenca);
        metak.GetComponent<metakskripta>().StartMetak(sprite.flipX);
        metak.transform.SetParent(transform);
        OkreniMetak(metak);
    }
    private void Potresi()
    {
        pozicijaPrijeTresenja = transform.position;
        TreseSe = true;

        if (TreseSe)
        {
            transform.position = pozicijaPrijeTresenja + UnityEngine.Random.insideUnitCircle * new Vector2(.5f, .5f);
            Invoke("ResetTresi", 0.2f);
        }
    }
    private void ResetTresi()
    {
        transform.position = pozicijaPrijeTresenja;
        TreseSe = false;
    }
    private void OkreniMetak(GameObject metak)
    {
        if (sprite.flipX) { metak.transform.localPosition = new Vector3(Pucanj.localPosition.x+pozicijaMetka,Pucanj.localPosition.y,0);}
        else { metak.transform.localPosition = Pucanj.localPosition; }
    }
    private bool VidimPlayera()
    {
        Vector2 granicaVidika = OdrediVidik();
        RaycastHit2D dodir = Physics2D.Linecast(vidok.transform.position, granicaVidika, 1 << LayerMask.NameToLayer("Action"));
        if (dodir.collider && hpManager.ImaLiHP())
        {
            if (dodir.collider.tag == "Player")
            {
                return true;
            }
        }
        Debug.DrawLine(vidok.transform.position, granicaVidika, Color.blue);
        return false;
    }
    private Vector2 OdrediVidik()
    {
        if (!sprite.flipX)
        {
            return vidok.transform.position + Vector3.right * -vidik;
        }
        return vidok.transform.position + Vector3.right * vidik;
    }
    private void OdrediPutanjuKretanja()
    {
        if (transform.position.y + razlika < pocetnaPozicija)
        {
            gore = true;
        }
        else if (transform.position.y - razlika > pocetnaPozicija )
        {
            gore = false;
        }
    }
    private void AnimacijaKretanja()
    {
        if (gore)
        {
            rb2.velocity = new Vector2(rb2.velocity.x, brzina);
        }
        else
        {
            rb2.velocity = new Vector2(rb2.velocity.x, -brzina);
        }  
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("metak"))
        {
            hpManager.SmanjiHP(.2f);
        }
    }
    private void AnimacijaPrimanjaStete()
    {
        if (hpManager.MozeAnimiratiStetu)
        {
            Potresi();
            hpManager.MozeAnimiratiStetu = false;
            StaviCrvenuBoju();
            if (hpManager.ImaLiHP())
            {
                EnemyPrimaStetu();
            }
            else
            {
                EnemyUmire();
            }
        }
    }
    private void EnemyUmire()
    {
        AnimacijaSmrti();
        DobivanjeBodova();
    }

    private void AnimacijaSmrti()
    {
        animator.SetTrigger("Smrt");
        rb2.gravityScale = 1;
        gameObject.layer = 10;
        hpManager.UnistiHPSlider();
        rb2.velocity = Vector2.zero;
    }

    private void DobivanjeBodova()
    {
        manager.PlayerDobivaBodove(int.Parse(brBodova));
        GameObject bod = Instantiate(bodovi, transform.position, Quaternion.identity);
        bod.transform.GetChild(0).GetComponent<TextMeshPro>().text = brBodova;
        bod.SetActive(true);
    }
    private void EnemyPrimaStetu()
    {
        rb2.velocity = Vector2.zero;
        animator.SetTrigger("Boli");
    }
    private void StaviCrvenuBoju()
    {
        sprite.color = Color.red;
        Invoke("VratiBoju",0.2f);
    }
    private void VratiBoju()
    {
        sprite.color = Color.white;
    }
}

