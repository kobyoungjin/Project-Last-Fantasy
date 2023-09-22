using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class Damaged : Action
    {
        Animator animator;
        public SharedInt hp;

        public override void OnStart()
        {
            animator = this.GetComponent<Animator>();
        }
        public override TaskStatus OnUpdate()
        {
            hp = this.GetComponent<Troll>().Hp;
            //Debug.Log(hp);
            if(hp.Value <= 0)
            {
                this.GetComponent<BehaviorTree>().enabled = false;
                return TaskStatus.Success;
            }
                

            return TaskStatus.Failure;
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
