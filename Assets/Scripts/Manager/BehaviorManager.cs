using UnityEngine;
using BehaviorDesigner.Runtime;

public class BehaviorManager : MonoBehaviour
{
    public ExternalBehaviorTree behaviorTree;

    void Start()
    {
        var bt = gameObject.AddComponent<BehaviorTree>();
        bt.StartWhenEnabled = false;
        bt.ExternalBehavior = behaviorTree;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
