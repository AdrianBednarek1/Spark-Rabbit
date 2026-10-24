using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igrac_menu : MonoBehaviour
{
    public Animator animator;
    
    private float zadnja_pozicija;
    private float trenutna_pozicija;

    [SerializeField]
    Transform pod;
    void Start()
    {
        animator = GetComponent<Animator>();
        zadnja_pozicija = transform.position.y;
    }
    private void FixedUpdate()
    {
        trenutna_pozicija = transform.position.y;

        UpravljajAnimacijomPadanja();

        zadnja_pozicija = trenutna_pozicija;
    }

    private void UpravljajAnimacijomPadanja()
    {
        if (JesamLiNaPodu()) //provjerava dali smo na podu         
        {
            animator.SetBool("pad", false);
            animator.SetBool("zrak", false);
        }
        else
        {
            if (trenutna_pozicija < zadnja_pozicija)//ako pada, stavi animaciju da pada
            {
                animator.SetBool("zrak", false);
                animator.SetBool("pad", true);
            }
        }
    }
    private bool JesamLiNaPodu()
    {
        if(Physics2D.Linecast(transform.position, pod.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            return true;
        }
        return false;
    }
}
