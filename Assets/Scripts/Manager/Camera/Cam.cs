using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                
    }
    void LateUpdate()
    {
        //FadeOut();
        //CalculateZoom();
    }

    private void FixedUpdate()
    {
        //Debug.Log(Mathf.Floor(Vector3.Distance(transform.position, player.transform.position) *1000f) /1000f);
        //Debug.Log(Mathf.Floor(QuterDis *1000f) /1000f);
        //Scene scene = SceneManager.GetActiveScene();
        //if (scene.name != "Main") return;

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
        Vector3 direction = (player.transform.position - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity,
            1 << LayerMask.NameToLayer("EnvironmentObject"));

        for (int i = 0; i < hits.Length; i++)
        {
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();

            for (int j = 0; j < obj.Length; j++)
            {
                obj[j]?.BecomeTransparent();
            }
        }
    }
}
