using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Background1 : MonoBehaviour
{
    private float duzinaSlike, pozicijaSlike;
    public GameObject cam;
    public float eff;
    float udaljenostOdKamere;
    float efekt;
    void Start()
    {
        pozicijaSlike = transform.position.x;
        duzinaSlike = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void FixedUpdate()
    {
        udaljenostOdKamere = (cam.transform.position.x * (1 - eff));
        efekt = (cam.transform.position.x * eff);
        transform.position = new Vector3(pozicijaSlike + efekt, transform.position.y, transform.position.z);

        if ( udaljenostOdKamere > pozicijaSlike + duzinaSlike)
        {
            pozicijaSlike += duzinaSlike;
        }
        else if(udaljenostOdKamere< pozicijaSlike - duzinaSlike)
        {
            pozicijaSlike -= duzinaSlike;
        }
    }
}
