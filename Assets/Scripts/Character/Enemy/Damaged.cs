using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class Damaged : Action
    {
        Animator animator;

        public override void OnStart()
        {
            animator = this.GetComponent<Animator>();
        }
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
        public override void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Melee"))
            {
                animator.SetTrigger("hit1");
            }
        }
    }
}
