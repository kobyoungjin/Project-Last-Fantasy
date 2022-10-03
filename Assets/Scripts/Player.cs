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

    private bool isAttack = false;

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

        if (inputManager.MoveInput && !isAttack)
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out click) )  // 클릭한 지점 레이케스트
            {
                Debug.Log(click.point);
                SetPosition(click.point);
            }
        }

        if (inputManager.AttackInput && !isAttack)
        {
            Attack();
            return;
        }
    
        Move();
    }

    // 플레이어 이동 함수
    private void Move()
    {
        SetAttackEnd();

        if (Vector3.Distance(destination, this.transform.position) <= 0.1f)  // 거리가 0.1 보다 작으면 정지
        {
            Stop();
            
            return;
        }

        animator.SetBool("Running", true);

        Vector3 pos = destination - this.transform.position;
        pos.y = 0f;

        var rotate = Quaternion.LookRotation(pos, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 5f);  // 천천히 회전
        transform.position += pos.normalized * Time.deltaTime * speed;

        return;
    }

    private void Stop()
    {
        animator.SetBool("Running", false);

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") 
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetBool("batIdle", true);
        }
            
        destination = transform.position;
    }

    // 마우스 좌표 설정함수
    void SetPosition(Vector3 pos)
    {
        destination = pos;
    }

    // 공격 함수
    void Attack()  
    {
        SetAttackStart();

        Stop();
    }

    private void SetAttackStart()
    {
        isAttack = true;

        animator.SetBool("isAttack", true);
        animator.SetBool("batIdle", false);
        animator.SetBool("Running", false);
    }

    private void SetAttackEnd()
    {
        isAttack = false;

        animator.SetBool("isAttack", false);
    }
}
