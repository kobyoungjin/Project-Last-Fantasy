using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    Vector3 delta;

    [SerializeField]
    GameObject player;

    public float distance = 7.0f;   // currentZoom보다 명확한 이름으로 변경
    float minZoom = 2.0f;
    float maxZoom = 6.0f;
 
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        delta = new Vector3(0, 0.6f, -1.5f);
    }
    void LateUpdate()
    {
        
        //transform.position = new Vector3(delta.x, 3.0f + delta.y * distance, delta.z * distance) + player.transform.position + Vector3.zero;
        //transform.rotation = Quaternion.Euler(transform.rotation.x + distance, transform.rotation.y, transform.rotation.z);
        transform.position = new Vector3(0, 7.2f, -10.5f) + player.transform.position + Vector3.zero;
        transform.LookAt(player.transform);

        //CalculateZoom();
    }

    void CalculateZoom()
    {
        // 마우스 줌 인/아웃
        distance -= Input.GetAxis("Mouse ScrollWheel");

        // 줌 최소/최대 제한
        // Clamp함수 : 최대/최소값을 설정해주고 제한
        distance = Mathf.Clamp(distance, minZoom, maxZoom);
    }
}
