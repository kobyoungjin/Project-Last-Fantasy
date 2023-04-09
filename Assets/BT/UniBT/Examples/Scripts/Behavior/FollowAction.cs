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

            Debug.Log("목표: "+ target.GetChild(index));
            Debug.Log("목표 도착: " + IsDone);
            //Debug.Log("목표 거리: "+ Vector3.Distance(target.GetChild(index).position, navMeshAgent.transform.position));
            //if (Vector3.Distance(target.GetChild(index).position, navMeshAgent.transform.position) > 20)
            //{
            //    SetWalking(false);
            //    SetRunning(true);
            //    navMeshAgent.SetDestination(target.GetChild(index).position);

            //    return Status.Running;
            //}
            //else
                navMeshAgent.SetDestination(target.GetChild(index).position);
               
            
            if (IsDone) // 목표에 도착했을때
            {
                SetWalking(false);
                SetRunning(false);

                if (target.CompareTag("Player"))
                {
                    return Status.Success;
                }
                   
                ++index;
                index = index % target.childCount;
                navMeshAgent.SetDestination(target.GetChild(index).position);
                return Status.Running;
            }

            if (target.CompareTag("Player"))
            {
                SetWalking(false);
                SetRunning(true);
                navMeshAgent.SetDestination(target.position);

                return Status.Running;
            }

            SetWalking(true);

            return Status.Running;
        }

        public override void Abort()
        {
            SetWalking(false);
            SetRunning(false);

            navMeshAgent.SetDestination(GetShortNode(target).transform.position);
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

        private GameObject GetShortNode(Transform target)
        {
            if (target.childCount == 2)
            {
                return Vector3.Distance(navMeshAgent.transform.position, target.GetChild(0).position) 
                    < Vector3.Distance(navMeshAgent.transform.position, target.GetChild(1).position) ? target.GetChild(0).gameObject : target.GetChild(1).gameObject;
            }
           
            GameObject min = target.GetChild(0).gameObject;
            for (int i = 1; i < target.childCount; i++)
            {
                float temp = Vector3.Distance(navMeshAgent.transform.position, target.GetChild(i).position);
                if (temp < Vector3.Distance(navMeshAgent.transform.position, min.transform.position))
                    min = target.GetChild(i).gameObject;
            }

            return min;
        }

        private bool IsDone => !navMeshAgent.pathPending &&
                               (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance ||
                                Mathf.Approximately(navMeshAgent.remainingDistance, navMeshAgent.stoppingDistance));
    }
}