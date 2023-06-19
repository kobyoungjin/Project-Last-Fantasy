using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskCategory("Movement")]
    public class ChangeTarget : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        public SharedGameObject navGameObject;
        public SharedBool action;

        private GameObject[] targets;

        public override void OnStart()
        {
            GameObject watPoints = GameObject.Find("WayPointTransforms");
            targets = new GameObject[watPoints.transform.childCount];
            for (int i = 0; i < watPoints.transform.childCount; i++)
            {
                targets[i] = watPoints.transform.GetChild(i).gameObject;
            }
        }

        public override TaskStatus OnUpdate()
        { 
            if(action.Value || navGameObject.Value.GetComponent<NavMeshAgent>().isStopped)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    Debug.Log(targetGameObject.Value.name);
                    if (targetGameObject.Value.name == targets[i].name)
                    {
                        targetGameObject = targets[(i + 1) % targets.Length];
                        break;
                    }
                }

                navGameObject.Value.GetComponent<NavMeshAgent>().isStopped = false;
                action.Value = false;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}
