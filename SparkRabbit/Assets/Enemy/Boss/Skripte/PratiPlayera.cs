using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratiPlayera : StateMachineBehaviour
{
    Transform boss;
    Transform player;
    private float udaljenost;
    private Vector2 kretanjePremaPlayeru;
    Rigidbody2D rb;

    [SerializeField]
    float RangeNapadanja;

    [SerializeField]
    private float brzina;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        udaljenost = Vector2.Distance(player.position, boss.position);
        Vector2 meta = new Vector2(player.position.x, boss.position.y);
        kretanjePremaPlayeru = Vector2.MoveTowards(boss.position, meta, Time.fixedDeltaTime * brzina);
        AkoDodzemDoPlayera(animator);

    }
    private void AkoDodzemDoPlayera(Animator animator)
    {
        if (udaljenost <= RangeNapadanja)
        {
            animator.SetTrigger("Napad");
        }
        else
        {
            rb.MovePosition(kretanjePremaPlayeru);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Napad");
    }
}
