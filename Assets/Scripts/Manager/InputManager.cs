using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class InputManager : MonoBehaviour
    {
        private bool moveInput;
        private bool attackInput;
        private bool keyCodeQ;

        public bool MoveInput { get => moveInput; }
        public bool AttackInput { get => attackInput; }
        public bool KeyCodeQ { get => keyCodeQ; }

        void Update()
        {
            moveInput = Input.GetMouseButton(1);
            attackInput = Input.GetMouseButton(0);
            keyCodeQ = Input.GetKeyDown(KeyCode.Q);

            print("InputManager print"+moveInput);
            //if (Input.GetKeyDown(keyCode))
            //    keyCode = 
        }
    }
}

