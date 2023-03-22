using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts.Behavior
{
    public class FollowAction : Action
    {

        private static readonly int Walking = Animator.StringToHash("Walking");
        private static readonly int Running = Animator.StringToHash("Running");
        
        [SerializeField] 
        private Transform target;
        int index = 0;
        [SerializeField] 
        private float speed;

        [SerializeField] 
        private float stoppingDistance;

        private Animator animator;

        private NavMeshAgent navMeshAgent;

        public override void Awake()
        {
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            animator = gameObject.GetComponent<Animator>();
        }

        protected override Status OnUpdate()
        {
            navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speed;
            navMeshAgent.stoppingDistance = stoppingDistance;
            navMeshAgent.SetDestination(target.GetChild(index).position);
            if (IsDone)
            {
                SetWalking(false);
                ++index;
                index = index % target.childCount;
                navMeshAgent.SetDestination(target.GetChild(index).position);
                //return Status.Success;
            }

            SetWalking(true);
            return Status.Running;
        }

        public override void Abort()
        {
            SetWalking(false);
        }

        private void SetWalking(bool walking)
        {
            if (animator != null)
            {
                animator.SetBool(Walking, walking);
            }

            navMeshAgent.isStopped = !walking;
        }

        private void SetRunning(bool running)
        {
            if (animator != null)
            {
                animator.SetBool(Running, running);
            }

            navMeshAgent.isStopped = !running;
        }

        private bool IsDone => !navMeshAgent.pathPending &&
                               (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance ||
                                Mathf.Approximately(navMeshAgent.remainingDistance, navMeshAgent.stoppingDistance));
    }
}