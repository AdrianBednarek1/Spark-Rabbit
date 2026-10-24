using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJeMrtav : StateMachineBehaviour
{
    private AudioSource source;

    [SerializeField]
    AudioClip zvuk;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BoxCollider2D>().enabled = false;
        animator.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        source = animator.gameObject.AddComponent<AudioSource>();
        PostaviBoolove();
        PobjedaZvuk();
    }

    private void PostaviBoolove()
    {
        BossHP.mrtav = true;
        Manager.gameFinished = true;
        UpravljanjePjesmom.BossGlazba = false;
    }

    private void PobjedaZvuk()
    {
        source.PlayOneShot(zvuk);
    }
}
