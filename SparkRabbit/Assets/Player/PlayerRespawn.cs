using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawn;

    CapsuleCollider2D coll;
    Animator animator;
    PlayerHP hpManager;
    PlayerKontroler PlayerKontrole;

    [SerializeField]
    GameObject StartCheckPoint;  
    void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        PlayerKontrole = GetComponent<PlayerKontroler>();
        hpManager = GetComponent<PlayerHP>();
        RespawnPozicija();
    }
    private void RespawnPozicija()
    {
        respawn = StartCheckPoint.transform.position;
        transform.position = respawn;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            respawn = collision.transform.position;
            collision.GetComponent<BoxCollider2D>().enabled = false;
            collision.GetComponent<Animator>().Play("stoji");
        }
    }
    public void VratiSeNaCheckPoint()
    {
        VratiZadanePostavkePlayera();
        transform.position = respawn;   
    }
    private void VratiZadanePostavkePlayera()
    {
        PlayerKontrole.NeBoli();
        animator.Play("New Animation");
        hpManager.ResetHP();
        coll.enabled = true;
        animator.speed = 1;
    }
}
