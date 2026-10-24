using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicanjeSlova : MonoBehaviour
{
    Rigidbody2D rb2;
    private float Ypozicija;

    [SerializeField]
    private float brzina;

    [SerializeField]
    private float razlika;

    [SerializeField]
    private bool gore;       
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        KreciSe();
        OdrediSmjer();
    }
    private void OdrediSmjer()
    {
        if (Ypozicija + razlika < rb2.velocity.y)
        {
            gore = false;
        }
        else if (Ypozicija - razlika > rb2.velocity.y)
        {
            gore = true;
        }
    }
    private void KreciSe()
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
}
