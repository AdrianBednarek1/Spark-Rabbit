using System.Collections;
using UnityEngine;

public class PlayerKontroler : MonoBehaviour
{
    Object metakref;
    Rigidbody2D rb2;
    SpriteRenderer sprite;
    PlayerHP HPmanager;
    Animator animator;
    
    [SerializeField]
    GameObject udaracSakom;
    
    [SerializeField]
    GameObject StartCheckPoint;

    [SerializeField]
    float radiusUdarcaNoge;

    [SerializeField]
    float radiusUdarcaSake;

    [SerializeField]
    GameObject udaracNOgom;

    [SerializeField]
    Transform pod;

    [SerializeField]
    Transform pod2;

    [SerializeField]
    Transform pod3;

    [SerializeField]
    LayerMask EnemyLayer;

    [SerializeField]
    float ultraBrzina;

    [SerializeField]
    float ultraBrzinaCDSec;

    private bool ONcooldown = false;
    private float zadnja_pozicija;
    private float trenutna_pozicija;
    private bool poluNevidljiv = false;
    private float horizontala = 0f;
    private float brzinatrcanja = 6.5f;
    private float skaci = 16f;  
    private bool mozeskakati = false, dupliSkok = false;
    private float DvoklikLijevo;
    private float DvoklikDesno;
    private bool UltraBrzinaKretanje = false;
    private bool obrni = true;
    private bool boli = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        metakref = Resources.Load("metak");      
        HPmanager = GetComponent<PlayerHP>();

