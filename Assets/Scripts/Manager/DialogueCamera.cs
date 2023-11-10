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

        Vector3 direction = TargetObj.transform.localRotation * Vector3.forward;
        transform.position = direction + new Vector3(1f, 2f, -6f) + TargetObj.transform.position + Vector3.zero;
        transform.LookAt(TargetObj.transform);
    }

    public void SetDiaLogTargetObject(Transform obj)
    {
        TargetObj = obj.gameObject;
    }
}
