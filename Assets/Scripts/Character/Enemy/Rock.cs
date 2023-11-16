using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    public bool isRock;
    Rigidbody rigid;
    float angularPower = 2;
    float scaleValue = 0.1f;
    bool isShoot;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    void Update()
    {
        //transform.Rotate(Vector3.right * 30 * Time.deltaTime);
        //transform.Rotate(Vector3.forward * 30 * Time.deltaTime);
        //transform.Rotate(Vector3.up * 30 * Time.deltaTime);
    }

    IEnumerator GainPowerTimer()
    {
        yield return new WaitForSeconds(2.2f);
        isShoot = true;
    }

    IEnumerator GainPower()
    {
        while(!isShoot)
        {
            angularPower += 1f;
            //scaleValue += 0.005f;
            //transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);
            yield return null;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(!isRock && collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject, 3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isMelee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
