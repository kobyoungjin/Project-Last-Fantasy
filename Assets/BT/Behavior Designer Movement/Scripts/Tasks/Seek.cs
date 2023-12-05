using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Seek the target specified using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}SeekIcon.png")]
    public class Seek : NavMeshMovement
    {
        [Tooltip("The GameObject that the agent is seeking")]
        public SharedGameObject target;
        [Tooltip("If target is null then use the target position")]
        public SharedVector3 targetPosition;

        Animator animator;
        public override void OnStart()
        {
            base.OnStart();

            SetDestination(Target());

            animator = this.GetComponent<Animator>();

            if (this.gameObject.transform.root.name == "≈∏¿Ã≈∫")
            {
                //target = GameObject.Find("SpwanPos").GetComponent<Spwan>().GetPlayer();
                targetPosition = GameObject.Find("SpwanPos").GetComponent<Spwan>().GetPlayer().transform.position;
            }

            else
            {
                //target = GameObject.Find("StartPos").GetComponent<Spwan>().GetPlayer();
                targetPosition = GameObject.Find("StartPos").GetComponent<Spwan>().GetPlayer().transform.position;
            }
        }

        // Seek the destination. Return success once the agent has reached the destination.
        // Return running if the agent hasn't reached the destination yet
        public override TaskStatus OnUpdate()
        {
            int num = animator.GetInteger("attack");
            if (num != 0) return TaskStatus.Failure;


            if (HasArrived()) {
                
                return TaskStatus.Success;
            }

            SetDestination(Target());

            return TaskStatus.Running;
        }
        
        // Return targetPosition if target is null
        private Vector3 Target()
        {
            if (target.Value != null) {
                return target.Value.transform.position;
            }
            return targetPosition.Value;
        }

        public override void OnReset()
        {
            base.OnReset();
            target = null;
            targetPosition = Vector3.zero;
        }
    }
}