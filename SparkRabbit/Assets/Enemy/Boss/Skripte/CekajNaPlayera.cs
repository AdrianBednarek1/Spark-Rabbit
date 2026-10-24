using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CekajNaPlayera : StateMachineBehaviour
{
    Vector2 pozicijaBossa;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pozicijaBossa = Camera.main.WorldToScreenPoint(animator.transform.position);
        if (Screen.safeArea.Contains(pozicijaBossa))
        {
            animator.SetTrigger("Hodaj");
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossHP.HpSlider.gameObject.SetActive(true);
    }

}
