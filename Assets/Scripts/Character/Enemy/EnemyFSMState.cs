using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    public class EnemySkeletonPatrol : BaseState<EnemySkeleton>
    {
        private EnemySkeleton skeleton;
        private Animator animator;
        private NavMeshAgent navMeshAgent;

        public EnemySkeletonPatrol(EnemySkeleton owner)
        {
            this.skeleton = owner;
        }

        public override void Enter()
        {
            Debug.Log("SkeletonPattrolEnter");

            animator = skeleton.GetAnimator();

            navMeshAgent = skeleton.GetNavMeshAgent();

            //skeleton.SetPattrolling(true);
            animator.SetBool("pattrol", true);
            animator.SetBool("isMove", false);

            skeleton.CheckAndMovePoint(skeleton.GetWayPoints());
        }

        public override void Excute()
        {
            Debug.Log("SkeletonPattrolExcute");

            float distance = skeleton.GetDistance();
            animator.SetFloat("distance", distance);
            Debug.Log(navMeshAgent.destination);
            if (distance <= skeleton.GetAttackDistance())
            {
                skeleton.ChangeState(EnemySkeletonState.attack);
                return;
            }

            if (distance <= skeleton.GetTraceDistance())
                skeleton.ChangeState(EnemySkeletonState.trace);
        }

        public override void PhysicsExcute()
        {
            if (!skeleton.GetPattrolling())
                return;

            animator.SetFloat("speed", navMeshAgent.velocity.magnitude);
            skeleton.LookingForward();
            skeleton.CheckAndMovePoint(skeleton.GetWayPoints());
        }

        public override void Exit()
        {
            Debug.Log("SkeletonPattrolExit");

            skeleton.SetPattrolling(false);
            animator.SetBool("pattrol", false);
        }
    }

    public class EnemySkeletonTrace : BaseState<EnemySkeleton>
    {
        private EnemySkeleton skeleton;
        private Animator animator;
        private NavMeshAgent navMeshAgent;

        public EnemySkeletonTrace(EnemySkeleton owner)
        {
            this.skeleton = owner;
        }

        public override void Enter()
        {
            Debug.Log("SkeletonTraceEnter");

            animator = skeleton.GetAnimator();
            animator.SetBool("isMove", true);

            navMeshAgent = skeleton.GetNavMeshAgent();

            skeleton.Tracing = skeleton.GetPlayer().transform.position;
        }

        public override void Excute()
        {
            Debug.Log("SkeletonTraceExcute");

            float distance = skeleton.GetDistance();

            animator.SetFloat("distance", distance);
            if (distance <= skeleton.GetAttackDistance())
            {
                skeleton.ChangeState(EnemySkeletonState.attack);
                return;
            }

            if (distance > skeleton.GetTraceDistance())
                skeleton.ChangeState(EnemySkeletonState.patrol);
        }

        public override void PhysicsExcute()
        {
            skeleton.Tracing = skeleton.GetPlayer().transform.position;
            skeleton.LookingForward();
        }

        public override void Exit()
        {
            Debug.Log("SkeletonTraceExit");
        }
    }

    public class EnemySkeletonAttack : BaseState<EnemySkeleton>
    {
        private EnemySkeleton skeleton;
        private Animator animator;
        private NavMeshAgent navMeshAgent;

        public EnemySkeletonAttack(EnemySkeleton owner)
        {
            this.skeleton = owner;
        }

        public override void Enter()
        {
            Debug.Log("SkeletonAttackEnter");

            animator = skeleton.GetAnimator();
            animator.SetBool("isMove", false);

            skeleton.Stop();
        }

        public override void Excute()
        {
            Debug.Log("SkeletonAttackExcute");

            float distance = skeleton.GetDistance();
            animator.SetFloat("distance", distance);

            // 공격중 이동 입력이 없으면 끝까지 애니메이션 출력
            if (distance < skeleton.GetAttackDistance()) return;

            if (distance <= skeleton.GetTraceDistance())
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
                    skeleton.ChangeState(EnemySkeletonState.trace);

                return;
            }

            Debug.LogWarning("AttackToPatrol");
            //if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
             //   skeleton.ChangeState(EnemySkeletonState.patrol);
        }

        public override void Exit()
        {
            Debug.Log("SkeletonAttackExit");
            animator.SetBool("isMove", true);
        }

        public override void PhysicsExcute()
        {

        }
    }
}