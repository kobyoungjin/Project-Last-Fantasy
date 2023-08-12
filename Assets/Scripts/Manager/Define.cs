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
        NPC = 7,
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

    public enum Scene
    {
        Unknown, // 디폴트
        Login, // 로그인 화면 씬
        Lobby, // 로비 씬
        Game, // 인게임 씬
    }
}
