using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Animator animator;
    Troll troll;
    Boss boss;
 
    private void Start()
    {
        animator = transform.GetComponentInParent<Animator>();
        boss = transform.GetComponent<Boss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {
            if (this.gameObject.transform.root.name == "∆Æ∑—")
            {
                troll.Damaged(30);
                animator.SetTrigger("hit1");
            }
            else if (this.gameObject.transform.root.name == "≈∏¿Ã≈∫")
                boss.Damaged(30);
            
        }
    }
}
