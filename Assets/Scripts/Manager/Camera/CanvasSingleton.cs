using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSingleton : InheritSingleton<CanvasSingleton>
{
    static CanvasSingleton instace;
    static CanvasSingleton Instance { get { Init(); return instace; } }

    private void Start()
    {
        Init();
    }

    static void Init()
    {
        if (instace == null)
        {
            GameObject obj = GameObject.Find("EtcCanvas");
            if (obj == null)
            {
                obj = new GameObject { name = "EtcCanvas" };
                obj.AddComponent<CanvasSingleton>();
            }

            DontDestroyOnLoad(obj);
            instace = obj.GetComponent<CanvasSingleton>();
        }
    }
}
