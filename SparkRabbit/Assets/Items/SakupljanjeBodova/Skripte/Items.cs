using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Items : MonoBehaviour
{
    Rigidbody2D rb2;
    private bool gore = true;
    private float Ypozicija;

    [SerializeField]
    private float razlika;

    [SerializeField]
    private float brzina;

    [SerializeField] 
    GameObject bodovi;

    [SerializeField] 
    private string brBodova;

    [SerializeField]
    private Manager manager;

    [SerializeField]
    AudioClip zvukBodova;

    BodoviZvuk UpravljanjeZvukom;
    private void Awake()
    {
        UpravljanjeZvukom = GetComponentInParent<BodoviZvuk>();
    }
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        Ypozicija = rb2.velocity.y;
        
    }
    void Update()
    {
        Lebdenje();
        OdrediSmjerLebdenja();
    }

    private void OdrediSmjerLebdenja()
    {
        if (AkoJePrekoracioGornjuGranicu())
        {
            gore = false;
        }
        else if (AkojePrekoracioDonjuGranicu())
        {
            gore = true;
        }
    }

    private bool AkojePrekoracioDonjuGranicu()
    {
        if(Ypozicija - razlika > rb2.velocity.y)
        {
            return true;
        }
        return false;
    }
    private bool AkoJePrekoracioGornjuGranicu()
    {
        if(Ypozicija + razlika < rb2.velocity.y)
        {
            return true;
        }
        return false;
    }
    private void Lebdenje()
    {
        if (gore)
        {
            rb2.velocity = new Vector2(rb2.velocity.x, rb2.velocity.y + brzina);
        }
        else
        {
            rb2.velocity = new Vector2(rb2.velocity.x, rb2.velocity.y - brzina);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PustiZvuk();
            PlayerDobiBodove();
            ProvjeriJeliMrkvaIliZivot(collision);
            Destroy(gameObject); 
        }        
    }

    private void PustiZvuk()
    {
        if (zvukBodova != null)
        {
            UpravljanjeZvukom.OnChildTrigger(zvukBodova);
        }
    }

    private void ProvjeriJeliMrkvaIliZivot(Collider2D collision)
    {
        if (gameObject.name.Contains("Mrkva"))
        {
            collision.gameObject.GetComponent<PlayerHP>().ResetHP();
        }
        else if (gameObject.name.Contains("Zivot"))
        {
            collision.gameObject.GetComponent<PlayerHP>().DobivaZivot();
        }

    }
    private void PlayerDobiBodove()
    {
        manager.PlayerDobivaBodove(int.Parse(brBodova));
        GameObject bod = Instantiate(bodovi, transform.position, Quaternion.identity);
        bod.SetActive(true);
        bod.transform.GetChild(0).GetComponent<TextMeshPro>().text = brBodova;
    }
}
