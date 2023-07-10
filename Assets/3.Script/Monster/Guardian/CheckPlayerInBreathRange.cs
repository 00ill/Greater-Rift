using BehaviorTree;
using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckPlayerInBreathRange : BehaviorTree.Node
{
    private Transform _enemyTransform;
    private Animator _enemyAnimator;
    private EnemyStatus _enemyStatus;
    private NavMeshAgent _enemyAgent;
    private int _breathRange = 15;
    private float _breathCooldown = 10f;
    private float _breathCooldownRemain = 0f;

    public CheckPlayerInBreathRange(Transform transform)
    {
        _enemyTransform = transform;
        _enemyTransform.TryGetComponent(out _enemyAnimator);
        _enemyTransform.TryGetComponent(out _enemyStatus);
        _enemyTransform.TryGetComponent(out _enemyAgent);
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.Failure;
            return state;
        }

        if(_enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Breath"))
        {
            return NodeState.Success;
        }

        if(_breathCooldownRemain > 0f )
        {
            _breathCooldownRemain -= Time.deltaTime;
            state = NodeState.Failure;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(_enemyTransform.position, target.position) <= _breathRange)
        {
            _breathCooldownRemain = _breathCooldown;
            if(_enemyAgent.enabled)
            {
                _enemyAgent.avoidancePriority = 49;
                _enemyAgent.isStopped = true;
            }
            _enemyAnimator.SetFloat("Locomotion", 0f);
            state = NodeState.Success;
            return state;
        }
        state = NodeState.Failure;
        return state;
    }
}
