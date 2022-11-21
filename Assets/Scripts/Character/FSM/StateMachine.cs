using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class StateMachine<T>
    {
        private T owner;

        private BaseState<T> currentState = null;
        private BaseState<T> prevState = null;

        public void Enter()
        {
            if(currentState != null)
            {
                currentState.Enter();
            }
        }

        public void Excute()
        {
            if(currentState != null)
            {
                currentState.Excute();
            }
        }

        public void Exit()
        {
            currentState.Exit();
            currentState = null;
            prevState = null;
            Debug.Log(owner.ToString() + "Á¾·á");
        }

        public void PhysicsExcute()
        {
            if (currentState != null)
            {
                currentState.PhysicsExcute();
            }
        }

        public void ChangeState(BaseState<T> newState)
        {
            if (newState == currentState)
                return;

            prevState = currentState;

            if (currentState != null)
                currentState.Exit();
            
            currentState = newState;

            if(currentState != null)
            {
                currentState.Enter();
            }            
        }

        public void SetState(BaseState<T> newState, T owner)
        {
            this.owner = owner;
            currentState = newState;

            if (currentState != newState && currentState != null)
                prevState = currentState;
        }
    }
}

