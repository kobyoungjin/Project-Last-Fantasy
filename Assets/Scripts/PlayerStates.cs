using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Idle : State
    {
        public override void Enter(Player entity)
        {
            entity.ChangeState(AnimState.idle);
        }

        public override void Excute(Player entity)
        {
           
        }

        public override void Exit(Player entity)
        {
            //if()            
            //    entity.ChangeState(AnimState.attack);
            //else if()
            //    entity.ChangeState(AnimState.running);
            //else if()
            //    entity.ChangeState(AnimState.abilityAttack);
        }
    }
}

