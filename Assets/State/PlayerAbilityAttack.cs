using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class PlayerAbilityAttack : CharacterStateBase
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

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ability")
                 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                animator.SetBool("batIdle", true);
                animator.SetBool("isAbilityAttack", false);
            }

            if (player.GetInput().KeyCodeQ)
            {
                animator.SetBool("isAbilityAttack", true);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}

