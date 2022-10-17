using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour
{
    float speed = 3;
    Vector3 destination;
    Vector3 StartPos;

    SphereCollider sphereCollider;

    void Start()
    {
        StartPos = this.transform.position;
        sphereCollider = this.GetComponent<SphereCollider>();
    }

    void Update()
    {
        bool overlapped = Overlap(this.transform.position, sphereCollider.radius);

        if (!overlapped)
            return;


        Vector3 pos = GetDestination() - this.transform.position;
        pos.y = 0f;
        

        if (pos.magnitude <= 0.1f)
        {
            //animator.SetBool("running", false);
            //animator.SetBool("batIdle", true);
            return;
        }

        var rotate = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 5f);  // 천천히 회전
        transform.position += pos.normalized * Time.deltaTime * GetSpeed();
    }


    bool Overlap(Vector3 pos, float radius)
    {
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 7);

        if (colls.Length == 0)
            return false ;

        

        //print(colls);
       // print(colls.Length);

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].CompareTag("Player"))
            {
                
               // Debug.Log("found");
                SetDestination(colls[i].gameObject.transform.position);
                
               // Debug.Log(Vector3.Distance(transform.position, colls[i].gameObject.transform.position));
               // Debug.Log(Vector3.Distance(pos, colls[i].gameObject.transform.position));

                return true;
            }
            else
            {
                //Debug.Log(colls[i].gameObject.name);
            }
        }
        return false;
    }

    public void SetDestination(Vector3 pos)
    {
        destination = pos;
    }

    public Vector3 GetDestination()
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

   
}
