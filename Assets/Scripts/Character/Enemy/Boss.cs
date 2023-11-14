using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;

public class Boss : Status
{
    Animator animator;
    GameObject player;
    NavMeshAgent navMeshAgent;
    bool isAlive = true;

    private void Start()
    {
        level = 3;
        hp = 100;
        maxHp = 100;
        attackDamage = 25;
        rate = 1.0f;
        defense = 20;

        animator = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            hp = 0;
            if (isAlive)
            {
                Dead();
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("death 2") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    Destroy(this.gameObject, 3.0f);
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
