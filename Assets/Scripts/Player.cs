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
    private bool cannotMove = false;

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

        if (inputManager.KeyCodeQ && !isAttack)
        {
            AbilityAttack();
            return;
        }

        Move();
    }

    // 플레이어 이동 함수
    private void Move()
    {        
        SetAttackEnd();
        animator.SetBool("isAttack", false);
        animator.SetBool("isAbilityAttack", false);

        if (Vector3.Distance(destination, this.transform.position) <= 0.1f)  // 거리가 0.1 보다 작으면 정지
        {
            Stop();
            
            return;
        }

        animator.SetBool("running", true);

        Vector3 pos = destination - this.transform.position;
        pos.y = 0f;

        var rotate = Quaternion.LookRotation(pos, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 5f);  // 천천히 회전
        transform.position += pos.normalized * Time.deltaTime * speed;

        return;
    }

    // 멈춤 함수
    private void Stop()
    {
        animator.SetBool("batIdle", true);

        if(!animator.GetBool("running"))
            animator.SetBool("batIdle", false);

        animator.SetBool("running", false);

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")    // 애니메이터의 State attack찾고 
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)  // 애니메이션 끝날때까지 기다리기
        {
            animator.SetBool("batIdle", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ability")
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
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

    // 일반 공격 함수
    void Attack()  
    {
        SetAttackStart();
        animator.SetBool("isAttack", true);
        Stop();
    }
    
    // 스킬 공격함수
    void AbilityAttack()
    {
        SetAttackStart();
        animator.SetBool("isAbilityAttack", true);
        Stop();
    }

    // 공격 시작 세팅함수
    private void SetAttackStart()
    {
        isAttack = true;
        cannotMove = true;
    }

    // 공격 끝 세팅함수
    private void SetAttackEnd()
    {
        isAttack = false;
        cannotMove = false;
    }
}
