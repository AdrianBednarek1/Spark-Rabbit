using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GledajUPlayera : MonoBehaviour
{
    public Transform player;
    bool okreniBossa = false;
    Vector3 LijevaStrana;
    Vector3 DesnaStrana;
    void Start()
    {
        LijevaStrana = new Vector3 (-transform.localScale.x,transform.localScale.y,0);
        DesnaStrana = transform.localScale;

    }
    void Update()
    {
        if (!BossNanosenjeStete.napadam &&!BossHP.mrtav)
        {
            BossSeOkrece();
        }
        LociranjePlayera();
    }
    private void BossSeOkrece()
    {
        if (okreniBossa)
        {
            transform.localScale = LijevaStrana;
        }
        else 
        {
            transform.localScale = DesnaStrana;
        }
    }
    private void LociranjePlayera()
    {
        if (player.position.magnitude < transform.position.magnitude)
        {
            okreniBossa = true;
        }
        else
        {
            okreniBossa = false;
        }
    }
}
