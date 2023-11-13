using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    Vector3 delta;
    [SerializeField]
    Define.CameraMode cameraMode = Define.CameraMode.Quarterview;
    Define.CameraMode preCameraMode = Define.CameraMode.Backview;
    [SerializeField]
    GameObject player;

    GameObject transparentObj;
    Renderer ObstacleRenderer;  // 오브젝트를 반투명하게 만들어주는 렌더러
    List<GameObject> Obstacles;

    public GameObject target;

    public float distance = 7.0f;   // currentZoom보다 명확한 이름으로 변경
    float dampTrace = 0.5f;
    float minZoom = 2.0f;
    float maxZoom = 6.0f;

    public float yPos;
    public float zPos;

    bool changed = false;

    float QuterDis;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Obstacles = new List<GameObject>(); // 새 리스트 생성
        delta = new Vector3(0, 0.6f, -1.5f);
        yPos = 7.26f;  // 8
        zPos = 5.65f;
    }

    private void Update()
    {
        //FadeOut();
        
    }
    void LateUpdate()
    {
        CalculateZoom();
    }

    private void FixedUpdate()
    {
        //Debug.Log(Mathf.Floor(Vector3.Distance(transform.position, player.transform.position) *1000f) /1000f);
        //Debug.Log(Mathf.Floor(QuterDis *1000f) /1000f);

        RaycastHit hit;
        if (cameraMode == Define.CameraMode.Quarterview)
        {
            if (Physics.Raycast(player.transform.position, delta, out hit, delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - player.transform.position).magnitude * 0.8f;
                transform.position = player.transform.position + delta.normalized * dist;
            }
            else
            {
                if(Mathf.Floor(Vector3.Distance(transform.position, player.transform.position) * 1000f) / 1000f == Mathf.Floor(QuterDis * 1000f) / 1000f)
                {
                    changed = false;
                }
                else
                {
                    changed = true;
                }

                if (preCameraMode == Define.CameraMode.Backview && changed)
                {
                    Debug.Log("s");
                    transform.position = Vector3.Lerp(transform.position, new Vector3(0, yPos, -zPos) + player.transform.position + Vector3.zero, dampTrace);
                    transform.LookAt(player.transform);
                    return;
                }

                transform.position = new Vector3(0, yPos, -zPos) + player.transform.position + Vector3.zero;
                QuterDis = Vector3.Distance(transform.position, player.transform.position);
                transform.LookAt(player.transform);
                return;
            }
        }
        else if (cameraMode == Define.CameraMode.Backview)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position - (player.transform.forward * distance)
                                                        + (Vector3.up * yPos), Time.deltaTime * dampTrace);
            transform.LookAt(player.transform);
        }
    }

    void CalculateZoom()
    {
        // 마우스 줌 인/아웃
        distance -= Input.GetAxis("Mouse ScrollWheel");

        // 줌 최소/최대 제한
        // Clamp함수 : 최대/최소값을 설정해주고 제한
        distance = Mathf.Clamp(distance, minZoom, maxZoom);
    }

    public void SetViewMode(Define.CameraMode mode)
    {
        preCameraMode = cameraMode;

        cameraMode = mode;        
        //target = obj;
    }

    public void SetTarget(GameObject obj)
    {
        target = obj;
    }

    private void FadeOut()
    {
        // Raycast를 이용하여 플레이어와 카메라 사이에 있는 오브젝트 감지
        // 오브젝트로 감지되지 않으려면 Layer를 Ignor Raycast로 바꿔놓아야 함
        // Ignore Raycast: Player, Terrain, Particles(Steam, DustStorm)
        float distance = Vector3.Distance(transform.position, player.transform.position) - 1;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        RaycastHit[] hits;

        // 카메라에서 플레이어를 향해 레이저를 쏘았을 때 맞은 오브젝트가 있다면
        hits = Physics.RaycastAll(transform.position, direction, distance);
       

        bool remove = true;
        if (Obstacles.Count != 0 && hits != null)
        {
            Debug.Log(Obstacles.Count);
            for (int i = 0; i < Obstacles.Count; i++)
            {
                foreach (var hit in hits)
                {
                    Debug.Log(hit);
                    // hit된 오브젝트가 리스트에 저장되지 않았은 것이면 계속 탐색
                    if (Obstacles[i] != hit.collider.gameObject)
                        continue;
                    // 저장된 오브젝트면 유지
                    else
                    {
                        remove = false;
                        break;
                    }
                }

                // 삭제 대상이면
                if (remove == true)
                {
                    ObstacleRenderer = Obstacles[i].GetComponent<MeshRenderer>();
                    RestoreMaterial();

                    Obstacles.Remove(Obstacles[i]);
                }
            }
        }

        if (hits.Length > 0)
        {
            // 이미 저장된 오브젝트인지 확인
            for (int i = 0; i < hits.Length; i++)
            {
                Debug.DrawRay(transform.position, direction * distance, Color.red);

                transparentObj = hits[i].collider.gameObject;

                // 이미 저장된 오브젝트이면 다음 오브젝트 검사
                if (Obstacles != null && Obstacles.Contains(transparentObj))
                    continue;

                // 저장되지 않은 오브젝트면 투명화 후 리스트에 추가
                if (transparentObj.layer == 9)
                    ObstacleRenderer = transparentObj.GetComponent<Renderer>();
                if (ObstacleRenderer != null && transparentObj != null)
                {
                    // 오브젝트를 반투명하게 렌더링한다
                    Material material = ObstacleRenderer.material;
                    Color matColor = material.color;
                    matColor.a = 0.5f;
                    material.color = matColor;

                    // 리스트에 추가
                    Obstacles.Add(transparentObj);
                    ObstacleRenderer = null;
                    transparentObj = null;
                }
            }
        }
    }

    // 기존 투명화한 오브젝트를 원상복구 하는 메소드
    void RestoreMaterial()
    {
        Material material = ObstacleRenderer.material;
        Color matColor = material.color;
        matColor.a = 1f;    // 알파값 1:불투명(원상복구)
        material.color = matColor;

        ObstacleRenderer = null;
    }
}
