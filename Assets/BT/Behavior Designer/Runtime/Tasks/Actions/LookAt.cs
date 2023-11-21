using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public class LookAt : Action
    {
        [Tooltip("target Transform")]
        public SharedTransform targetTransform;
        [Tooltip("Angular speed of the enemy")]
        public SharedFloat angularSpeed;

        public SharedInt ranAction;
        Animator animator;
        private GameObject Spwan;
        public override void OnStart()
        {
            base.OnStart();

            animator = this.GetComponent<Animator>();
            
        }
        public override TaskStatus OnUpdate()
        {
            int num = animator.GetInteger("attack");
            if(num != 0)
            {
                return TaskStatus.Running;
            }

            if(this.gameObject.transform.root.name == "타이탄")
                Spwan = GameObject.Find("SpwanPos").gameObject;
            else
                Spwan = GameObject.Find("StartPos").gameObject;
            targetTransform = Spwan.GetComponent<Spwan>().GetPlayer().transform;
            var lookRotation = targetTransform.Value.position - transform.position;
            var lookAt = Quaternion.LookRotation(lookRotation);

            //gameObject.transform.LookAt(targetTransform.Value.transform);

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


