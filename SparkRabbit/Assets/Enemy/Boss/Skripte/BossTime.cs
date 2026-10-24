using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTime : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UpravljanjePjesmom.BossGlazba = true;
            Destroy(gameObject);
        }
    }
}
