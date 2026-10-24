using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportiranje : MonoBehaviour
{
    private Teleport parent;
    private void Start()
    {
        parent = GetComponentInParent<Teleport>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TeleportirajPlayera(collision);
        }
    }

    private void TeleportirajPlayera(Collider2D collision)
    {
        if (collision.transform.position.x>transform.position.x)
        {
            parent.ChildTrigger(transform, collision, Vector3.left*2);
        }
        else
        {
            parent.ChildTrigger(transform, collision, Vector3.right * 2);
        }
        
    }
}
