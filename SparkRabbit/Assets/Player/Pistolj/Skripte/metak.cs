using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metak : MonoBehaviour
{
    
    Rigidbody2D rb2;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rb2 = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {

        rb2.AddForce(new Vector2(2, 0));
    }
}
