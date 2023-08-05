using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWayPoints : MonoBehaviour
{
    private GameObject parent;
    void Start()
    {
        parent = transform.parent.gameObject;
 
        for (int i = 0; i < this.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).transform.position = new Vector3(parent.transform.position.x 
                + Random.Range(-6, 6), 0, parent.transform.position.z + Random.Range(-6, 6));
        }
    }
}
