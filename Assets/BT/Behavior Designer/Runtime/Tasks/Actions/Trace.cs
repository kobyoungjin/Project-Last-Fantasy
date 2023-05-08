using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Trace the target specified using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [TaskIcon("Assets/BT/Behavior Designer/Editor/Icons/{SkinColor}SeekIcon.png")]
    public class Trace : Action
    {
        [Tooltip("The speed of the enemy")]
        public SharedFloat speed;
        [Tooltip("The enemy has arrived when the square magnitude is less than this value")]
        public float stoppingDistance = 1.5f;

        public SharedFloat distance;
        // A cache of the NavMeshAgent
        private NavMeshAgent navMeshAgent;
        private Animator animator;

        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        private Transform targetTransform;
        private GameObject prevGameObject;

        public override void OnAwake()
        {
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            animator = gameObject.GetComponent<Animator>();
        }

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject)
            {
                targetTransform = currentGameObject.GetComponent<Transform>();
                prevGameObject = currentGameObject;
            }

            navMeshAgent.enabled = true;
            navMeshAgent.speed = speed.Value;
            navMeshAgent.stoppingDistance = stoppingDistance;
            navMeshAgent.destination = targetTransform.position;
        }

        public override TaskStatus OnUpdate()
        {
            if (targetTransform == null)
            {
                Debug.LogWarning("Transform is null");
                return TaskStatus.Failure;
            }

            distance.Value = Vector3.Distance(gameObject.transform.position, targetTransform.transform.position);
            //Debug.Log(distance.Value);

            if (distance.Value <= stoppingDistance || distance.Value > 10)
            {
                animator.SetBool("Running", false);
                return TaskStatus.Failure;
            }

            if (IsDone) // 목표에 도착했을때
            {
                animator.SetBool("Running", false);
                return TaskStatus.Success;
            }

            navMeshAgent.destination = targetTransform.position;

            return TaskStatus.Running;
        }

        private bool IsDone => !navMeshAgent.pathPending &&
                               (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance ||
                                Mathf.Approximately(navMeshAgent.remainingDistance, navMeshAgent.stoppingDistance));
    }
}