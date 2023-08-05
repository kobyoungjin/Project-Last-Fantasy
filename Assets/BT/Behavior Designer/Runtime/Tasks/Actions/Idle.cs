

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Returns a TaskStatus of running. Will only stop when interrupted or a conditional abort is triggered.")]
    [TaskIcon("{SkinColor}IdleIcon.png")]
    public class Idle : Action
    {
        float time;
        public override TaskStatus OnUpdate()
        {
            time += UnityEngine.Time.deltaTime;
            if (time > 1f)
            {
                time = 0;
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
    }
}