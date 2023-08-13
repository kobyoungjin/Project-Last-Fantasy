using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Animator animator;
    Troll troll;
 
    private void Start()
    {
        animator = transform.GetComponentInParent<Animator>();
        troll = transform.GetComponentInParent<Troll>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {
            troll.Damaged(30);
            animator.SetTrigger("hit1");
        }
    }
}
