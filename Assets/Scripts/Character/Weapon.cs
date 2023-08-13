using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };  // 근접, 원거리
    public enum characterType { Player, Troll };
    public Type type;
    public int damage;
    public float rate;  // 공격속도
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    string temp;
    private void Start()
    { 
        temp = LayerMask.LayerToName(this.gameObject.layer);
        if (this.gameObject.transform.parent.name == "Troll_model")
            temp = "Troll";
    }
    public void Use()
    {
        if (type == Type.Melee)
        {
            if(temp == "Player")
            {
                damage = GameObject.Find(temp).GetComponent<Player>().AttackDamage;
                rate = GameObject.Find(temp).GetComponent<Player>().Rate;
            }
            else if(temp == "Troll")
            {
                damage = GameObject.Find(temp).GetComponentInChildren<Troll>().AttackDamage;
                rate = GameObject.Find(temp).GetComponentInChildren<Troll>().Rate;
            }
           
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }
    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        if(trailEffect != null)
            trailEffect.enabled = true;

        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.2f);
        if (trailEffect != null)
            trailEffect.enabled = false;
    }
}

