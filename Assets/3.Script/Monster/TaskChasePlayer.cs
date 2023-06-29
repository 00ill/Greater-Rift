using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;
using System;
using System.Runtime.CompilerServices;
using Enemy;

public class TaskChasePlayer : Node
{
    private NavMeshAgent _enemyAgent;
    private EnemyStatus _enemyStatus;
    
    public TaskChasePlayer(Transform transform)
    {
        transform.TryGetComponent(out _enemyAgent);
        transform.TryGetComponent(out _enemyStatus);
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        _enemyAgent.avoidancePriority = 50;
        _enemyAgent.SetDestination(target.position);
        _enemyAgent.isStopped = false;
        state = NodeState.Running;
        Debug.Log("ÂÑ¾Æ°¡´Â Áß");
        return state;
    }
}
