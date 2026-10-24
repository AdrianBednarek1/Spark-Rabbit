using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tranzicija : MonoBehaviour
{
    Animator animator;
    private bool zapocelaDrugaAnimacija = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("MakniTranziciju",1.5f);
    }
    private void MakniTranziciju()
    {
        if (!zapocelaDrugaAnimacija)
        {
            gameObject.SetActive(false);
        }
        zapocelaDrugaAnimacija=false;
    }
    public void PrikaziTranziciju()
    {
        gameObject.SetActive(true);
    }
    public void ZapocniDruguTranziciju()
    {
        PrikaziTranziciju();
        zapocelaDrugaAnimacija = true;
        animator.SetTrigger("Pocni");
    }
}
