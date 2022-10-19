using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected BaseState currentState;
    

    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    void Update()
    {
        Debug.Log("StateMachine scripts" + currentState.name);
        

        if (currentState != null)
            currentState.Excute();
    }
    void LateUpdate()
    {
        if (currentState != null)
            currentState.PhysicsExcute();
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return new Idle(gameObject.GetComponent<Player>());
    }

    private void OnGUI()
    {
        string content = currentState != null ? currentState.name : "(no current state)";
        GUILayout.Label($"<color = 'black'><size=40>{content}</size></color>");
    }
}
