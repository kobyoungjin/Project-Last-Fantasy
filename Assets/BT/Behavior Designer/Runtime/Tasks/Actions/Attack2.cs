using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Attack target.")]
    public class Attack2 : Action
    {
        [Tooltip("target Power")]
        public SharedInt force;
        [Tooltip("target animator")]
        public Animator animator;
        [Tooltip("The transform that the agent is moving towards")]
        public SharedGameObject enemy;
        public SharedGameObject enemyWeapon;
        public SharedGameObject enemyWeapon2;
        public SharedInt ranAction;

        public SharedString animationName;
        private bool isFireReady = false;
        Weapon weapon;
        Weapon weapon2;
        float fireDelay;

        private bool triggered = false;

        public override void OnAwake()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        public override void OnStart()
        {
            weapon = enemyWeapon.Value.GetComponent<Weapon>();
            weapon2 = enemyWeapon2.Value.GetComponent<Weapon>();
        }

        public override TaskStatus OnUpdate()
        {
            if (weapon == null || weapon2 == null || ranAction.Value != 2)
            {
                animator.SetInteger("attack", 0);
                return TaskStatus.Failure;
            }
            fireDelay += Time.deltaTime;

            isFireReady = weapon.rate < fireDelay;

            if (isFireReady)  // 나중에 맞을때 조건 걸기
            {
                weapon.Use();
                weapon2.Use();
                fireDelay = 0;
                enemy.Value.GetComponent<NavMeshAgent>().isStopped = false;
                return TaskStatus.Success;
            }
            enemy.Value.GetComponent<NavMeshAgent>().isStopped = true;

            return TaskStatus.Running;
        }

        public void CancelAttack()
        {
            enemy.Value.GetComponent<NavMeshAgent>().enabled = true;
            //rigid.isKinematic = true;
            animator.SetBool(animationName.Value, false);
        }

        public void OnAttack(int force)
        {
            enemy.Value.GetComponent<NavMeshAgent>().enabled = false;
            //animator.SetBool("Attacking", true);
            //rigid.isKinematic = false;
            //rigid.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}
