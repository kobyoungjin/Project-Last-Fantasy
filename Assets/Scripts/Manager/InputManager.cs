using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputManager : MonoBehaviour
{
    private bool moveInput;
    private bool attackInput;
    private bool keyCodeQ;
    private bool quitInput;

    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool pressed = false;
    float pressedTime = 0;

    public bool MoveInput { get => moveInput; }
    public bool AttackInput { get => attackInput; }
    public bool KeyCodeQ { get => keyCodeQ; }
    public bool QuitInput { get => quitInput; }

    void Update()
    {
        moveInput = Input.GetMouseButton(1);
        attackInput = Input.GetMouseButton(0);
        keyCodeQ = Input.GetKeyDown(KeyCode.Q);
        quitInput = Input.GetKeyDown(KeyCode.Escape);

        //print("InputManager print"+moveInput);
        //if (Input.GetKeyDown(keyCode))
        //    keyCode = 
    }
    
    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0)) // PointerDown -> Press
            {
                if (!pressed) // 한번도 눌렀던적이 없었는데 클릭 들어온거면 
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    pressedTime = Time.time; // start 이후 경과 시간 (구분만 할 수 있으면 됨)
                }
                MouseAction.Invoke(Define.MouseEvent.Press); // 비효율적일 수 있음(이벤트 발생)
                pressed = true;
            }
            else // Click(금방 뗏을 때) -> PointerUp(좀 오래 눌려있다가 뗏을 때)
            {
                if (pressed)  // 클릭 이벤트는 안들어왔는데 눌린 상태였었다면
                {
                    if (Time.time < pressedTime + 0.2f) // 0.2초 내에 뗐을때
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
                }
                pressed = false;
                pressedTime = 0;
            }
        }
    }
    
}

