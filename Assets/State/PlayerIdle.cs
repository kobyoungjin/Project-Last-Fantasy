using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class PlayerIdle : CharacterStateBase
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
        {
            animator.SetBool("batIdle", false);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Player player = GetPlayer(animator);

            if (player.GetInput().MoveInput)
            {
                RaycastHit click;

                if (Physics.Raycast(player.GetMainCamera().ScreenPointToRay(Input.mousePosition), out click))  // 클릭한 지점 레이케스트
                {
                    player.SetPosition(click.point);
                }
                animator.SetBool("running", true);

                return;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("isAttack")    // 애니메이터의 State attack찾고 
                    && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)  // 애니메이션 끝날때까지 기다리기
            {
                animator.SetBool("batIdle", true);
                animator.SetBool("isAttack", false);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ability")
                   && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                animator.SetBool("batIdle", true);
                animator.SetBool("isAbilityAttack", false);
            }

            if (player.GetInput().AttackInput)
            {
                animator.SetBool("isAttack", true);
            }

            if(player.GetInput().KeyCodeQ)
            {
                animator.SetBool("isAbilityAttack", true);
            }
        }


        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("idle", false);
        }
    }
    
}

