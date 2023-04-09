using System.Collections.Generic;
using UnityEngine;

namespace UniBT
{
    public abstract class Composite : NodeBehavior  // 하나 이상의 하위 노드가 있으며 업데이트할 하위 노드를 제어
    {
        [SerializeReference]
        private List<NodeBehavior> children = new List<NodeBehavior>();
        
        public List<NodeBehavior> Children => children;

        protected sealed override void OnRun()
        {
            children.ForEach( e => e.Run(gameObject));
        }
        
        public sealed override void Awake()
        {
            OnAwake();
            children.ForEach( e => e.Awake());
        }

        protected virtual void OnAwake()
        {
        }

        public sealed override void Start()
        {
            OnStart();
            children.ForEach(c => c.Start());
        }
        
        protected virtual void OnStart()
        {
        }

        public sealed override void PreUpdate()
        {
            children.ForEach(c => c.PreUpdate());
        }
        
        public sealed override void PostUpdate()
        {
            children.ForEach(c => c.PostUpdate());
        }

#if UNITY_EDITOR
        public void AddChild(NodeBehavior child)
        {
            children.Add(child);
        }
#endif
        
    }
}