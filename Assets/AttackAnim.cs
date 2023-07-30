using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class AttackAnim : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack 1") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetInteger("battle", 0);
        }
    }
}
