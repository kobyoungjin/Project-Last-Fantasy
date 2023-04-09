using System.Collections.Generic;

namespace UniBT
{
    public class All : Composite
    {

        private List<NodeBehavior> runningNodes;

        protected override void OnAwake()
        {
            runningNodes = new List<NodeBehavior>();
        }

        /// <summary>
        /// Update all nodes.
        /// - any running -> Running
        /// - any failed -> Failure
        /// - else -> Success
        /// </summary>
        protected override Status OnUpdate()
        {
            runningNodes.Clear();
            var anyFailed = false;
            foreach (var c in Children)
            {
                var result = c.Update();    // uniBt 현재 상태 업데이트
                if (result == Status.Running)
                {
                    runningNodes.Add(c);
                }
                else if (result == Status.Failure)
                {
                    anyFailed = true;
                }
            }
            if (runningNodes.Count > 0)  // uniBt의 running상태가 1개이상 있으면
            {
                return Status.Running;
            }

            if (anyFailed)  // uniBt가 Failed 상태이면
            {
                runningNodes.ForEach(e => e.Abort());  
                return Status.Failure;
            }

            return Status.Success;
        }

        public override void Abort()
        {
            runningNodes.ForEach( e => e.Abort() ); // Failed 상태이면 모든 running 중인 상태 정지
            runningNodes.Clear();
        }

    }
}