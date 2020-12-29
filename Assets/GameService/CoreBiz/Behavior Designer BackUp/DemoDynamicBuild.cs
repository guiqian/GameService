using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class DemoDynamicBuild : MonoBehaviour {

    void Start() {
        BehaviorTree behaviorTree = gameObject.AddComponent<BehaviorTree>();
        behaviorTree.StartWhenEnabled = false;

        var entryTask = new EntryTask();
#if UNITY_EDITOR
        entryTask.NodeData = new NodeData();
#endif
        behaviorTree.GetBehaviorSource().EntryTask = entryTask;

        var rootTask = new Sequence();
#if UNITY_EDITOR
        rootTask.NodeData = new NodeData();
#endif
        behaviorTree.GetBehaviorSource().RootTask = rootTask;

        Log log = new Log();
#if UNITY_EDITOR
        log.NodeData = new NodeData();
#endif
        log.logError = new SharedBool();
        rootTask.AddChild(log, 0);
        log.text = "try me!...";

        behaviorTree.GetBehaviorSource().HasSerialized = true;
        behaviorTree.EnableBehavior();
    }

}
