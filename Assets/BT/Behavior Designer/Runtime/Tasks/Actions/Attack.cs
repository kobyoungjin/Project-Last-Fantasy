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

        public SharedString animationName;

        private bool triggered = false;

        public override void OnAwake()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        public override TaskStatus OnUpdate()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                //CancelAttack();
                return TaskStatus.Running;
            }


            //  CancelAttack();
            //  triggered = false;
            //return TaskStatus.Success;
            //}
            CancelAttack();
            //OnAttack(force.Value);
            // triggered = true;
            return TaskStatus.Success;
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
