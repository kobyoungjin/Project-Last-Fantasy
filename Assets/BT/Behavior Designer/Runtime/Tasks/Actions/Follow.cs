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
        [Tooltip("The enemy has arrived when the square magnitude is less than this value")]
        public float stoppingDistance = 0.1f;

        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        private Transform targetTransform;
        private GameObject prevGameObject;

        public SharedFloat distance;
        // A cache of the NavMeshAgent
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        GameObject player;
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

            player = GameObject.FindGameObjectWithTag("Player");
        }

        public override TaskStatus OnUpdate()
        {
            distance.Value = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log(distance.Value);

            if (distance.Value <= 10 || targetTransform == null)
            {
                animator.SetBool("Walking", false);
                return TaskStatus.Failure;
            }

            if (IsDone)
            {
                animator.SetBool("Walking", false);
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