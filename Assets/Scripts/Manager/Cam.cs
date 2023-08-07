using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    Vector3 delta;
    [SerializeField]
    Define.CameraMode cameraMode = Define.CameraMode.Quarterview;
    [SerializeField]
    GameObject player;

    public float distance = 7.0f;   // currentZoom보다 명확한 이름으로 변경
    float minZoom = 2.0f;
    float maxZoom = 6.0f;
    public float yPos;
    public float zPos;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        delta = new Vector3(0, 0.6f, -1.5f);
        yPos = 7.26f;  // 8
        zPos = 5.65f;
    }
    void LateUpdate()
    {
        if(cameraMode == Define.CameraMode.Quarterview)
        {
            RaycastHit hit;

            if (Physics.Raycast(player.transform.position, delta, out hit, delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - player.transform.position).magnitude * 0.8f;
                transform.position = player.transform.position + delta.normalized * dist;
            }
            else
            {
                //transform.position = new Vector3(delta.x, 3.0f + delta.y * distance, delta.z * distance) + player.transform.position + Vector3.zero;
                //transform.rotation = Quaternion.Euler(transform.rotation.x + distance, transform.rotation.y, transform.rotation.z);
                transform.position = new Vector3(0, yPos, -zPos) + player.transform.position + Vector3.zero;
                transform.LookAt(player.transform);
            }
        }
        

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

    public void SetQuarterView(Vector3 delta)
    {
        cameraMode = Define.CameraMode.Quarterview;
        this.delta = delta;
    }
}
