using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 3;
    Vector3 destination;

    void Start()
    {
        
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
}
