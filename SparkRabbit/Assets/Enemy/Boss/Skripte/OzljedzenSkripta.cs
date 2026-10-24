using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OzljedzenSkripta : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossHP.AnimacijaOzljedzenCD = true;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Boli");
    }
}
