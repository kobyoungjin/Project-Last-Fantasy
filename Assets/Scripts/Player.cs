using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera camera;
    InputManager inputManager;
    private Animator animator;
    private RaycastHit click;

    Vector3 destination;
    private bool isMove;

    float speed = 4.0f;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
    }

    void FixedUpdate()
    {
        animator.SetBool("batIdle", false);

        if (inputManager.MoveInput)
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out click) )  // 클릭한 지점 레이케스트
            {
                Debug.Log(click.point);
                SetPosition(click.point);
            }
        }

        Move();

        if(inputManager.AttackInput)
        {
            Attack();
        }
        else
        {
            animator.SetBool("isClicking", false);
        }
    }

    // 플레이어 이동 함수
    private void Move()
    {
        if (Vector3.Distance(destination, this.transform.position) <= 0.1f)  // 거리가 0.1 보다 작으면 정지
        {
            animator.SetBool("isMove", false);
            Debug.Log(isMove);
            return;
        }
        
        Vector3 pos = destination - this.transform.position;
        pos.y = 0f;

        var rotate = Quaternion.LookRotation(pos, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 5f);  // 천천히 회전
        transform.position += pos.normalized * Time.deltaTime * speed;
    }

    // 마우스 좌표 설정함수
    void SetPosition(Vector3 pos)
    {
        destination = pos;
        animator.SetBool("isMove", true);
        Debug.Log(isMove);
    }

    // 공격 함수
    void Attack()  
    {
        animator.SetTrigger("isAttack");
        animator.SetBool("isClicking", true);
    }
}
