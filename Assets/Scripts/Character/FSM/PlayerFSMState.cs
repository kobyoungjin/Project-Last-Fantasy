using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class PlayerIdle : BaseState<Player>
    {
        private Player player;
        private Animator animator;

        public PlayerIdle(Player owner)
        {
            this.player = owner;
        }

        public override void Enter()
        {
            //Debug.Log("PlayerIdleEnter");

            animator = player.GetAnimator();
            this.player.SetCurrentState(PlayerState.idle);
            animator.SetInteger("idle", 0);
        }

        public override void Excute()
        {
            //Debug.Log("PlayerIdleExcute");
            //Debug.Log(player.GetInput().MoveInput);
            RaycastHit click;
            if (player.GetInput().MoveInput)
            {
                if (Physics.Raycast(player.GetMainCamera().ScreenPointToRay(Input.mousePosition), out click))  // 클릭한 지점 레이케스트
                {
                    player.SetPosition(click.point);
                    Debug.Log(click.collider.gameObject.name);
                    if(click.collider.gameObject.layer == (int)Define.Layer.Monster || click.collider.gameObject.layer == (int)Define.Layer.NPC)
                    {
                        player.GetMouseManager().SetMovePointer(false);
                    }
                    else
                    {
                        player.GetMouseManager().SetPos(click.point);
                        player.GetMouseManager().SetMovePointer(true);
                    }

                    float distance = player.GetDistance();
                    if (click.transform.gameObject.CompareTag("NPC") && distance <= 0.2f)
                    {
                        return;
                    }

                    player.ChangeState(PlayerState.running);
                }
                return;

            }

            if (player.GetInput().AttackInput)
            {
                player.Attack();

                player.ChangeState(PlayerState.attack);
                return;
            }

            if (player.GetInput().KeyCodeQ)
            {
                player.ChangeState(PlayerState.abilityAttack);
                return;
            }
        }

        public override void PhysicsExcute()
        {
            return;
        }

        public override void Exit()
        {
            //Debug.Log("PlayerIdleExit");
            player.SetPrevState(PlayerState.idle);
            animator.SetBool("isIdle", false);
        }
    }

    public class PlayerCombatIdle : BaseState<Player>
    {
        private Player player;
        private Animator animator;
        private float time = 0;
        public PlayerCombatIdle(Player owner)
        {
            this.player = owner;
        }

        public override void Enter()
        {
            //Debug.Log("PlayerCombatIdleEnter");

            animator = player.GetAnimator();
            this.player.SetCurrentState(PlayerState.combatIdle);
            animator.SetInteger("idle", 1);
        }

        public override void Excute()
        {
            //Debug.Log("PlayerCombatIdleExcute");

            if (player.GetInput().MoveInput)
            {
                RaycastHit click;
                // 클릭을하면 위치 세팅
                if (Physics.Raycast(player.GetMainCamera().ScreenPointToRay(Input.mousePosition), out click))  // 클릭한 지점 레이케스트
                {
                    if (click.transform.gameObject.CompareTag("NPC"))
                    {
                        return;
                    }

                    player.SetPosition(click.point);
                    player.GetMouseManager().SetPos(click.point);
                    player.GetMouseManager().SetMovePointer(true);
                    player.ChangeState(PlayerState.running);
                }
                return;
            }

            // 공격
            if (player.GetInput().AttackInput)
            {
                player.Attack();

                player.ChangeState(PlayerState.attack);
                return;
            }


            // Q 스킬 
            if (player.GetInput().KeyCodeQ)
            {
                player.ChangeState(PlayerState.abilityAttack);
                return;
            }
        }

        public override void PhysicsExcute()
        {
            time += Time.deltaTime;
            if (time > 5.5f && !Input.anyKeyDown)
            {
                time = 0;
                player.ChangeState(PlayerState.idle);
                return;
            }


            return;
        }

        public override void Exit()
        {
            time = 0;
            // Debug.Log("PlayerCombatIdleExit");
            player.SetPrevState(PlayerState.combatIdle);
            animator.SetInteger("idle", 0);
        }
    }

    public class PlayerRunning : BaseState<Player>
    {
        private Player player;
        private Animator animator;

        public PlayerRunning(Player owner)
        {
            this.player = owner;
        }

        public override void Enter()
        {
            //Debug.Log("PlayerRunningEnter");

            animator = player.GetAnimator();

            player.SetCurrentState(PlayerState.running);
            animator.SetInteger("run", 1);
            animator.SetBool("move", true);
        }

        public override void Excute()
        {
            //Debug.Log("PlayerRunningExcute");

            if (player.GetInput().MoveInput)
            {
                RaycastHit click;

                if (Physics.Raycast(player.GetMainCamera().ScreenPointToRay(Input.mousePosition), out click))  // 클릭한 지점 레이케스트
                {
                    player.SetPosition(click.point);

                    if (click.collider.gameObject.layer == (int)Define.Layer.Monster || click.collider.gameObject.layer == (int)Define.Layer.NPC)
                    {
                        player.GetMouseManager().SetMovePointer(false);
                    }
                    else
                    {
                        player.GetMouseManager().SetPos(click.point);
                        player.GetMouseManager().SetMovePointer(true);
                    }
                }
                return;
            }

            if (player.GetInput().AttackInput)
            {
                player.Attack();

                player.ChangeState(PlayerState.attack);
                return;
            }

            if (player.GetInput().KeyCodeQ)
            {
                player.ChangeState(PlayerState.abilityAttack);
                return;
            }
        }

        public override void PhysicsExcute()
        {
            if (player.GetIsMove())
            {
                if (Vector3.Distance(player.GetTargetPosition(), player.transform.position) <= 0.1f)
                {
                    player.SetIsMove(false);
                    player.GetMouseManager().SetMovePointer(false);
                    //player.ChangeState(PlayerState.idle);
                    player.ChangeState(PlayerState.combatIdle);
                    return;
                }
            }

            var pos = player.GetTargetPosition() - player.transform.position;
            pos.y = 0f;

            // 지정된 위치까지 이동
            var rotate = Quaternion.LookRotation(pos);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotate, Time.deltaTime * 5f);  // 천천히 회전
            player.transform.position += pos.normalized * Time.deltaTime * player.MoveSpeed;
            return;
        }

        public override void Exit()
        {
            //Debug.Log("PlayerRunningExit");

            player.prevState = PlayerState.running;
            animator.SetInteger("run", 0);
            animator.SetBool("move", false);
        }
    }

    public class PlayerAttack : BaseState<Player>
    {
        private Player player;
        private Animator animator;
        bool attacking = false;
        public PlayerAttack(Player owner)
        {
            this.player = owner;
        }

        public override void Enter()
        {
            //Debug.Log("PlayerAttackEnter");

            animator = player.GetAnimator();
            this.player.SetCurrentState(PlayerState.attack);
            animator.SetInteger("attack", 1);
        }

        public override void Excute()
        {
            //Debug.Log("PlayerAttackExcute");

            if (player.GetInput().MoveInput)
            {
                RaycastHit click;

                if (Physics.Raycast(player.GetMainCamera().ScreenPointToRay(Input.mousePosition), out click))  // 클릭한 지점 레이케스트
                {
                    if (click.transform.gameObject.CompareTag("NPC"))
                    {
                        return;
                    }

                    player.SetPosition(click.point);
                    player.GetMouseManager().SetPos(click.point);
                    player.GetMouseManager().SetMovePointer(true);
                    player.ChangeState(PlayerState.running);
                }
                return;
            }

            // 공격중 이동 입력이 없으면 끝까지 애니메이션 출력
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("DefaltAttack")
                    && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                attacking = false;
                player.ChangeState(PlayerState.combatIdle);
                return;
            }
            else
            {
                attacking = true;
            }

            if (player.GetInput().AttackInput && !attacking)
            {
                animator.Play("DefaltAttack", -1, 0.1f);
                return;
            }
        }

        public override void Exit()
        {
            //Debug.Log("PlayerAttackExit");
            player.SetPrevState(PlayerState.attack);

            animator.SetInteger("attack", 0);
        }

        public override void PhysicsExcute()
        {

        }
    }

    public class PlayerAbilityAttack : BaseState<Player>
    {
        private Player player;
        private Animator animator;

        public PlayerAbilityAttack(Player owner)
        {
            this.player = owner;
        }

        public override void Enter()
        {
            //Debug.Log("PlayerAbilityEnter");

            animator = player.GetAnimator();
            this.player.SetCurrentState(PlayerState.abilityAttack);

            animator.SetInteger("attack", 2);
        }

        public override void Excute()
        {
            //Debug.Log("PlayerAbilityExcute");
            // 끝까지 애니메이션 출력
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ability")
             && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                player.ChangeState(PlayerState.combatIdle);
                return;
            }
        }

        public override void Exit()
        {
            //Debug.Log("PlayerAbilityExit");
            player.SetPrevState(PlayerState.abilityAttack);
            animator.SetInteger("attack", 0);
        }

        public override void PhysicsExcute()
        {

        }
    }
}





