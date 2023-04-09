using UnityEngine;

namespace UniBT
{
    public class Sequence : Composite  
    {
        [SerializeField]
        private bool abortOnConditionChanged = true;  // 실행 중인 노드보다 우선 순위가 높은 노드가 실행 불가능해지면 실행 중인 노드를 중단합니다.

        private NodeBehavior runningNode;

        public override bool CanUpdate()
        {
            foreach (var child in Children)  // 이 노드는 모든 자식이 업데이트할 수 있을 때 업데이트할 수 있다.
            {
                if (!child.CanUpdate())
                {
                    return false;
                }
            }
            return true;
        }

        protected override Status OnUpdate()
        {
            if (runningNode != null)  // 이전 상태가 실행 중인 경우 실행 중인 노드를 업데이트
            {
                if (abortOnConditionChanged && IsConditionChanged(runningNode))  // 우선 순위보다 높은 노드가 실행 불가능하면
                {
                    runningNode.Abort();
                    return UpdateWhileSuccess(0);
                }

                var currentOrder = Children.IndexOf(runningNode);
                var status = runningNode.Update();
                if (status == Status.Success)  // 현재 실행중인 노드가 끝나면 다음노드 
                {
                    // update next nodes
                    return UpdateWhileSuccess(currentOrder + 1);
                }

                return HandleStatus(status, runningNode);
            }

            return UpdateWhileSuccess(0);

        }

        private bool IsConditionChanged(NodeBehavior runningChild)  // 자신보다 우선 순위가 높은 노드의 조건이 업데이트할 수 없는 경우
        {
            var priority = Children.IndexOf(runningChild);
            for (var i = 0; i < priority; i++)
            {
                var candidate = Children[i];
                if (!candidate.CanUpdate()) // 우선순위 높은 노드가 업데이가 안되면 // NodeBehavior클래스의 b변수 canUpdate
                {
                    return true;
                }
            }

            return false;
        }

        private Status UpdateWhileSuccess(int start)  // 현재 실행중인 노드가 완료되는 실행되는 함수
        {
            for (var i = start; i < Children.Count; i++)
            {
                var target = Children[i];
                var childStatus = target.Update();
                if (childStatus == Status.Success)
                {
                    continue;
                }
                return HandleStatus(childStatus, target);  // 다음 노드 실행
            }

            return HandleStatus(Status.Success, null);  
        }

        private Status HandleStatus(Status status, NodeBehavior updated)
        {
            runningNode = status == Status.Running ? updated : null;
            return status;
        }

        public override void Abort()
        {
            if (runningNode != null)
            {
                runningNode.Abort();
                runningNode = null;
            }
        }
    }
}