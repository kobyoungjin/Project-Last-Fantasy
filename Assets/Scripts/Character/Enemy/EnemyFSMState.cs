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

            navMeshAgent = skeleton.transform.GetComponent<NavMeshAgent>();
            navMeshAgent.autoBraking = false;       // 균등한 속도를 위해
            navMeshAgent.updateRotation = false;   //자동으로 회전하는 기능을 비활성화
            navMeshAgent.speed = skeleton.GetPattrollSpeed();
            
            wayPoints = skeleton.GetWayPoints();
            this.skeleton.SetCurrentState(EnemySkeletonState.pattroll);
            animator.SetBool("Pattrol", true);
            skeleton.SetPattrolling(true);
            animator.SetBool("isMove", true);

            skeleton.MoveWayPoint();
        }

        public override void Excute()
        {
            Debug.Log("SkeletonPattrolExcute");

            float distance = Vector3.Distance(skeleton.GetPlayer().transform.position, skeleton.transform.position);

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
            skeleton.Move();
        }

        public override void Exit()
        {
            Debug.Log("SkeletonPattrolExit");
            skeleton.SetPattrolling(false);
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

            skeleton.SetCurrentState(EnemySkeletonState.trace);
            animator.SetBool("running", true);
            
            skeleton.Tracing = skeleton.GetPlayer().transform.position;
            animator.SetBool("isMove", true);
        }

        public override void Excute()
        {
            Debug.Log("SkeletonTraceExcute");

            float distance = Vector3.Distance(skeleton.GetPlayer().transform.position, skeleton.transform.position);
            
            if (distance <= skeleton.GetAttackDistance())
                skeleton.ChangeState(EnemySkeletonState.attack);

            if (distance > skeleton.GetTraceDistance())
                skeleton.ChangeState(EnemySkeletonState.pattroll);
        }

        public override void PhysicsExcute()
        {
            
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
            //nimator.SetBool("attack", true);
            animator.SetBool("isMove", false);
            skeleton.Stop();
        }

        public override void Excute()
        {
            Debug.Log("SkeletonAttackExcute");

            float distance = Vector3.Distance(skeleton.GetPlayer().transform.position, skeleton.transform.position);
                    
            if (distance > skeleton.GetAttackDistance() && distance <= skeleton.GetTraceDistance())
                skeleton.ChangeState(EnemySkeletonState.trace);

            if (distance > skeleton.GetTraceDistance())
                skeleton.ChangeState(EnemySkeletonState.pattroll);


            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")    // 애니메이터의 State attack찾고 
            //        && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)  // 애니메이션 끝날때까지 기다리기
            //{
            //    skeleton.ChangeState(EnemySkeletonState.pattroll);
            //}
        }

        public override void Exit()
        {
            Debug.Log("SkeletonAttackExit");
        }

        public override void PhysicsExcute()
        {

        }
    }
}