using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    Player player;

   public Idle(Player stateMachine) : base("Idle", stateMachine)
    {
        player = (Player)stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("Enter");
    }

    public override void Excute()
    {
        Debug.Log("Excute");
        Debug.Log(player.GetInput().MoveInput);

        if (player.GetInput().MoveInput)
        {
            RaycastHit click;

            if (Physics.Raycast(player.GetMainCamera().ScreenPointToRay(Input.mousePosition), out click))  // 클릭한 지점 레이케스트
            {
                player.SetPosition(click.point);
            }
        }

        Vector3 pos = player.GetPosition();
        Debug.Log(pos);

        if(pos != Vector3.zero)
            stateMachine.ChangeState(player.runningState);
    }

    public override void Exit()
    {
       
    }

    public override void PhysicsExcute()
    {
       
    }
}

public class Running : BaseState
{
    Player player;
    bool isMove;

    public Running(Player stateMachine) : base("Running", stateMachine)
    {
        player = (Player)stateMachine;
    }

    public override void Enter()
    {
        isMove = false;
    }

    public override void Excute()
    {
        Vector3 pos = player.GetPosition() - player.transform.position;
        pos.y = 0f;

        if (pos.magnitude <= 0.1f && !isMove)
        {
            player.ChangeState(player.idleState);
            //player.ChangeAnimState(AnimState.running);
            //Debug.Log(player.currentState);
            return;
        }

        var rotate = Quaternion.LookRotation(pos);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotate, Time.deltaTime * 5f);  // 천천히 회전
        player.transform.position += pos.normalized * Time.deltaTime * player.GetSpeed();
    }

    public override void Exit()
    {

    }

    public override void PhysicsExcute()
    {

    }
}

//public class CombatIdle : BaseState
//{
//    public CombatIdle(Player stateMachine) : base("CombatIdle", stateMachine)
//    {

//    }

//    public override void Enter()
//    {
//        RaycastHit click;

//        if (Physics.Raycast(player.GetMainCamera().ScreenPointToRay(Input.mousePosition), out click))  // 클릭한 지점 레이케스트
//        {
//            Debug.Log(player.GetState());
//            player.SetPosition(click.point);
//        }
//    }

//    public override void Excute()
//    {
//        Vector3 pos = GetPosition();

//        if (pos != Vector3.zero)
//            stateMachine.ChangeState(runningState);
//    }

//    public override void Exit()
//    {

//    }

//    public override void PhysicsExcute()
//    {

//    }
//}




