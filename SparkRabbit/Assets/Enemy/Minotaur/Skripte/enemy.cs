using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    Animator animator;  
    Rigidbody2D rb2;
    SpriteRenderer sprite;

    private bool napada=false;   
    private UnityEngine.Object prahReferenca;
    private float brzina = 4;
    private float brzinaHodanja;
    private const string LEVO = "left", DESNO = "right";
    private string smjerKretanja;
    private bool TreseSe;
    private Vector3 FlipanjeMinotaura;    
    private Vector2 pozicijaPrijeTresenja;

    [SerializeField]
    Transform ground;

    [SerializeField]
    Transform ground2;

    [SerializeField]
    Transform vidim;

    [SerializeField]
    private float udaljenost;

    [SerializeField]
    private float radius;

    [SerializeField]
    private GameObject udarac;

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField] 
    GameObject bodovi;

    [SerializeField]  
    string brBodova;

    [SerializeField] 
    Manager manager;

    EnemyHP hpManager;
    void Start()
    {
        FlipanjeMinotaura = transform.localScale;
        smjerKretanja = DESNO;
        animator = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        hpManager = GetComponent<EnemyHP>();
        prahReferenca = Resources.Load("Prah");        
    }
    private void FixedUpdate()
    {         
        OdrediSmjerKretanja();
        AnimacijaHodanja();
        AnimacijaNapadanja();
        AnimacijaPrimanjeStete();
    }
    private void OdrediSmjerKretanja()
    {
        if (smjerKretanja == LEVO && JesamLiNaPodu())
        {
            brzinaHodanja = -brzina;
        }
        else
        {
            brzinaHodanja = brzina;
        }
        AkoDiramZidRub();
    }
    private void AkoDiramZidRub()
    {
        if ((DodirujemLiZid() || JesamLiNaRubu()) && JesamLiNaPodu())
        {
            if (smjerKretanja == LEVO)
            {
                ZrcaljenjeMinotaura(DESNO);
            }
            else
            {
                ZrcaljenjeMinotaura(LEVO);
            }
        }
    }
    private void Potresi()
    {
        if (TreseSe)
        {
            transform.position = pozicijaPrijeTresenja + UnityEngine.Random.insideUnitCircle * new Vector2(0.1f, 1f);
            Invoke("ResetTresi", 0.2f);
        }
    }
    private void AnimacijaHodanja()
    {
        if (MozemoLiHodati())
        {
            rb2.velocity = new Vector2(brzinaHodanja, rb2.velocity.y);
            animator.SetFloat("hodam", brzina);
        }
        else
        {
            rb2.velocity = new Vector2(0, rb2.velocity.y);
            animator.SetFloat("hodam", 0);
        }
    }
    private void AnimacijaNapadanja()
    {
        if (VidimPlayera() && !napada)
        {
            napada = true;
            rb2.velocity = new Vector2(0, rb2.velocity.y);
            Invoke("napad", 0.5f);
        }
    }
    private bool MozemoLiHodati()
    {
        if(!napada && !animator.GetCurrentAnimatorStateInfo(0).IsName("napad") && JesamLiNaPodu())
        {
            return true;
        }
        return false;
    }

    private bool JesamLiNaPodu()
    {
        if (Physics2D.Linecast(transform.position, ground2.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            return true;
        }
        return false;
    }
    private void ZrcaljenjeMinotaura(string novismjer)
    {
        Vector3 NovoFlipanje = FlipanjeMinotaura;
        if (novismjer == LEVO)
        {
            NovoFlipanje.x = -FlipanjeMinotaura.x;
        }
        else
        {
            NovoFlipanje.x = FlipanjeMinotaura.x;
        }
        transform.localScale = NovoFlipanje;
        smjerKretanja = novismjer;
    }
    private bool DodirujemLiZid() 
    {
        
        float udaljenostZida = udaljenost;

        if (smjerKretanja == LEVO)
        {
            udaljenostZida = -udaljenost;
        }

        Vector3 DodirZida = ground.position;
        DodirZida.x += udaljenostZida;

        Debug.DrawLine(ground.position, DodirZida, Color.blue);
        if (Physics2D.Linecast(ground.position, DodirZida, 1 << LayerMask.NameToLayer("Ground")))
        {
            return true;
        }
        return false;
    }
    bool JesamLiNaRubu()
    {
        float UdaljenostRuba = udaljenost;

        Vector3 DodirRuba = ground.position;
        DodirRuba.y -= UdaljenostRuba;

        Debug.DrawLine(ground.position, DodirRuba, Color.red);

        if (Physics2D.Linecast(ground.position, DodirRuba, 1 << LayerMask.NameToLayer("Ground")))
        {
            return false;
        }

        return true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {      
        if (collision.gameObject.CompareTag("metak")) 
        {
            hpManager.SmanjiHP(0.2f);    
        }
    }
    private void AnimacijaPrimanjeStete()
    {
        if (hpManager.MozeAnimiratiStetu)
        {
            hpManager.MozeAnimiratiStetu = false;
            AnimacijaStete();
        }
    }

    private void AnimacijaStete()
    {
        if (!hpManager.ImaLiHP())
        {
            MinotaurUmire();
            PlayerDobivaBodove();
        }
        else
        {
            MinotaurDobivaStetu();
        }
    }
    private void MinotaurDobivaStetu()
    {
        pozicijaPrijeTresenja = transform.position;
        TreseSe = true;
        Potresi();
        CrvenaBoja();
        Invoke("ZadanaBoja", 0.2f);
    }
    private void MinotaurUmire()
    {
        GameObject prah = (GameObject)Instantiate(prahReferenca);
        prah.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        UnistiSveObjekte(prah);
    }
    private void UnistiSveObjekte(GameObject prah)
    {
        Destroy(prah.gameObject, 1f);
        hpManager.UnistiHPSlider();
        Destroy(gameObject);
    }
    private void PlayerDobivaBodove()
    {
        manager.PlayerDobivaBodove(int.Parse(brBodova));
        GameObject bod = Instantiate(bodovi, transform.position, Quaternion.identity);
        bod.transform.GetChild(0).GetComponent<TextMeshPro>().text = brBodova;
        bod.SetActive(true);
    }
    private void ResetTresi()
    {
        TreseSe = false;
        transform.position = pozicijaPrijeTresenja;
    }
    private void CrvenaBoja()
    {
        sprite.color = new Color(255,0,0);
    }
    private void ZadanaBoja()
    {
        sprite.color = new Color(255, 255, 255);
    }

    private void napad()
    {
        animator.SetTrigger("napad");
        Invoke("Udarac",0.4f);    
        napada = false;
    }
    private void Udarac()
    {     
        Collider2D[] udarioPlayera = Physics2D.OverlapCircleAll(udarac.transform.position, radius, playerLayer);
        foreach (Collider2D player in udarioPlayera)
        {
            if (player.CompareTag("Player"))
            {
                player.GetComponent<PlayerKontroler>().ozljedzen();
            }
        }
    }
    private bool VidimPlayera()
    {
        float smjer = 1.1f;

        if (smjerKretanja == LEVO)
        {
            smjer = -1.1f;
        }

        Vector3 KrajLinije = vidim.position + Vector3.right *smjer;
        RaycastHit2D VidimPlayera = Physics2D.Linecast(vidim.position, KrajLinije, 1<< LayerMask.NameToLayer("Action"));
        Debug.DrawLine(vidim.position, KrajLinije, Color.blue);

        if (VidimPlayera.collider)
        {
            if(VidimPlayera.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.DrawWireSphere(udarac.transform.position, radius);
    }
}
