using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDest : MonoBehaviour
{
    public static DontDest Instance;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

}
