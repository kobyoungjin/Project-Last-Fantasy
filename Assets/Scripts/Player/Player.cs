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

public class Player : StateMachine
{
    private Camera camera;
    private InputManager inputManager;
    private RaycastHit click;
    Animator animator;

    Vector3 destination;

    float speed = 4.0f;
    int hp;
    int mp;

    //private State[] states;
    //private State currentState;

    [HideInInspector]
    public Idle idleState;
    //[HideInInspector]
    //public CombatIdle combatIdle;
    [HideInInspector]
    public Running runningState;


    //private AnimState currentAnimState;

    void Awake()
    {
        camera = Camera.main;
        //currentAnimState = AnimState.idle;

        idleState = new Idle(this);
        runningState = new Running(this);
        //combatIdle = new CombatIdle(this);
    }

    void Start()
    {
        Debug.Log("Player scripts");
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        // gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        GetInitialState();
    }

    void Update()
    {
        currentState = idleState;

        print(currentState.name);

        
        //if (GetInput().MoveInput)
        //    ChangeState(AnimState.running);
        //else if (entity.GetInput().AttackInput)
        //    entity.ChangeState(AnimState.attack);
        //else if (entity.GetInput().KeyCodeQ)
        //    entity.ChangeState(AnimState.abilityAttack);

        //Move();
    }
    
    protected override BaseState GetInitialState()
    {
        print("Player init");
        return idleState;
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

        //Vector3 pos = GetPosition() - transform.position;
        //pos.y = 0f;

        //if (pos.magnitude <= 0.1f)
        //{
        //    ChangeState(AnimState.combatIdle);
        //    Debug.Log(currentState);
        //    return;
        //}

        //var rotate = Quaternion.LookRotation(pos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 5f);  // 천천히 회전
        //transform.position += pos.normalized * Time.deltaTime * GetSpeed();
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
    
    //public void ChangeAnimState(AnimState newState)
    //{
    //    animator.SetBool(currentAnimState.ToString(), false);
    //    currentAnimState = newState;
    //    animator.SetBool(currentAnimState.ToString(), true);
    //}

    //public void SetAnimState(AnimState newState)
    //{
    //    currentAnimState = newState;
    //}

    //public string GetState()
    //{
    //    return currentState.ToString();
    //}

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

    //public void Updated()
    //{
    //    if (currentState != null)
    //        currentState.Excute(this);
    //}

    //public override void Init(string name)
    //{
    //    base.Init(name);

    //    gameObject.name = "Player";

    //    states = new State[10];
    //    states[(int)AnimState.idle] = new PlayerStates.Idle();
    //    states[(int)AnimState.combatIdle] = new PlayerStates.CombatIdle();
    //    states[(int)AnimState.running] = new PlayerStates.Running();
    //    states[(int)AnimState.attack] = new PlayerStates.Attack();

    //    ChangeState(AnimState.idle);
    //    hp = 100;
    //    mp = 100;
    //    //currentLocation = LocationInfo.
    //}

    //public void ChangeState(AnimState newState)
    //{
    //    // 새로 바꾸려는 상태가 비어있으면 상태를 바꾸지 않는다
    //    if (states[(int)newState] == null) return;

    //    // 현재 재생중인 상태가 있으면 Exit() 메소드 호출
    //    if (currentState != null)
    //    {
    //        currentState.Exit(this);
    //    }

    //    // 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 메소드 호출
    //    currentState = states[(int)newState];
    //    currentState.Enter(this);
    //}s
}
