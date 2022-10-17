using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using PlayerState;

public enum AnimState
{
    idle = 0,
    combatIdle,
    running,
    attack,
    abilityAttack,
}

public class Player : BaseGameEntity
{
    private Camera camera;
    private InputManager inputManager;
    private RaycastHit click;
    Animator animator;

    private LocationInfo currentLocation;

    Vector3 destination;

    float speed = 4.0f;
    int hp;
    int mp;

    private AnimState[] states;
    private AnimState currentState;

    private void Awake()
    {
        camera = Camera.main;
        currentState = AnimState.idle;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
       // gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    void Update()
    {
        if (GetInput().MoveInput)
        {
            RaycastHit click;

            ChangeState(AnimState.running);
            if (Physics.Raycast(GetMainCamera().ScreenPointToRay(Input.mousePosition), out click))  // 클릭한 지점 레이케스트
            {
                Debug.Log(currentState);
                SetPosition(click.point);
            }
        }

        if (GetInput().AttackInput)
        {
            ChangeState(AnimState.attack);
            Debug.Log(currentState);
            
            return;
        }

        Move();
    }

    public void Updated()
    {

    }

    public override void Init(string name)
    {
        base.Init(name);

        gameObject.name = "Player";

        hp = 100;
        mp = 100;
        //currentLocation = LocationInfo.
    }

    void Move()
    {
       

        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ability")
        //       && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        //{
        //    animator.SetBool("batIdle", true);
        //    animator.SetBool("isAbilityAttack", false);
        //}

        //if (GetInput().AttackInput)
        //{
        //    animator.SetBool("isAttack", true);
        //}

        //if (GetInput().KeyCodeQ)
        //{
        //    animator.SetBool("isAbilityAttack", true);
        //}

        Vector3 pos = GetPosition() - transform.position;
        pos.y = 0f;

        if (pos.magnitude <= 0.1f)
        {
            ChangeState(AnimState.combatIdle);
            Debug.Log(currentState);
            return;
        }

        var rotate = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 5f);  // 천천히 회전
        transform.position += pos.normalized * Time.deltaTime * GetSpeed();
    }

    // 마우스 좌표 설정함수
    public void SetPosition(Vector3 pos)
    {
        destination = pos;
    }

    public Vector3 GetPosition()
    {
        return destination;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public InputManager GetInput()
    {
        return inputManager;
    }

    public Camera GetMainCamera()
    {
        return camera;
    }

    public void ChangeState(AnimState name)
    {
        animator.SetBool(currentState.ToString(), false);
        currentState = name;
        animator.SetBool(currentState.ToString(), true);
    }

    public void SetAnimState(AnimState name)
    {
        currentState = name;
    }

    public string GetState()
    {
        return currentState.ToString();
    }

    public int GetHP()
    {
        return hp;
    }

    public void SetHP(int hp)
    {
        this.hp = hp;
    }

    public int GetMP()
    {
        return mp;
    }

    public void SetMP(int mp)
    {
        this.mp = mp;
    }
}
