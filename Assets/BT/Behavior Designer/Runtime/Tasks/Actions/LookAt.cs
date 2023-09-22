using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class LookAt : Action
    {
        [Tooltip("target Transform")]
        public SharedTransform targetTransform;
        [Tooltip("Angular speed of the enemy")]
        public SharedFloat angularSpeed;

                
        public override TaskStatus OnUpdate()
        {
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f
                &&animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
            {
                return TaskStatus.Running;
            }

            var lookRotation = targetTransform.Value.position - transform.position;
            var lookAt = Quaternion.LookRotation(lookRotation);

            gameObject.transform.LookAt(targetTransform.Value.transform);

            if (Quaternion.Angle(lookAt, transform.rotation) <= 5.0f)  // 앵글각이 5보다 작으면 ok
            {
                return TaskStatus.Success;
            }

            if (lookRotation != Vector3.zero)
                transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, angularSpeed.Value * Time.deltaTime);
       
           
            return TaskStatus.Running;
        }
    }
}


