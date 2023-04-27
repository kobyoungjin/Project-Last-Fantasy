using UnityEngine;

namespace UniBT.Examples.Scripts.Behavior
{
    public class AnimAction : Action
    {
        [SerializeField]
        private bool acting;

        [SerializeReference]
        private static string animName;
        

        private static readonly int anim = Animator.StringToHash(animName);
        private Animator animator;

        public override void Awake()
        {
            animator = gameObject.GetComponent<Animator>(); 
        }

        protected override Status OnUpdate()
        {
            SetAnim(acting);

            return Status.Running;
        }

        private void SetAnim(bool isAct)
        {
            if (animator != null)
            {
                animator.SetBool(anim, isAct);
            }
        }
    }
}