using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    [SerializeField]
    private float VisinaSkakanja;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerSkace(collision);
        }
    }
    private void PlayerSkace(Collider2D collision)
    {
        float x = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x,VisinaSkakanja);
    }
}
