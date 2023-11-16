using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwan : MonoBehaviour
{
    private Transform spwanPos;
    private GameObject player;

    private void Start()
    {
        spwanPos = GameObject.Find("SpwanPos").transform;

        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = spwanPos.transform.position; 
    }
}
