using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSingleton : InheritSingleton<CameraSingleton>
{
    static CameraSingleton instace;
    static CameraSingleton Instance { get { Init(); return instace; } }

    private void Start()
    {
        Init();
    }

    static void Init()
    {
        if (instace == null)
        {
            GameObject obj = GameObject.Find("Camera");
            if (obj == null)
            {
                obj = new GameObject { name = "Camera" };
                obj.AddComponent<CameraSingleton>();
            }

            DontDestroyOnLoad(obj);
            instace = obj.GetComponent<CameraSingleton>();
        }
    }
}
