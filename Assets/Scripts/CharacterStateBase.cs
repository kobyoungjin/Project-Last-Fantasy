using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class CharacterStateBase : StateMachineBehaviour
    {
        private Player player;

        public Player GetPlayer(Animator animator)
        {
            if(player == null)
            {
                player = animator.GetComponent<Player>();
            }

            return player;
        }
    }
}

namespace EnemyState
{
    public class EnemyStateBase : StateMachineBehaviour
    {
        private EnemySkeleton skeleton;

        public EnemySkeleton GetEnemy(Animator animator)
        {
            if (skeleton == null)
            {
                skeleton = animator.GetComponent<EnemySkeleton>();
            }

            return skeleton;
        }
    }
    
}

