using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using FSM;

public class Boss : Status
{
    Animator animator;
    GameObject player;
    NavMeshAgent navMeshAgent;
    bool isAlive = true;

    Slider bossHP;
    GameObject getOutGateUI;
    private void Start()
    {
        level = 3;
        hp = 200;
        maxHp = 200;
        attackDamage = 25;
        rate = 1.0f;
        defense = 5;

        animator = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
        bossHP = GameObject.Find("BossCanvas").GetComponentInChildren<Slider>();
        getOutGateUI = GameObject.Find("EtcCanvas").transform.GetChild(5).gameObject;
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
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") &&
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
        hp -= (int)(attack);// / defense);
        float ratio = hp / (float)maxHp;

        bossHP.value = ratio;
    }

    public void Dead()
    {
        isAlive = false;
        navMeshAgent.isStopped = true;
        navMeshAgent.angularSpeed = 0;
        GetComponent<CapsuleCollider>().enabled = false;
        animator.SetBool("dead", true);
        Player script = player.GetComponent<Player>();
        script.GetGameManager().isClear = true;
        getOutGateUI.SetActive(true);

        //script.GetGameManager().GetQuestManager().KilledTroll();
    }

}
