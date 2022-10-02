using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool moveInput;
    private bool attackInput;
    private bool getkey;
    private KeyCode keyCode;

    public bool MoveInput { get => moveInput; }
    public bool AttackInput { get => attackInput; }

    void Update()
    {
        moveInput = Input.GetMouseButton(1);
        attackInput = Input.GetMouseButton(0);

        //if (Input.GetKeyDown(keyCode))
        //    keyCode = 
    }

    void SetKeyCode(KeyCode keyCode)
    {
        this.keyCode = keyCode;
    }
    
}
