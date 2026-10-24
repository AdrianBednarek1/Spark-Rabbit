using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Zmija : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animator;
    BoxCollider2D udarac;

    private bool Napada = false;
    private bool Ozljedzen = false;
    private bool KrecemSe = false;
    private Vector2 pozicijaPrijeTresenja;
    EnemyHP hpManager;

    [SerializeField]
    float BrzinaHodanja;

    [SerializeField]
    GameObject hitBox;

    [SerializeField]
    GameObject vidok;

    [SerializeField]
    private float vidik;

    [SerializeField]
    GameObject bodovi;

    [SerializeField]
    string brBodova;

    [SerializeField]
    Manager manager;

    [SerializeField]
    private Color BojaZmije;
    private bool TreseSe;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            udarac = hitBox.GetComponent<BoxCollider2D>();
            hpManager = GetComponent<EnemyHP>();
            sprite.color = BojaZmije;
        }
        private void Update()
        {
            PostaviAnimacijuHodanja();
            NapadanjePlayera();
            PrimanjeStete();
            if (KretanjeDopusteno())
            {
                KrecemSe = true;
                StartCoroutine(KreciSe(RandomBroj()));
            }
        }
    private void NapadanjePlayera()
    {
        if (VidimPlayera() && !Napada && hpManager.ImaLiHP())
        {
            Napada = true;
            animator.SetTrigger("Napad");
            rb.velocity = new Vector2(0, rb.velocity.y);
            Invoke("NeNapadaj", 1.2f);
        }
    }
    private void NeNapadaj()
    {
        Napada = false;
    }
    private bool VidimPlayera()
    {
        Vector2 granicaVidika = OdrediVidik();
        RaycastHit2D dodir = Physics2D.Linecast(vidok.transform.position,granicaVidika, 1 << LayerMask.NameToLayer("Action"));
        if (dodir.collider && hpManager.ImaLiHP())
        {
            if (dodir.collider.tag == "Player")
            {
                return true;
            }  
        }
        Debug.DrawLine(vidok.transform.position,granicaVidika, Color.blue);
        return false;
    }
    private Vector2 OdrediVidik()
    {
        if (sprite.flipX)
        {
            return vidok.transform.position+Vector3.right * -vidik;
        }
        return  vidok.transform.position+ Vector3.right * vidik;
    }   
    private void PostaviAnimacijuHodanja()
    {
        animator.SetFloat("BrzinaHodanja", Math.Abs(rb.velocity.x));
        OkreniSe();
    }
    private bool KretanjeDopusteno()
    {
        if(!Ozljedzen && hpManager.ImaLiHP() && !Napada && !KrecemSe)
        {
            return true;
        }
        return false;
    }
    private void UkljuciCollider()
    {
        hitBox.SetActive(true);
    }
    private void IskljuciCollider()
    {
        hitBox.SetActive(false);
    }
    private void OkreniSe()
    {
        if (rb.velocity.x < 0)
        {
            sprite.flipX = true;
            hitBox.transform.localScale = new Vector3(-1,1,1);
            vidok.transform.localPosition = new Vector3(-Math.Abs(vidok.transform.localPosition.x), vidok.transform.localPosition.y, vidok.transform.localPosition.z);
        }
        else if(rb.velocity.x > 0)
        {
            sprite.flipX = false;
            hitBox.transform.localScale = Vector3.one;
            vidok.transform.localPosition = new Vector3(Math.Abs(vidok.transform.localPosition.x), vidok.transform.localPosition.y, vidok.transform.localPosition.z);
        }
    }
    private int RandomBroj()
    {
        int NasumicniBroj = UnityEngine.Random.Range(1, 4);      
        return NasumicniBroj;
    }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<PlayerKontroler>().ozljedzen();
            }
        }
    private IEnumerator KreciSe(int broj)
    {
        switch (broj)
        {
            case 1:
                {
                    HodamLijevo();
                    break;
                }
            case 2:
                {
                    HodamDesno();
                    break;
                }
            case 3:
                {
                    Stojim();
                    break;
                }              
        }       
        yield return new WaitForSeconds(RandomBroj());
        KrecemSe=false;
    }
    private void Stojim()
    {       
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    private void HodamDesno()
    {
        rb.velocity = new Vector2(BrzinaHodanja, rb.velocity.y);
    }  
    private void HodamLijevo()
    {
        rb.velocity = new Vector2(- BrzinaHodanja, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("metak"))
        {
            hpManager.SmanjiHP(.2f);        
        }
    }
    private void PrimanjeStete()
    {
        if (hpManager.MozeAnimiratiStetu)
        {
            hpManager.MozeAnimiratiStetu = false;
            Ozljedzen = true;
            Invoke("NijeOzljedzen", 1f);
            Potresi();
            StaviCrvenuBoju();
            AnimirajOzljedu();
        }
        AkoJeMrtavIskljuciHitBox();
    }
    private void Potresi()
    {
        pozicijaPrijeTresenja = transform.position;
        TreseSe = true;

        if (TreseSe)
        {
            transform.position = pozicijaPrijeTresenja + UnityEngine.Random.insideUnitCircle * new Vector2(0.5f, 1f);
            Invoke("ResetTresi", 0.2f);
        }
    }
    private void ResetTresi()
    {
        transform.position = pozicijaPrijeTresenja;
        TreseSe = false;
    }
    private void AkoJeMrtavIskljuciHitBox()
    {
        if(!hpManager.ImaLiHP() && hitBox.activeInHierarchy)
        {
            IskljuciCollider();
        }
    }
    private void AnimirajOzljedu()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (!Napada)
        {
            animator.SetTrigger("Uplasen");
        }
        if (!hpManager.ImaLiHP())
        {
            ZmijaUmire();
        }
    }

    private void NijeOzljedzen()
    {
        Ozljedzen = false;
    }
    private void ZmijaUmire()
    {
        PostaviAnimacijuUmiranja();     
        PlayerDobiBodove();
        hpManager.UnistiHPSlider();
    }

    private void PostaviAnimacijuUmiranja()
    {
        gameObject.layer = 10;
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetBool("mrtav", true);
        animator.SetTrigger("Mrtav");
    }

    private void PlayerDobiBodove()
    {
        manager.PlayerDobivaBodove(int.Parse(brBodova));
        GameObject bod = Instantiate(bodovi, transform.position, Quaternion.identity);
        bod.transform.GetChild(0).GetComponent<TextMeshPro>().text = brBodova;
        bod.SetActive(true);
    }
    private void StaviCrvenuBoju()
    {
        sprite.color = Color.red;
        Invoke("ZadanaBoja",.2f);
    }
    private void ZadanaBoja()
    {
        sprite.color = BojaZmije;
    }
}
