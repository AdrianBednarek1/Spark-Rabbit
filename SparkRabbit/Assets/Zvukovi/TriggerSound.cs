using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioClip zvuk;

    AudioSource readyFight;
    BoxCollider2D coll;

    private void Start()
    {
        readyFight = gameObject.AddComponent<AudioSource>();
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyFight.PlayOneShot(zvuk);
            coll.enabled=false;
            Destroy(gameObject,4f);
        }
    }

}
