using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool moveInput;
    private bool attackInput;
    private bool keyCodeQ;
    private bool quitInput;

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
}

