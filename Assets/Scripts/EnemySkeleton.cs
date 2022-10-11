using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour
{
    float speed = 3;
    Vector3 destination;
    Vector3 StartPos;

    //SphereCollider sphereCollider;

    void Start()
    {
        StartPos = this.transform.position;
        //sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        
    }

    public void SetPosition(Vector3 pos)
    {
        destination = pos;
    }

    public Vector3 GetPosition()
    {
        return destination;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    //void Overlap(Vector3 pos, float radius)
    //{
    //    Collider[] colls = Physics.OverlapSphere(pos, radius);

    //    if (colls.Length < 0)
    //        return;

    //    for (int i = 0; i < cols.Length; i++)
    //    {
    //        if (cols[i].CompareTag("Player"))
    //        {
    //            Debug.Log("found");
    //            destination = cols[i].gameObject.transform.position;
    //        }
    //    }

    //}
}
