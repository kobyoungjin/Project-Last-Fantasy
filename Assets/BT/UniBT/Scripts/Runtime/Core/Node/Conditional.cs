using UnityEngine;

namespace UniBT
{
    public abstract class Conditional : NodeBehavior  // 하나의 자식 노드가 있고 자식이 업데이트 가능한지 조건을 확인합니다
    {
        /// <summary>
        /// true: don't re evaluate condition when the previous status is Running.
        /// </summary>
        [SerializeField] 
        private bool dontReEvaluateOnRunning = false; // 이전 상태가 running상태 였는지

        [SerializeReference]
        private NodeBehavior child;

        public NodeBehavior Child
        {
            get => child;
#if UNITY_EDITOR
            set => child = value;
#endif
        }

        private bool? frameScope = null;  // 프레임 범위?

        private bool isRunning = false;

        protected sealed override void OnRun()
        {
            child?.Run(gameObject);
        }
        
        public sealed override void Awake()
        {
            OnAwake();
            child?.Awake();
        }

        protected virtual void OnAwake()
        {
        }

        public sealed override void Start()
        {
            OnStart();
            child?.Start();
        }
        
        protected virtual void OnStart()
        {
        }
        
        protected override Status OnUpdate()
        {
            // no child means leaf node
            if (child == null)
            {
                return CanUpdate() ? Status.Success : Status.Failure;
            }
            if (CanUpdate())  // running 상태를 유지중이면
            {
                var status = child.Update();
                isRunning = status == Status.Running;  // status와 Status.Running 비교후 참/거짓 반환
                return status;
            }
            return Status.Failure;
        }

        public sealed override void PreUpdate()
        {
            frameScope = null;
            child?.PreUpdate();
        }
        
        public sealed override void PostUpdate()
        {
            frameScope = null;
            child?.PostUpdate();
        }
        
        public override bool CanUpdate()
        {
            if (frameScope != null)  // 프레임 범위가 null값이 아니면
            {
                return frameScope.Value;
            }

            frameScope = isRunning && dontReEvaluateOnRunning || IsUpdatable();  // 현재상태와 이전상태가 running이거나 enemy가 attacking 상태일때
            return frameScope.Value;
        }

        public override void Abort()
        {
            if (isRunning)
            {
                isRunning = false;
                child?.Abort();
            }
        }

        protected abstract bool IsUpdatable();
    }
    
}