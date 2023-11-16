using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;

public class Troll : Status
{
    Animator animator;
    GameObject player;
    NavMeshAgent navMeshAgent;
    GameManager gameManager;
    bool isAlive = true;
    
    private void Start()
    {
        level = 3;
        hp = 100;
        maxHp = 100;
        attackDamage = 15;
        rate = 0.5f;
        defense = 5;

        animator = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
        Managers.UI.Make3D_UI<UI_HPBar>(transform);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.transform.GetChild(2).GetComponent<Renderer>().sharedMaterial.SetFloat("_OutLineWidth", 0);
    }

    private void Update()
    {
        if(hp <= 0)
        {
            hp = 0;
            if(isAlive)
            {
                Dead();
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("death 2") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    Destroy(this.transform.parent.gameObject, 3.0f);
                    return;
                }
                navMeshAgent.isStopped = true;
            }
        }
    }

    public void Damaged(float attack)
    {
        transform.LookAt(player.transform);
        
        hp -= (int)attack;

    }

    public void Dead()
    {
        isAlive = false;
        navMeshAgent.isStopped = true;
        navMeshAgent.angularSpeed = 0;
        transform.GetChild(2).GetComponent<BoxCollider>().enabled = false;
        animator.SetBool("dead", true);
        Player script = player.GetComponent<Player>();
        script.GetGameManager().GetQuestManager().KilledTroll();
    }
 
}