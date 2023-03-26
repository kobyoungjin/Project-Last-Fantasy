using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCamera : MonoBehaviour
{
    GameObject TargetObj;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (TargetObj == null)
            return;

        transform.position = new Vector3(0, 1f, -8f) + TargetObj.transform.position + Vector3.zero;
        transform.LookAt(TargetObj.transform);
    }

    public void SetDiaLogTargetObject(Transform obj)
    {
        TargetObj = obj.gameObject;
    }
}
