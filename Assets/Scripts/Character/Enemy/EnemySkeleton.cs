using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    public class EnemySkeleton : MonoBehaviour
    { 
        float speed = 2;
        Vector3 destination;
        Vector3 StartPos;

        SphereCollider sphereCollider;

        private GameObject player;
        private Animator animator;
        private float maxDistanceToCheck = 6.0f;
        private float currentDistance;

        public StateMachine<EnemySkeleton> newState;
        public BaseState<EnemySkeleton>[] arrState = new BaseState<EnemySkeleton>[(int)EnemySkeletonState.end];

        public EnemySkeletonState currentState;
        public EnemySkeletonState prevState;

        // Patrol 
        public NavMeshAgent navMeshAgent;
        public List<Transform> wayPoints;
        public int nextPoint = 0;

        private Transform playerTrans;
        private Transform enemyTrans;

        private float attackDistance = 1.0f;
        private float traceDistance = 8.0f;

        public bool isDie = false;
        private WaitForSeconds ws;

        // 순찰 여부 판단 변수
        private readonly float pattrollSpeed = 2.0f;
        private readonly float traceSpeed = 2.5f;
        private float damping = 1.0f;

        private bool isPatrolling;
        // patrolling 프로퍼티 정의
        public bool Patrolling
        {
            get { return isPatrolling; }
            set
            {
                isPatrolling = value;
                if (isPatrolling)
                {
                    navMeshAgent.speed = pattrollSpeed;
                    // 순찰 상태의 회전계수
                    damping = 1.0f;
                    MoveWayPoint();
                }
            }
        }

        //추적 대상의 위치를 저장하는 변수
        private Vector3 traceTarget;
        public Vector3 Tracing
        {
            get { return traceTarget; }
            set
            {
                traceTarget = value;
                navMeshAgent.speed = traceSpeed;
                // 순찰 상태의 회전계수
                damping = 7.0f;
                TraceTarget(traceTarget);
            }
        }

        // hp
        private readonly float hp = 100.0f;

        public float Speed
        {
            get { return navMeshAgent.velocity.magnitude; }
        }

        public EnemySkeleton()
        {
            Init();
        }

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            animator = gameObject.GetComponent<Animator>();

            if (player != null)
                playerTrans = player.GetComponent<Transform>();

            enemyTrans = GetComponent<Transform>();
            ws = new WaitForSeconds(0.3f);
        }

        void Start()
        {
            StartPos = this.transform.position;
            sphereCollider = this.GetComponent<SphereCollider>();

            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.autoBraking = false;       // 균등한 속도를 위해
            navMeshAgent.updateRotation = false;   //자동으로 회전하는 기능을 비활성화
            navMeshAgent.speed = pattrollSpeed;

            GameObject wayPointsGroup = GameObject.Find("WayPointTransforms");

            if (wayPointsGroup != null)
            {
                wayPointsGroup.GetComponentsInChildren<Transform>(wayPoints);
                wayPoints.RemoveAt(0);
            }

            //MoveWayPoint();
            Enter();
        }

        void Update()
        {
            //if (!isPatrolling)
            //    return;
            //animator.SetFloat("speed", Speed);
            //Move();

            Excute();         
        }

        private void FixedUpdate()
        {
            PhysicsExcute();
        }

        public void Init()
        {
            newState = new StateMachine<EnemySkeleton>();

            arrState[(int)EnemySkeletonState.pattroll] = new EnemySkeletonPattrol(this);
            arrState[(int)EnemySkeletonState.trace] = new EnemySkeletonTrace(this);
            arrState[(int)EnemySkeletonState.attack] = new EnemySkeletonAttack(this);

            newState.SetState(arrState[(int)EnemySkeletonState.pattroll], this);
        }

        public void ChangeState(EnemySkeletonState skeletonState)
        {
            for (int i = 0; i < (int)EnemySkeletonState.end; ++i)
            {
                if (i == (int)skeletonState)
                    newState.ChangeState(arrState[(int)skeletonState]);
            }
        }

        public void Enter()
        {
            newState.Enter();
        }

        public void Excute()
        {
            newState.Excute();
        }

        public void PhysicsExcute()
        {
            newState.PhysicsExcute();
        }

        public void Exit()
        {
            newState.Exit();
        }

        public void TraceTarget(Vector3 pos)
        {
            if (navMeshAgent.isPathStale)
                return;

            navMeshAgent.destination = pos;
            navMeshAgent.isStopped = false;
        }

        public void Move()
        {
            if (navMeshAgent.isStopped == false)
            {
                Quaternion rot = Quaternion.LookRotation(navMeshAgent.desiredVelocity);
                Debug.Log(rot);
                enemyTrans.rotation = Quaternion.Slerp(enemyTrans.rotation, rot, Time.deltaTime * damping);
            }
            if (!isPatrolling)
               return;

            // 이동과 목적지에 도착했는지 여부 계산
            if (navMeshAgent.velocity.sqrMagnitude >= 0.2f * 0.2f
                && navMeshAgent.remainingDistance <= 0.5f)
            {
                ++nextPoint;
                nextPoint = nextPoint % wayPoints.Count;  // 다음 목적지 배열 계산

                MoveWayPoint();
            }
        }

        IEnumerator CheckState()
        {
            while (!isDie)
            {
                if (currentState == EnemySkeletonState.die) yield break;

                float distance = Vector3.Distance(playerTrans.position, enemyTrans.position);

                if (distance <= attackDistance)
                    currentState = EnemySkeletonState.attack;
                else if (distance <= traceDistance)
                    currentState = EnemySkeletonState.trace;
                else
                    currentState = EnemySkeletonState.pattroll;

                print(currentState);
                yield return ws;
            }
        }

        IEnumerator Action()
        {
            while (!isDie)
            {
                yield return ws;

                switch (currentState)
                {
                    case EnemySkeletonState.pattroll:
                        isPatrolling = true;
                        animator.SetBool("isMove", true);
                        break;
                    case EnemySkeletonState.trace:
                        Tracing = playerTrans.position;
                        animator.SetBool("isMove", true);
                        break;
                    case EnemySkeletonState.attack:
                        Stop();
                        animator.SetBool("isMove", false);
                        break;
                    case EnemySkeletonState.die:
                        Stop();
                        break;
                }
            }
        }

        //private void OnEnable()
        //{
        //    StartCoroutine(CheckState());
        //    StartCoroutine(Action());
        //}

        public void MoveWayPoint()
        {
            if (navMeshAgent.isPathStale)   // 최단거리 경로 계산이 끝나지 않았으면 다음을 수행하지 않음
                return;

            navMeshAgent.destination = wayPoints[nextPoint].position;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
            isPatrolling = false;
        }

        public List<Transform> GetWayPoints()
        {
            return wayPoints;
        }

        public bool GetPattrolling()
        {
            return isPatrolling;
        }

        public void SetPattrolling(bool isAct)
        {
            isPatrolling = isAct;
        }

        public void SetCurrentState(EnemySkeletonState newState)
        {
            currentState = newState;
        }

        public void SetPrevState(EnemySkeletonState newState)
        {
            prevState = newState;
        }

        public Animator GetAnimator()
        {
            return animator;
        }

        public Vector3 GetPosition()
        {
            return destination;
        }

        public void SetDestinationPos(Vector3 pos)
        {
            destination = pos;
        }

        public Vector3 GetDestinationPos()
        {
            return destination;
        }

        public float GetSpeed()
        {
            return navMeshAgent.velocity.magnitude;
        }

        public float GetDumping()
        {
            return damping;
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        public float GetPattrollSpeed()
        {
            return pattrollSpeed;
        }

        public float GetAttackDistance()
        {
            return attackDistance;
        }

        public float GetTraceDistance()
        {
            return traceDistance;
        }

        public GameObject GetPlayer()
        {
            return player;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                animator.SetBool("isInPlayer", true);
                Vector3 pos = GetDestinationPos() - other.transform.position;
                pos.y = 0f;

                if (pos.magnitude <= 0.1f)
                {
                    //animator.SetBool("running", false);
                    //animator.SetBool("batIdle", true);
                    return;
                }

                var rotate = Quaternion.LookRotation(pos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 5f);  // 천천히 회전
                transform.position += pos.normalized * Time.deltaTime * GetSpeed();
            }
        }
    }
}

