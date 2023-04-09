using UnityEngine;

namespace UniBT
{
    /// <summary>
    ///  update the children in order.
    ///  update only one child per frame.
    /// </summary>
    public class Rotator : Composite  // 하위 노드를 순서대로 업데이트
    {
        [SerializeField]
        private bool resetOnAbort;

        private int targetIndex;

        private NodeBehavior runningNode;

        protected override Status OnUpdate()
        {
            if (runningNode != null)  // 이전 상태가 실행중인 경우 현재 실행중인 노드 업데이트
            {
                return HandleStatus(runningNode.Update(), runningNode);
            }

            var target = Children[targetIndex];
            return HandleStatus(target.Update(), target);
        }

        private void SetNext()
        {
            targetIndex++;
            if (targetIndex >= Children.Count)
            {
                targetIndex = 0;
            }
        }

        private Status HandleStatus(Status status, NodeBehavior updated)
        {
            if (status == Status.Running)  // 현재 상태가 running 상태면
            {
                runningNode = updated;
            }
            else  // 현재 실행중인 노드 null로 바꾸고 다음 하위노드로 바꾸기
            {
                runningNode = null;
                SetNext();
            }
            return status;
        }

        public override void Abort()
        {
            if (runningNode != null)
            {
                runningNode.Abort();
                runningNode = null;
            }

            if (resetOnAbort)
            {
                targetIndex = 0;
            }
        }
    }
}