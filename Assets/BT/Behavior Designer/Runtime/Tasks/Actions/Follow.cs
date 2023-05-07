using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Trace the target specified using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [TaskIcon("Assets/BT/Behavior Designer/Editor/Icons/{SkinColor}SeekIcon.png")]
    public class Follow : Action
    {
        [Tooltip("The speed of the enemy")]
        public SharedFloat speed;
        [Tooltip("Angular speed of the enemy")]
        public SharedFloat angularSpeed;
        [Tooltip("The enemy has arrived when the square magnitude is less than this value")]
        public float stoppingDistance = 0.1f;
        [Tooltip("The transform that the agent is moving towards")]
        public SharedTransform targetTransform;
        [Tooltip("If target is null then use the target position")]
        public SharedVector3 targetPosition;

        public SharedFloat distance;
        // True if the target is a transform
        private bool dynamicTarget;
        // A cache of the NavMeshAgent
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        public override void OnAwake()
        {
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            animator = gameObject.GetComponent<Animator>();
        }

        public override void OnStart()
        {
            dynamicTarget = (targetTransform != null && targetTransform.Value != null);

            navMeshAgent.enabled = true;
            navMeshAgent.speed = speed.Value;
            navMeshAgent.angularSpeed = angularSpeed.Value;            
            navMeshAgent.stoppingDistance = stoppingDistance;
            navMeshAgent.destination = Target();
        }

        public override TaskStatus OnUpdate()
        {
            if(distance.Value < 10) return TaskStatus.Failure;

            if (IsDone) // 목표에 도착했을때
            {
                //animator.SetBool("Walking", false);
                return TaskStatus.Success;
            }

            navMeshAgent.destination = Target();

            return TaskStatus.Running;
        }

        private Vector3 Target()
        {
            return targetTransform.Value.position;
        }

        private bool IsDone => !navMeshAgent.pathPending &&
                               (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance ||
                                Mathf.Approximately(navMeshAgent.remainingDistance, navMeshAgent.stoppingDistance));
    }
}