using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObj;

    void LateUpdate()
    {
        if (targetObj == null)
            return;

        float lookDis = 0;
        if (targetObj.transform.root.name == "ÇÑ½º")
            lookDis = 5f;
        else
            lookDis = 3f;

        Vector3 direction = targetObj.transform.forward * lookDis + Vector3.up * 1f + Vector3.right;
        transform.position = direction + targetObj.transform.position;// + new Vector3(3f, 3f, -4f) + TargetObj.transform.position + Vector3.zero;

        if(targetObj.name == "body")
            transform.LookAt(targetObj.transform.GetChild(3));
        else
            transform.LookAt(targetObj.transform.GetChild(2));
    }

    public void SetDiaLogTargetObject(Transform obj)
    {
        targetObj = obj.gameObject;
    }
}
