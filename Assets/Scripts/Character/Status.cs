using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Status : MonoBehaviour
    {
        [SerializeField]
        protected int level;
        [SerializeField]
        protected int hp;
        [SerializeField]
        protected int maxHp;
        [SerializeField]
        protected int attackDamage;
        [SerializeField]
        protected int defense;
        [SerializeField]
        protected float moveSpeed;
        [SerializeField]
        protected float rate;

    public int Level { get { return level; } set { level = value; } }
        public int Hp { get { return hp; } set { hp = value; } }
        public int MaxHp { get { return maxHp; } set { maxHp = value; } }
        public int AttackDamage { get { return attackDamage; } set { attackDamage = value; } }
        public int Defense { get { return defense; } set { defense = value; } }
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
        public float Rate { get { return rate; } set { rate = value; } } 

        private void Start()
        {
            level = 1;
            hp = 100;
            maxHp = 100;
            attackDamage = 5;
            defense = 5;
            moveSpeed = 4.0f;
        }
    }
