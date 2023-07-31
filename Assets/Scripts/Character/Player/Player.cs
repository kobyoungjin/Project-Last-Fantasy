using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace FSM
{
    public class Player : MonoBehaviour
    {
        private Camera camera;
        private InputManager inputManager;
        private RaycastHit click;
        private Animator animator;
        private GameManager gameManager;

        Vector3 destination;

        float speed = 4.0f;
        int hp;
        int mp;
        private bool isMove;

        public StateMachine<Player> currentFSM;
        public BaseState<Player>[] arrState = new BaseState<Player>[(int)PlayerState.end];

        public PlayerState currentState;
        public PlayerState prevState;

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


