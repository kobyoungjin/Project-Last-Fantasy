using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace FSM
{
    public class Player : Status
    {
        private Camera camera;
        private InputManager inputManager;
        private RaycastHit click;
        private Animator animator;
        private GameManager gameManager;

        Vector3 destination;
        private bool isMove;

        public StateMachine<Player> currentFSM;
        public BaseState<Player>[] arrState = new BaseState<Player>[(int)PlayerState.end];

        public PlayerState currentState;
        public PlayerState prevState;

        protected int exp;
        protected int gold;

        public int Exp { get { return exp; } set { exp = value; } }
        public int Gold { get { return gold; } set { gold = value; } }

        public Player()
        {
            Init();
        }

        void Awake()
        {
            camera = Camera.main;
        }          

        void Start()
        {
            level = 1;
            hp = 100;
            maxHp = 100;
            attack = 5;
            defense = 5;
            moveSpeed = 4.0f;
            exp = 0;
            gold = 0;

            animator = GetComponent<Animator>();
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
            inputManager = gameManager.GetComponent<InputManager>();

            //gameManager.SetText(this.gameObject);
            Enter();
        }

        void Update()
        {
            Excute();
        }

        private void FixedUpdate()
        {
            PhysicsExcute();
        }

        // 초기화 함수
        public void Init()
        {
            currentFSM = new StateMachine<Player>();

            arrState[(int)PlayerState.idle] = new PlayerIdle(this);
            arrState[(int)PlayerState.combatIdle] = new PlayerCombatIdle(this);
            arrState[(int)PlayerState.running] = new PlayerRunning(this);
            arrState[(int)PlayerState.attack] = new PlayerAttack(this);
            arrState[(int)PlayerState.abilityAttack] = new PlayerAbilityAttack(this);

            currentFSM.SetState(arrState[(int)PlayerState.idle], this);
        }
       
        // 스테이트 바꿔주는 함수
        public void ChangeState(PlayerState nextState)
        {
            currentFSM.ChangeState(arrState[(int)nextState]);
        }

        public void Enter()
        {
            currentFSM.Enter();
        }

        public void Excute()
        {
            currentFSM.Excute();
        }

        public void PhysicsExcute()
        {
            currentFSM.PhysicsExcute();
        }

        public void Exit()
        {
            currentFSM.Exit();
        }

        //public void TakeDamage(EnemySkeleton skeleton)
        //{
        //    if(hp <= 0)
        //    {
        //        ChangeState(PlayerState.die);
        //        return;
        //    }

        //    EnemySkeleton enemySkeleton = new EnemySkeleton
        //    {

        //    }
        //}

        public new void Attack()
        {
            Vector3 mPosition = Input.mousePosition;
            Vector3 oPosition = transform.position;

            mPosition.z = oPosition.z - Camera.main.transform.position.z;
            Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
            float dz = target.z - oPosition.z;
            float dx = target.x - oPosition.x;
            float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, rotateDegree, 0f);
            return;
        }

        // 마우스 좌표 설정함수
        public void SetPosition(Vector3 pos)
        {
            destination = pos;
            isMove = true; 
        }

        public float GetDistance(Vector3 targetPos)
        {
            return Vector3.Distance(this.transform.position, targetPos);

        }
        public Vector3 GetTargetPosition()
        {
            return destination;
        }

        public InputManager GetInput()
        {
            return inputManager;
        }

        public Camera GetMainCamera()
        {
            return camera;
        }

        public Animator GetAnimator()
        {
            return animator;
        }
        
        public void SetCurrentState(PlayerState newState)
        {
            currentState = newState;
        }

        public void SetPrevState(PlayerState newState)
        {
            prevState = newState;
        }

        public bool GetIsMove()
        {
            return isMove;
        }

        public void SetIsMove(bool isMove)
        {
            this.isMove = isMove;
        }
    }        
}


