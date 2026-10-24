using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrajLevela : MonoBehaviour
{
    UpravljanjeScenom upravljanjeScenom;
    private void Start()
    {
        upravljanjeScenom = GetComponent<UpravljanjeScenom>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Manager.ekstraZivoti= collision.GetComponent<PlayerHP>().stanjeZivota();
            upravljanjeScenom.SljedeciLevel();
        }
    }
}
