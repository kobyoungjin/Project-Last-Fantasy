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
            float distance = Vector3.Distance(gameObject.transform.position, enemy.Value.transform.position);

            if (triggered || distance > 1.5f)
            {
                 if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking") &&
                    animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f && attacking.Value)
                    return TaskStatus.Running;

                CancelAttack();
                triggered = false;
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
            animator.SetBool("Attacking", false);
            attacking = false;
        }

        public void OnAttack(SharedInt force)
        {
            enemy.Value.GetComponent<NavMeshAgent>().enabled = false;
            animator.SetBool("Attacking", true);
            attacking = true;
            //rigid.isKinematic = false;
            //rigid.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}
