using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    GameObject gateUI;

    private void Start()
    {
        gateUI = GameObject.Find("EtcCanvas").transform.GetChild(2).gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            gateUI.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gateUI.SetActive(true);
        }
    }
}
