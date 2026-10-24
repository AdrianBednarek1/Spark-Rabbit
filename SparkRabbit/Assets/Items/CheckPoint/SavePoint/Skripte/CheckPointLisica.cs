using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointLisica : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject Tekst;

    float PozicijaPlayera;
    bool saved = false;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
   
    }
    void Update()
    {
        GledajUPlayera();
        PozicijaPlayera = player.transform.position.x;
        
    }

    private void AnimiraniTekst()
    {
        if (!saved)
        {
            saved = true;
            GameObject ParentTekst = Instantiate(Tekst, transform.position, Quaternion.identity);
        }
    }
    private void GledajUPlayera()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("stoji")) 
        {
            AnimiraniTekst();          
            OkreniPlayera();
        }
    }
    private void OkreniPlayera()
    {
        if (PozicijaPlayera < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
