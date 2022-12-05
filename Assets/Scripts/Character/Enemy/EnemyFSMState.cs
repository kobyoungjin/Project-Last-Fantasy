using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    public class EnemySkeletonPattrol : BaseState<EnemySkeleton>
    {
        private EnemySkeleton skeleton;
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private List<Transform> wayPoints;

        public int nextPoint = 0;

        public EnemySkeletonPattrol(EnemySkeleton owner)
        {
            this.skeleton = owner;
        }

        public override void Enter()
        {
            Debug.Log("SkeletonPattrolEnter");

            animator = skeleton.GetAnimator();

            navMeshAgent = skeleton.navInit(navMeshAgent);

             wayPoints = skeleton.GetWayPoints();
            this.skeleton.SetCurrentState(EnemySkeletonState.pattroll);

            skeleton.SetPattrolling(true);
            animator.SetBool("pattrol", true);
            animator.SetBool("isMove", false);

            skeleton.MoveWayPoint();
        }

        public override void Excute()
        {
            Debug.Log("SkeletonPattrolExcute");

            float distance = skeleton.GetDistance();
            animator.SetFloat("distance", distance);

            if (distance <= skeleton.GetAttackDistance())
                skeleton.ChangeState(EnemySkeletonState.attack);

            if (distance <= skeleton.GetTraceDistance())
                skeleton.ChangeState(EnemySkeletonState.trace);
        }

        public override void PhysicsExcute()
        {
            if (!skeleton.GetPattrolling())
                return;
            
            animator.SetFloat("speed", navMeshAgent.velocity.magnitude);
            skeleton.LookingForward();
            skeleton.CheckToPoint();
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

            navMeshAgent = skeleton.navInit(navMeshAgent);

            skeleton.SetCurrentState(EnemySkeletonState.trace);
            
            skeleton.Tracing = skeleton.GetPlayer().transform.position;
            //skeleton.TraceTarget(skeleton.GetPlayer().transform.position);
            animator.SetBool("isMove", true);            
        }

        public override void Excute()
        {
            Debug.Log("SkeletonTraceExcute");

            float distance = skeleton.GetDistance();

            animator.SetFloat("distance", distance);
            if (distance <= skeleton.GetAttackDistance())
                skeleton.ChangeState(EnemySkeletonState.attack);

            if (distance > skeleton.GetTraceDistance())
                skeleton.ChangeState(EnemySkeletonState.pattroll);
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
            this.skeleton.SetCurrentState(EnemySkeletonState.attack);
            
            animator.SetBool("isMove", false);
            skeleton.Stop();
        }

        public override void Excute()
        {
            Debug.Log("SkeletonAttackExcute");

            float distance = skeleton.GetDistance();
            animator.SetFloat("distance", distance);

            // 공격중 이동 입력이 없으면 끝까지 애니메이션 출력
            if (distance > skeleton.GetAttackDistance() && distance <= skeleton.GetTraceDistance())
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Attack01")
                    && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
                    skeleton.ChangeState(EnemySkeletonState.trace);
            }
            else
            {
                
            }

            if (distance > skeleton.GetTraceDistance())
            {
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Attack01")
                    && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
                skeleton.ChangeState(EnemySkeletonState.pattroll);
            }
            else
            {

            }
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