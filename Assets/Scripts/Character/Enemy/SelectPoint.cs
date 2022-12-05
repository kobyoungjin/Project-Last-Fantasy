using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class SelectPoint : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //EnemySkeleton enemySkeleton = animator.gameObject.GetComponent<EnemySkeleton>();
           // enemySkeleton.Move();
        }
    }
}

