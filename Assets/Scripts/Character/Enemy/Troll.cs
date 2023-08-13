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
    
    private void Start()
    {
        level = 3;
        hp = 100;
        maxHp = 100;
        attackDamage = 15;
        rate = 0.5f;
        defense = 5;

        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
        //Managers.UI.MakeWorldSpace<UI_HPBar>(transform);
    }

    private void Update()
    {
        if(hp <= 0)
        {
            Dead();

            Destroy(this.gameObject);
        }
    }

    public void Damaged(float attack)
    {
        transform.LookAt(player.transform);
        
        hp -= (int)attack;
    }

    public void Dead()
    {
        navMeshAgent.isStopped = true;
        animator.SetTrigger("dead");
    }
 
}