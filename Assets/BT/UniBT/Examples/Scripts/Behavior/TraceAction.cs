using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts.Behavior
{
    public class TraceAction : Action
    {
        private static readonly int Running = Animator.StringToHash("Running");

        [SerializeField]
        private Transform target;
        int index = 0;
        [SerializeField]
        private float speed;
        private float distance;
        [SerializeField]
        private float stoppingDistance;

        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private Enemy enemy;
        public override void Awake()
        {
            enemy = gameObject.GetComponent<Enemy>();
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            animator = gameObject.GetComponent<Animator>();
            navMeshAgent.enabled = false;
        }

        public override void Start()
        {
            navMeshAgent.enabled = true;
            SetRunning(false);
        }

        protected override Status OnUpdate()
        {
            navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speed;
            navMeshAgent.stoppingDistance = stoppingDistance;

            if (enemy.step != Enemy.STEP.TRACE)
            {
                SetRunning(false);
                return Status.Failure;
            }

            Debug.Log(target.name);
            navMeshAgent.SetDestination(target.position);
            SetRunning(true);

            if (IsDone) // 목표에 도착했을때
            {
                SetRunning(false);
                
                return Status.Success;
            }
            
            return Status.Running;
        }

        public override void Abort()
        {
            SetRunning(false);
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
