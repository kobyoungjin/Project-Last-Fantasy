//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace PlayerStates
//{
//    public class Idle : BaseState
//    {
//        public override void Enter()
//        {
//            //entity.ChangeState(AnimState.idle);
//        }

//        public override void Excute()
//        {
            
//        }

//        public override void Exit()
//        {
            
//        }
//    }

//    public class CombatIdle : State
//    {
//        public override void Enter(Player entity)
//        {
//            //entity.ChangeState(AnimState.combatIdle);
//        }

//        public override void Excute(Player entity)
//        {
//            //base.Excute();

//            if (entity.GetInput().AttackInput)
//                entity.ChangeState(AnimState.attack);
//            else if (entity.GetInput().MoveInput)
//                entity.ChangeState(AnimState.running);
//            else if (entity.GetInput().KeyCodeQ)
//                entity.ChangeState(AnimState.abilityAttack);
//        }

//        public override void Exit(Player entity)
//        {

//        }

//    }
//    public class Running : State
//    {
//        public override void Enter(Player entity)
//        {
//            RaycastHit click;

//            entity.ChangeState(AnimState.running);
//            if (Physics.Raycast(entity.GetMainCamera().ScreenPointToRay(Input.mousePosition), out click))  // 클릭한 지점 레이케스트
//            {
//                Debug.Log(entity.GetState());
//                entity.SetPosition(click.point);
//            }
//        }

//        public override void Excute(Player entity)
//        {
//            Vector3 pos = entity.GetPosition() - entity.transform.position;
//            pos.y = 0f;

//            if (pos.magnitude <= 0.1f)
//            {
//                entity.ChangeState(AnimState.combatIdle);
//                Debug.Log(entity.GetState());
//                return;
//            }

//            var rotate = Quaternion.LookRotation(pos);
//            entity.transform.rotation = Quaternion.Slerp(entity.transform.rotation, rotate, Time.deltaTime * 5f);  // 천천히 회전
//            entity.transform.position += pos.normalized * Time.deltaTime * entity.GetSpeed();
//        }

//        public override void Exit(Player entity)
//        {
           
//        }
//    }

//    public class Attack : State
//    {
//        public override void Enter(Player entity)
//        {
            
//        }

//        public override void Excute(Player entity)
//        {

//        }

//        public override void Exit(Player entity)
//        {
//            //if()            
//            //    entity.ChangeState(AnimState.attack);
//            //else if()
//            //    entity.ChangeState(AnimState.running);
//            //else if()
//            //    entity.ChangeState(AnimState.abilityAttack);
//        }
//    }

//    public class AbilityAttack : State
//    {
//        public override void Enter(Player entity)
//        {
//            //entity.ChangeState(AnimState.idle);
//        }

//        public override void Excute(Player entity)
//        {

//        }

//        public override void Exit(Player entity)
//        {
//            //if()            
//            //    entity.ChangeState(AnimState.attack);
//            //else if()
//            //    entity.ChangeState(AnimState.running);
//            //else if()
//            //    entity.ChangeState(AnimState.abilityAttack);
//        }
//    }
//}

