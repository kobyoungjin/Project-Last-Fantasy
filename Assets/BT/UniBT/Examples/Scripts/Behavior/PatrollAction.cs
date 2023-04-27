using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts.Behavior
{
    public class PatrollAction : Action
    {
        private static readonly int Walking = Animator.StringToHash("Walking");

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

            SetWalking(true);

            target = target.GetChild(index);
            navMeshAgent.SetDestination(target.position);
        }

        protected override Status OnUpdate()
        {
            navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speed;
            navMeshAgent.stoppingDistance = stoppingDistance;

            Debug.Log("Patroll: " + target.name);

            if (enemy.step != Enemy.STEP.PATROLL)
            {
                SetWalking(false);
                return Status.Failure;
            }
            

            if (IsDone) // 목표에 도착했을때
            {
                SetWalking(false);

                ++index;
                if(index >= 2)
                    index = index % target.parent.childCount;
                navMeshAgent.enabled = false;
                target = target.parent.GetChild(index);
                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(target.position);

                SetWalking(true);
                return Status.Success;
            }
            navMeshAgent.SetDestination(target.position);

            Vector3 lookRotation = target.position - this.gameObject.transform.position;
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(lookRotation), 6 * Time.deltaTime);

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

        private bool IsDone => !navMeshAgent.pathPending &&
                               (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance ||
                                Mathf.Approximately(navMeshAgent.remainingDistance, navMeshAgent.stoppingDistance));
    }
}