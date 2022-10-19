using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public string name;
    protected StateMachine stateMachine;

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();

    public abstract void Excute();

    public abstract void PhysicsExcute();

    public abstract void Exit();
}
