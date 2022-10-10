using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class PlayerRunning : CharacterStateBase
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("idle", false);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Player player = GetPlayer(animator);

            //animator.SetBool("isAttack", false);
            //animator.SetBool("isAbilityAttack", false);    
            if (player.GetInput().MoveInput)
            {
                RaycastHit click;

                if (Physics.Raycast(player.GetMainCamera().ScreenPointToRay(Input.mousePosition), out click))  // 클릭한 지점 레이케스트
                {
                    player.SetPosition(click.point);
                }
            }

            Vector3 pos = player.GetPosition() - player.transform.position;
            pos.y = 0f;

            if(pos.magnitude <= 0.1f)
            {
                animator.SetBool("running", false);
                animator.SetBool("batIdle", true);
                return;
            }

            var rotate = Quaternion.LookRotation(pos);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotate, Time.deltaTime * 5f);  // 천천히 회전
            player.transform.position += pos.normalized * Time.deltaTime * player.GetSpeed();

            return;
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //animator.SetBool("running", false);
        }
    }
}


