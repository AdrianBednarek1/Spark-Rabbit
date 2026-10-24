using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metakskripta : MonoBehaviour
{
    Rigidbody2D rb2;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    public void StartMetak(bool obrni)
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 0;
        rb2 = GetComponent<Rigidbody2D>();
        rb2.constraints = RigidbodyConstraints2D.FreezePositionY;
        if (obrni)
        {
            rb2.velocity = new Vector2(20, 0);
            sprite.flipX = true;
        } 
        else { rb2.velocity = new Vector2(-20, 0); }

        Invoke("DestroySelf", .7f);

    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (gameObject.name.Contains("metak"))
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("enemy")) { Destroy(gameObject); }
        }
        else if (gameObject.name.Contains("metakPcela"))
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player")) { 
                Destroy(gameObject); 
            }
        }      
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
