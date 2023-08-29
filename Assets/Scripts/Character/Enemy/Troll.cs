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
    bool isAlive = true;
    
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
        Managers.UI.Make3D_UI<UI_HPBar>(transform);
    }

    private void Update()
    {
        if(hp <= 0 && isAlive)
        {
            hp = 0;
            Dead();

            StopCoroutine("Destroy");
            StartCoroutine("Destroy");
        }
    }

    public void Damaged(float attack)
    {
        transform.LookAt(player.transform);
        
        hp -= (int)attack;

    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
    }

    public void Dead()
    {
        isAlive = false;
        navMeshAgent.isStopped = true;
        transform.GetChild(2).GetComponent<BoxCollider>().enabled = false;
        animator.SetTrigger("dead");
    }
 
}