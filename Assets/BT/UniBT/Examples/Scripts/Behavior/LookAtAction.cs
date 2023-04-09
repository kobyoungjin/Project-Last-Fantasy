using UnityEngine;

namespace UniBT.Examples.Scripts.Behavior
{
    public class LookAtAction : Action
    {
        [SerializeField]
        private bool useLerp = false;

        [SerializeField]
        private Transform target;

        private Transform transform;

        public override void Awake()
        {
            transform = gameObject.transform;
        }

        protected override Status OnUpdate()
        {
            if (useLerp)
            {
                var from = transform.position;
                var direction = target.position - from; 
                var goal = Quaternion.LookRotation(direction);  
                if (Quaternion.Angle(goal, transform.rotation) < 5.0f)  // 앵글각이 5보다 작으면 ok
                {
                    return Status.Success;
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, goal, Time.deltaTime * 2); // 회전
                return Status.Running;
            }
            
            transform.LookAt(target);  // 타켓이 쳐다보게
            return Status.Running;

        }

    }
}