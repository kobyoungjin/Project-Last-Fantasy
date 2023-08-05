using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public enum UIEvent
    {
        Click,
        Drag,
    }
    public enum Layer
    {
        Monster = 8,
        Ground = 9,
        Block = 10
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }

    public enum CameraMode
    {
        Quarterview,
    }
}
