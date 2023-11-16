using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace FSM
{
    public class Player : Status
    {
        private InputManager inputManager;
        private Animator animator;
        private GameManager gameManager;
        private MouseManager mouseManager;

        private Vector3 destination;
        private RaycastHit click;
        private Rigidbody playerRigidbody;

        public StateMachine<Player> currentFSM;
        public BaseState<Player>[] arrState = new BaseState<Player>[(int)PlayerState.end];

        static Player instace;
        static Player Instance { get { HInit(); return instace; } }


        public PlayerState currentState;
        public PlayerState prevState;
        Weapon weapon;

        private float turnSpeed = 300f;
        private bool isMove;
        private bool goAttack = false;
        private bool isFireReady = false;

        float fireDelay;

        protected int exp;
        protected int gold;
        private GameObject cave;
        private GameObject ectCanvas;

        private Button indexBtn;
        private GameObject target;
        Scene scene;

        public int Exp { get { return exp; } set { exp = value; } }
        public int Gold { get { return gold; } set { gold = value; } }

        public Player()
        {
            Init();
        }

        void Awake()
        {
            this.playerRigidbody = GetComponent<Rigidbody>();

            HInit();
        }

        void Start()
        {
            level = 1;
            hp = 100;
            maxHp = 100;
            attackDamage = 30;
            defense = 5;
            moveSpeed = 4.0f;
            rate = 0.4f;
            exp = 0;
            gold = 0;
            isMove = true;

            scene = SceneManager.GetActiveScene();
            if(scene.name == "Heian")
            {
                weapon = GameObject.FindGameObjectWithTag("Melee").GetComponent<Weapon>();
                //Managers.UI.Make3D_UI<UI_HPBar>(transform);
                cave = GameObject.Find("Cave").gameObject;

            }
     
            animator = GetComponent<Animator>();
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
            inputManager = gameManager.GetComponent<InputManager>();
            mouseManager = gameManager.GetComponent<MouseManager>();
            ectCanvas = GameObject.Find("EtcCanvas").gameObject;
            indexBtn = ectCanvas.transform.GetChild(1).GetChild(2).GetComponent<Button>();

            Enter();
        }

        void Update()
        {
            fireDelay += Time.deltaTime;

            Excute();

            if(scene.name == "Heian")
            {
                if (gameManager.talkPanel.activeSelf && inputManager.QuitInput)
                {
                    gameManager.talkIndex = 0;
                    gameManager.talkPanel.SetActive(false);
                    ectCanvas.transform.GetChild(3).gameObject.SetActive(true);
                    gameManager.ChangeCamera(gameManager.dialogueCamera, gameManager.mainCamera);
                    SetIsMove(true);
                    gameManager.isAction = false;
                }

                if(gameManager.talkPanel.activeSelf && inputManager.InteractionInput)
                {
                    gameManager.Action(target);
                }
            }

            if (inputManager.QuestInput && !gameManager.isAction)
            {
                if (ectCanvas.transform.GetChild(3).gameObject.activeSelf)
                    ectCanvas.transform.GetChild(3).gameObject.SetActive(false);
                else
                    ectCanvas.transform.GetChild(3).gameObject.SetActive(true);
            }
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

        static void HInit()
        {
            if (instace == null)
            {
                GameObject obj = GameObject.Find("Player");
                if (obj == null)
                {
                    obj = new GameObject { name = "Player" };
                    obj.AddComponent<Player>();
                }

                DontDestroyOnLoad(obj);
                instace = obj.GetComponent<Player>();
            }
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

        public void Turn()
        {
            if (animator.GetInteger("attack") > 0) return;

            //RaycastHit hit;
            Vector3 targetPos = Vector3.zero;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out click, 10000f))
            {
                targetPos = click.point;
            }
            transform.LookAt(targetPos);
        }

        public void SetMousePoint(RaycastHit click)
        {
            SetPosition(click.point);
            //Debug.Log(click.point);
            GetMouseManager().SetPos(destination);
            
            if (click.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                GetMouseManager().SetMovePointer(false);
            }

            if (click.collider.gameObject.layer == (int)Define.Layer.NPC)
            {
                //SetIsMove(false);
                target = click.collider.transform.parent.gameObject;
                //transform.LookAt(click.collider.transform);
                ectCanvas.transform.GetChild(3).gameObject.SetActive(false);
                gameManager.Action(click.collider.transform.parent.gameObject);
                GetMouseManager().SetMovePointer(false);
            }


            float distance = Vector3.Distance(this.transform.position, destination);
            if (click.transform.gameObject.CompareTag("NPC") && distance <= 1.0f)
            {
                //gameManager.Action(click.collider.gameObject);
                
                SetIsMove(false);
                return;
            }
        }

        public void Attack()
        {
            if (weapon == null) return;

            Turn();

            isFireReady = weapon.rate < fireDelay;

            if (isFireReady)  // 나중에 맞을때 조건 걸기
            {
                weapon.Use();
                fireDelay = 0;
            }
        }

#region get, set함수

        // 마우스 좌표 설정함수
        public void SetPosition(Vector3 pos)
        {
            destination = pos;
            SetIsMove(true);
        }

        public Vector3 GetTargetPosition()
        {
            return destination;
        }

        public InputManager GetInput()
        {
            return inputManager;
        }
        public Animator GetAnimator()
        {
            return animator;
        }

        public GameManager GetGameManager()
        {
            return gameManager;
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
            if (!isMove)
                this.animator.StopPlayback();
            //else
            //    this.animator.StartPlayback();

            this.isMove = isMove;
        }

        public MouseManager GetMouseManager()
        {
            return mouseManager;
        }
        public void SetAttackStart()
        {
            return;
        }

        public void SetAttackEnd()
        {
            return;
        }

        #endregion

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                switch (other.transform.root.name)
                {
                    case "트롤":
                        Troll troll = gameManager.GetTrollScript();
                        this.hp -= (int)(troll.AttackDamage * 0.3);
                        float ratio = this.hp / (float)maxHp;
                        ectCanvas.transform.GetChild(0).GetComponent<Slider>().value = ratio;
                        break;
                    default:
                        break;
                }
                return;
            }

            if(other.gameObject.CompareTag("Rock"))
            {
               
                this.hp -= (int)(other.gameObject.GetComponent<Rock>().damage);
                float ratio = this.hp / (float)maxHp;
                return;
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            bool changed = true;

            if (collision.gameObject.layer == LayerMask.NameToLayer("Bridge") && changed) 
            {
                changed = false;
                gameManager.mainCamera.GetComponent<Cam>().SetTarget(cave);
                gameManager.mainCamera.GetComponent<Cam>().SetViewMode(Define.CameraMode.Backview);
                //gameManager.GetAnimationManager().SetFadeScene("Dungeon", 1.0f);
            }
            else
            {
                changed = true;
                gameManager.mainCamera.GetComponent<Cam>().SetTarget(this.gameObject);
                gameManager.mainCamera.GetComponent<Cam>().SetViewMode(Define.CameraMode.Quarterview);
            }
        }

        //public void OnCollisionExit(Collision collision)
        //{
        //    if (collision.gameObject.layer == LayerMask.NameToLayer("Bridge"))
        //    {
        //        gameManager.mainCamera.GetComponent<Cam>().SetTarget(this.gameObject);
        //        gameManager.mainCamera.GetComponent<Cam>().SetViewMode(Define.CameraMode.Quarterview);
        //    }
        //}
    }
}


