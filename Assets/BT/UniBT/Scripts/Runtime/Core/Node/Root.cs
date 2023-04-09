using UnityEngine;

namespace UniBT
{
    public class Root : NodeBehavior  // 최상위 노드 클래스
    {
        [SerializeReference]
        private NodeBehavior child;

#if UNITY_EDITOR
        [HideInEditorWindow]
        public System.Action UpdateEditor;
#endif
        public NodeBehavior Child
        {
            get => child;
#if UNITY_EDITOR
            set => child = value;
#endif
        }

        protected sealed override void OnRun()
        {
            child.Run(gameObject);
        }

        public override void Awake()
        {
            child.Awake();
        }

        public override void Start()
        {
           child.Start();
        }

        public override void PreUpdate()  // 업데이트 이전
        {
            child.PreUpdate();
        }

        protected sealed override Status OnUpdate()  // 업데이트 중
        {
#if UNITY_EDITOR
            UpdateEditor?.Invoke();
#endif
            return child.Update();
        }
        
        
        public override void PostUpdate()  // 업데이트 이후
        {
            child.PostUpdate();
        }

        public override void Abort()  // 중단
        {
            child.Abort();
        }

    }
}