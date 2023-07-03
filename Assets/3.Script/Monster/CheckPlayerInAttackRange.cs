using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;
using Enemy;

public class CheckPlayerInAttackRange : BehaviorTree.Node
{
    private Transform _enemyTransform;
    private Animator _enemyAnimator;
    private EnemyStatus _enemyStatus;
    private NavMeshAgent _enemyAgent;
    public CheckPlayerInAttackRange(Transform transform)
    {
        _enemyTransform = transform;
        _enemyTransform.TryGetComponent(out  _enemyAnimator);
        _enemyTransform.TryGetComponent(out _enemyStatus);
        _enemyTransform.TryGetComponent(out _enemyAgent);
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if(t == null) 
        {
            state = NodeState.Failure;
            return state;
        }

        Transform target = (Transform)t;
        if(Vector3.Distance(_enemyTransform.position, target.position) <= _enemyStatus.GetStats(Enemy.Statistic.AttackRange).IntegerValue)
        {
            _enemyAgent.avoidancePriority = 49;
            _enemyAgent.isStopped = true;
            _enemyAnimator.SetFloat("Locomotion", 0f);
            state = NodeState.Success;
            return state;
        }
        state = NodeState.Failure;
        return state;
    }
}
