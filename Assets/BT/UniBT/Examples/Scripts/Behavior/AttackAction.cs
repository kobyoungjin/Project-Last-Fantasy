using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts.Behavior
{
    public class AttackAction : Action
    {
        private static readonly int AttackAnim = Animator.StringToHash("Attacking");

        [SerializeField]
        private int force;
        [SerializeField]
        private Transform target;

        private Enemy enemy;
        private Animator animator;
        private bool triggered = false;

        public override void Awake()
        {
            enemy = gameObject.GetComponent<Enemy>();
            animator = gameObject.GetComponent<Animator>();
        }

        protected override Status OnUpdate()
        {
            float distance = Vector3.Distance(enemy.transform.position, target.position);
            //Mathf.Approximately(
            if (distance > 3.0f)
            {
                SetAttacking(false);
                return Status.Success;
            }

            if (triggered)
            {
                if (enemy.Attacking)
                {
                    return Status.Running;
                }

                SetAttacking(false);
                triggered = false;
                return Status.Success;
            }

            enemy.Attack(force);
            SetAttacking(true);
            triggered = true;
            return Status.Running;
        }

        public override void Abort()
        {
            enemy.CancelAttack();
            SetAttacking(false);
            triggered = false;
        }

        private void SetAttacking(bool attacking)
        {
            if (animator != null)
            {
                animator.SetBool(AttackAnim, attacking);
            }
        }

    }
}