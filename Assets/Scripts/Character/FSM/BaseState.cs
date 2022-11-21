using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<T>
{
    private readonly T owner;

    public abstract void Enter();

    public abstract void Excute();

    public abstract void PhysicsExcute();

    public abstract void Exit();
}

public enum PlayerState
{
    idle = 0,
    combatIdle,
    running,
    attack,
    abilityAttack,
    die,
    end,
}

public enum EnemySkeletonState
{
   pattroll =0,
   trace,
   attack,
   die,
   end,
}
