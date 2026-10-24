using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    float hp = 20;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    public static bool mrtav = false;
    public static bool AnimacijaOzljedzenCD = false;
    private bool CoroutineSinkronizacija = false;
    [SerializeField]
    Manager manager;

    [SerializeField]
    Canvas canvasParent;

    [SerializeField]
    Slider slider;

    public static Slider HpSlider;

    [SerializeField]
    private float Ypozicija;

    UpravljanjeScenom upravljanjeScenom;

    void Start()
    {
        KreirajHPSlider();
        upravljanjeScenom = GetComponent<UpravljanjeScenom>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        HpSlider.transform.position = PozicijaHPslidera();
        if (mrtav)
        {
            GameFinished();
        }
    }

    private Vector3 PozicijaHPslidera()
    {
        return new Vector3(canvasParent.transform.position.x, canvasParent.transform.position.y - Ypozicija, 0);
    }

    private void GameFinished()
    {
        if (!CoroutineSinkronizacija)
        {
            CoroutineSinkronizacija = true;
            StartCoroutine(NakonPorazaNaMenu());
        }    
    }
    private IEnumerator NakonPorazaNaMenu()
    {
        yield return new WaitForSeconds(3f);
        mrtav = false;
        CoroutineSinkronizacija = false;
        upravljanjeScenom.NaMenu();
    }

    public void SmanjiHP(float damage)
    {
        hp-= damage;
        HpSlider.value = hp;   
        rb.velocity = Vector3.zero;
        AnimacijaBoli();
        JeLiZiv();
    }
    private void StaviIzvornuBoju()
    {
        sprite.color = Color.white;
    }

    public void KreirajHPSlider()
    {
        HpSlider = Instantiate(slider);
        HpSlider.transform.SetParent(canvasParent.transform);
        HpSlider.maxValue = hp;
        HpSlider.value = hp;
        HpSlider.gameObject.SetActive(false);
    }

    private void AnimacijaBoli()
    {
        sprite.color = Color.red;
        Invoke("StaviIzvornuBoju", .2f);

        if (AnimacijaOzljedzenCD && !CoroutineSinkronizacija)
        {
            CoroutineSinkronizacija=true;
            StartCoroutine(ZapocniCooldown());
        }
        else if(!AnimacijaOzljedzenCD)
        {        
            animator.SetTrigger("Boli");
        }
    }

    private IEnumerator ZapocniCooldown()
    {
        yield return new WaitForSeconds(2f);
        AnimacijaOzljedzenCD = false;
        CoroutineSinkronizacija = false;
    }

    private void JeLiZiv() 
    {
        if (hp < 0)
        {
            animator.SetTrigger("Mrtav");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("metak"))
        {
            SmanjiHP(.2f);
        }
    }

}