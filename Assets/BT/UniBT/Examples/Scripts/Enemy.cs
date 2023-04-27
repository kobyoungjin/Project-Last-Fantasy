using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public enum STEP
        {      // 상태를 나타내는 열거체.
            NONE = -1,          // 상태 정보 없음.
            PATROLL = 0,       // 순찰 상태
            TRACE,              // 추격 상태 
            ATTACK,             // 공격 상태
        }

        public bool Attacking { get; private set; }

        [SerializeField]
        private Transform player;
        
        private Rigidbody rigid;
        
        private NavMeshAgent navMeshAgent;

        public float Distance { get; private set; }
        public STEP step = STEP.NONE;       // 현재 상태.

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            this.step = STEP.NONE;          // 현 단계 상태를 초기화.
            navMeshAgent.isStopped = false;
        }

        private void FixedUpdate()
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }

        private void Update()
        {
            Distance = Vector3.Distance(transform.position, player.position);
            //Debug.Log("거리: " + (int)Distance);
            Debug.Log("상태: " + step.ToString());
            Debug.Log(Attacking);

            if (Distance <= 1.5f) step = STEP.ATTACK;
            else if (Distance <= 10.0f)
            {
                step = STEP.TRACE;
                CancelAttack();
            }
            else step = STEP.PATROLL;
        }

        public void Attack(float force)
        {
            Attacking = true;
            navMeshAgent.enabled = false;
            //rigid.isKinematic = false;
            rigid.AddForce(Vector3.up * force, ForceMode.Impulse);

        }

        private void OnCollisionStay(Collision other)
        {
            // TODO other.collider.name cause GC.Alloc by Object.GetName
            if (Attacking && other.collider.name == "Ground" && Mathf.Abs(rigid.velocity.y) < 0.1)
            {
                CancelAttack();
            }
        }

        public void CancelAttack()
        {
            navMeshAgent.enabled = true;
            //rigid.isKinematic = true;
            Attacking = false;
        }
        
        public Transform GetPlayerPos()
        {
            return player;
        }
    }
}