        PostaviTimerZaDvoklik();
        ZapamptiPozicijuPlayeraI();
    }
    void Update()
    {
        trenutna_pozicija = transform.position.y;

        KretanjeAnimacija();
        NapadanjeAnimacija();
        OzljedzenAnimacija();
        UltraBrzaAnimacija();
        SkokAnimacija();
        PucanjeAnimacija();
        OkreniPlayera();
        BrzoTrcanjeAnimacija();

        zadnja_pozicija = trenutna_pozicija;
    }
    public void NeBoli()
    {
        boli = false;
    }
    private void PostaviTimerZaDvoklik()
    {
        DvoklikLijevo = Time.time;
        DvoklikDesno = Time.time;
    }

    private void ZapamptiPozicijuPlayeraI()
    {
        zadnja_pozicija = transform.position.y;
    }

    private void OnDrawGizmos()
    {
        //Udarac nogom
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.DrawWireSphere(udaracNOgom.transform.position, radiusUdarcaNoge);

        //Udarac sakom
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.DrawWireSphere(udaracSakom.transform.position, radiusUdarcaSake);
    }  
    private void PucanjeAnimacija()
    {
        if (MoguLiPucati())
        {
            animator.SetBool("pucam", true);
            if (JesamLiNaPodu())
            {
                rb2.velocity = Vector3.zero;
            }            
            AnimacijaMetka();

            Invoke("resetBullet", 0.12f);
        }
    }
    private void AnimacijaMetka()
    {
        GameObject metak = (GameObject)Instantiate(metakref);
        metak.GetComponent<metakskripta>().StartMetak(obrni);
        PozicijaMetka(metak);
    }
    private void PozicijaMetka(GameObject metak)
    {
        if (obrni) 
        { 
            metak.transform.position = new Vector3(transform.position.x + 1.7f, transform.position.y, 0); 
        }
        else 
        { 
            metak.transform.position = new Vector3(transform.position.x - 1.7f, transform.position.y, 0); 
        }
    }
    private bool MoguLiPucati()
    {
        if(Input.GetKeyDown("b") && !animator.GetBool("pucam") && HPmanager.AkoImaHP() && !ONcooldown)
        {
            return true;
        }
        return false;
    }
    private void SkokAnimacija()
    {
        PostaviUvjeteSkakanja();
        NaKlikSkoci();
    }
    private void NaKlikSkoci()
    {
        if (Input.GetKeyDown("space") && HPmanager.AkoImaHP())
        {
            if (mozeskakati)
            {
                Skoci();
            }
            else if (dupliSkok)
            {
                IskoristiDupliSkok();
            }
        }
    }
    private void PostaviUvjeteSkakanja()
    {
        if (JesamLiNaPodu())
        {
            MoguSkakati();
        }
        else
        {
            mozeskakati = false;
            PostavljanjeAnimacijePadanjaSkakanja();
        }
    }
    private void PostavljanjeAnimacijePadanjaSkakanja()
    {
        if (DaliPadam())
        {
            animator.SetBool("zrak", false);
            animator.SetBool("pad", true);
        }
        else if(DaliLetimUZrak())
        {
            animator.SetBool("zrak", true);
            animator.SetBool("pad", false);
        }
    }

    private bool DaliLetimUZrak()
    {
        if(trenutna_pozicija > zadnja_pozicija)
        {
            return true;
        }
        return false;
    }

    private bool DaliPadam()
    {
        if(trenutna_pozicija < zadnja_pozicija && !mozeskakati)
        {
            return true;
        }
        return false;
    }
    private void MoguSkakati()
    {
        animator.SetBool("pad", false);
        animator.SetBool("zrak", false);
        mozeskakati = true;
        dupliSkok = true;
    }
    private void IskoristiDupliSkok()
    {
        dupliSkok = false;
        animator.SetBool("pad", false);
        animator.SetBool("zrak", true);
        rb2.velocity = new Vector2(rb2.velocity.x, skaci);
    }
    private void Skoci()
    {
        mozeskakati = false;
        animator.SetBool("zrak", true);    
        rb2.velocity = new Vector2(rb2.velocity.x, skaci);
    }
    private void OzljedzenAnimacija()
    {
        if (KadaJeOzljedzen())
        {
            HPmanager.smanjiHPiliZivot();
            if (HPmanager.AkoImaHP())
            {
                poluNevidljiv = true;
                
                rb2.velocity = new Vector2(0, skaci + 5);
                
                StartCoroutine(PoluNevidljiv(3.5f));
            }
            else
            {
                PlayerGubiZivotAnimacija();
            }
        }
        boli = false;
    }
    private bool KadaJeOzljedzen()
    {
        if(boli && !poluNevidljiv && !ONcooldown)
        {
            return true;
        }
        return false;
    }
    private void NapadanjeAnimacija()
    {
        if (MozeNapasti())
        {
            if (JesamLiNaPodu())
            {
                rb2.velocity = Vector3.zero;
            }
            IzvediAnimacijuIDamage();
            Invoke("resetAttack", 0.3f);
        }
    }
    private void IzvediAnimacijuIDamage()
    {
        int BrojAnimacije = NasumicniBrojNapada();
        animator.SetBool("napad",true);
        animator.Play("napad" + BrojAnimacije);

        float damage = OdrediDamage(BrojAnimacije);
        NanesiDamage(damage);
    }
    private int NasumicniBrojNapada()
    {
        if (!JesamLiNaPodu())
        {
            return 3;//napad s nogom
        }
        return UnityEngine.Random.Range(1, 4);
    }
    private void NanesiDamage(float damage)
    {
        Collider2D[] udariSakom = Physics2D.OverlapCircleAll(udaracSakom.transform.position, radiusUdarcaSake, EnemyLayer);
        foreach (Collider2D protivnik in udariSakom)
        {
            if (protivnik.name.Contains("Boss"))
            {
                protivnik.GetComponent<BossHP>().SmanjiHP(damage);
            }
            else 
            {
                protivnik.GetComponent<EnemyHP>().SmanjiHP(damage);
            }           
        }
    }
    private float OdrediDamage(int r)
    {
        if (r <= 2)
        {
            return 0.3f;
        }
        return 0.4f;
    }
    private bool MozeNapasti()
    {
        if(Input.GetKeyDown("n") && !animator.GetBool("napad")&&!ONcooldown&& !boli && HPmanager.AkoImaHP())
        {
            return true;
        }
        return false;
    }
    private void UltraBrzaAnimacija()
    {
        DesniDvoklik();
        LijeviDvoklik();
    }
    private void LijeviDvoklik()
    {
        if (Input.GetKeyDown("left") && !boli && !ONcooldown)
        {
            float VrijemeLijevogKlika = Time.time - DvoklikLijevo;
            if (VrijemeLijevogKlika < .2f)//ako je dvoklik
            {   
                PostaviAnimacijuUltraBrz(-1);
                Invoke("MakniUltraBrzinu", 0.12f);
                Invoke("MakniUltraBrzCD", ultraBrzinaCDSec);
            }
            DvoklikLijevo = Time.time;
        }
    }
    private void DesniDvoklik()
    {
        if (Input.GetKeyDown("right") && !boli && !ONcooldown)
        {
            float VrijemeDesnogKlika = Time.time - DvoklikDesno;
            if (VrijemeDesnogKlika < .2f)//ako je dvoklik
            {
                PostaviAnimacijuUltraBrz(1);
                Invoke("MakniUltraBrzinu", 0.12f);
                Invoke("MakniUltraBrzCD", ultraBrzinaCDSec);
            }
            DvoklikDesno = Time.time;
        }
    }
    private void PostaviAnimacijuUltraBrz(int strana)
    {
        ONcooldown = true;
        UltraBrzinaKretanje = true;
        animator.SetBool("UltraBrz",true);
        animator.SetTrigger("UltraBrzTrigger");
        rb2.velocity = new Vector2(ultraBrzina*strana, 0);
    }
    private void MakniUltraBrzCD()
    {
        animator.SetBool("UltraBrz", false);
        ONcooldown = false;
    }
    private void MakniUltraBrzinu()
    {
        UltraBrzinaKretanje = false;
    }
    private void PlayerGubiZivotAnimacija()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        animator.Play("ozljedzen");
        animator.speed = 0;
    }
    private bool JesamLiNaPodu()
    {
        if((Physics2D.Linecast(transform.position, pod.position, 1 << LayerMask.NameToLayer("Ground"))) ||//provjerava dali smo na podu
           (Physics2D.Linecast(transform.position, pod2.position, 1 << LayerMask.NameToLayer("Ground"))) ||
           (Physics2D.Linecast(transform.position, pod3.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            return true;
        }
        return false;
    }
    IEnumerator PoluNevidljiv(float trajanje)
    {
        float pamtiTrajanje = Time.time;
        while (true)
        {
            if (trajanje <= Time.time - pamtiTrajanje)
            {
                poluNevidljiv = false;
                break;
            }
            sprite.color = new Color(1, 1, 1, 0.2f);
            yield return new WaitForSeconds(0.3f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void ozljedzen()
    {
        if (!boli && !poluNevidljiv&&!ONcooldown)
        {
            animator.SetTrigger("ozljeda");
            Invoke("ResetOzljeda", .4f);
            boli = true;
        }
    }
    private void ResetOzljeda()
    {
        animator.ResetTrigger("ozljeda");
    }
    void resetAttack()
    {
        animator.SetBool("napad",false);
    }
    void resetBullet()
    {
        animator.SetBool("pucam", false);
    }
    private void BrzoTrcanjeAnimacija()
    {
        if (mozeBrzoTrcati()&&(MoguLiSeKretatiPoZraku()||MoguLiHodatiNaPodu()))
        {
            rb2.velocity = new Vector2(horizontala * 2, rb2.velocity.y);
            animator.SetFloat("ubrzanje", 3f);
        }
    }
    private bool mozeBrzoTrcati()
    {
        if(Input.GetKey("m") && (rb2.velocity.x > 0 || rb2.velocity.x < 0)&& !UltraBrzinaKretanje)
        {
            return true;
        }
        return false;
    }
    private void OkreniPlayera()
    {
        if (Input.GetKey("left"))
        {
            obrni = false;
            sprite.flipX = true;
        }
        if (Input.GetKey("right"))
        {
            obrni = true;
            sprite.flipX = false;
        }
        OkreniGizmosNapadanja();
    }
    private void OkreniGizmosNapadanja()
    {
        if (udaracNOgom.transform.localPosition.x < 0 && obrni)
        {
            udaracNOgom.transform.localPosition = PromijeniStranuUdarcaNogom();
            udaracSakom.transform.localPosition = PromijeniStranuUdarcaRukom();
        }
        else if (udaracNOgom.transform.localPosition.x >= 0 && !obrni)
        {
            udaracNOgom.transform.localPosition = PromijeniStranuUdarcaNogom();
            udaracSakom.transform.localPosition = PromijeniStranuUdarcaRukom();
        }
    }
    private Vector3 PromijeniStranuUdarcaRukom()
    {
        return new Vector3(-udaracSakom.transform.localPosition.x, udaracSakom.transform.localPosition.y, 0);
    }

    private Vector3 PromijeniStranuUdarcaNogom()
    {
        return new Vector3(-udaracNOgom.transform.localPosition.x, udaracNOgom.transform.localPosition.y, 0);
    }
    private void KretanjeAnimacija()
    {
        if (MoguLiHodatiNaPodu()||MoguLiSeKretatiPoZraku())
        {
            horizontala = Input.GetAxisRaw("Horizontal") * brzinatrcanja;
            rb2.velocity = new Vector2(horizontala, rb2.velocity.y);
            PostavljanjeAnimacijeKretanja();
        }
    }
    private bool MoguLiSeKretatiPoZraku()
    {
        if (!JesamLiNaPodu()&& HPmanager.AkoImaHP()&&!UltraBrzinaKretanje)
        {
            return true;
        }
        return false;
    }
    private bool MoguLiHodatiNaPodu()
    {
        if (HPmanager.AkoImaHP() && JesamLiNaPodu() && !animator.GetBool("pucam")&&!animator.GetBool("napad")&&!UltraBrzinaKretanje)
        {
            return true;
        }
        return false;
    }
    private void PostavljanjeAnimacijeKretanja()
    {
        animator.SetFloat("pomak", Mathf.Abs(horizontala));
        animator.SetFloat("ubrzanje", 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            ozljedzen();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            ozljedzen();
        }
    }
}
