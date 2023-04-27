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
        private Transform preTarget = null;
        int index = 0;
        [SerializeField]
        private float speed;
        private float distance;
        [SerializeField]
        private float stoppingDistance;

        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private Enemy enemy;

        private bool isComplete = false;
        private bool isGoal = true;

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
        }

        protected override Status OnUpdate()
        {
            navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speed;
            navMeshAgent.stoppingDistance = stoppingDistance;
            
            if (preTarget != null) Debug.Log("이전목표: " + preTarget.gameObject.name);
            Debug.Log("목표: " + target.gameObject.name);
            Debug.Log("목표 도착: " + IsDone);
            if (target.name == "WayPointTransforms")
            {
                target = target.GetChild(index);
            }

            

            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
            {
                navMeshAgent.speed = 2.0f;

           
            }

            //if (preTarget != null && preTarget.CompareTag("WayPoint"))
            //{
                //Debug.Log("순회 목표 거리: " + (int)distance);
                //distance = Vector3.Distance(preTarget.position, navMeshAgent.transform.position);
                //if (distance <= 1.5f && !isComplete)
                //{
                //    isComplete = true;
                //    isGoal = true;
                //    return Status.Success;
                //}
                //if (distance >= 15f)
                //{
                //    GameObject wayPoint = GameObject.FindGameObjectWithTag("WayPoint");
                //    preTarget = target;
                //    target = GetShortNode(wayPoint.transform).transform;
                //    //SetWalking(true);
                //    //SetRunning(true);
                //    //isComplete = false;
                //    //speed = 4;
                //    return Status.Running;
                //}
                //isComplete = true;

            //}           

            navMeshAgent.SetDestination(target.position);

            if (IsDone) // 목표에 도착했을때
            {
                SetWalking(false);
                SetRunning(false);

                if (target.CompareTag("Player"))
                {
                    navMeshAgent.isStopped = true;
                    return Status.Success;
                }

                ++index;
                index = index % target.parent.childCount;
                preTarget = target;
                target = target.parent.GetChild(index);
                //navMeshAgent.SetDestination(target.position);
                SetWalking(true);
                return Status.Success;
            }

            
            //if (!isComplete) return Status.Running;

            if (target.CompareTag("Player"))
            {
                if(isGoal)
                {
                    GameObject wayPoint = GameObject.FindGameObjectWithTag("WayPoint");
                    preTarget = GetShortNode(wayPoint.transform).transform;
                    isGoal = false;
                }

                SetWalking(false);
                SetRunning(true);
                //navMeshAgent.SetDestination(target.position);

                return Status.Running;
            }

            SetWalking(true);
            return Status.Running;
        }

        public override void Abort()
        {
            SetWalking(false);
            SetRunning(false);
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