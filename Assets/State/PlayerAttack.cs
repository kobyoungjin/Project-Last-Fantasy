using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class PlayerAttack : CharacterStateBase
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("batIdle", false);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Player player = GetPlayer(animator);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")    // 애니메이터의 State attack찾고 
                    && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)  // 애니메이션 끝날때까지 기다리기
            {
                animator.SetBool("batIdle", true);
                animator.SetBool("isAttack", false);
            }

            if (player.GetInput().AttackInput)
            {
                animator.SetBool("isAttack", true);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}

