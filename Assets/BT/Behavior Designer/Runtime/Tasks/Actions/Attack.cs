using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Attack target.")]
    public class Attack : Action
    {
        [Tooltip("target Power")]
        public SharedInt force;
        [Tooltip("target animator")]
        public Animator animator;
        [Tooltip("The transform that the agent is moving towards")]
        public SharedGameObject enemy;
        [Tooltip("Attack ing?")]
        public SharedBool attacking;

        private bool triggered = false;

        public override void OnAwake()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        public override TaskStatus OnUpdate()
        {
            if (triggered)
            {
                if (attacking.Value)
                {
                    return TaskStatus.Running;
                }

                CancelAttack();
                triggered = false;
                animator.SetBool("Attcking", false);
                return TaskStatus.Success;
            }

            OnAttack(force);
            triggered = true;
            return TaskStatus.Running;
        }

        public void CancelAttack()
        {
            enemy.Value.GetComponent<NavMeshAgent>().enabled = true;
            //rigid.isKinematic = true;
            attacking = false;
        }

        public void OnAttack(SharedInt force)
        {
            attacking = true;
            enemy.Value.GetComponent<NavMeshAgent>().enabled = false;
            //rigid.isKinematic = false;
            //rigid.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}
