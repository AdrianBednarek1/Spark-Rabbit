using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNapadanje : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossNanosenjeStete.napadam = true;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossNanosenjeStete.napadam = false;
    }

}
