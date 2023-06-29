using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Enemy;

public class CheckPlayerInFOVRange : Node
{
    private static int _enemyLayerMask = 1 << 6;

    private Transform _transform;
    private Animator _animator;
    private EnemyStatus _enemyStatus;

    public CheckPlayerInFOVRange(Transform transform)
    {
        _transform = transform;
        _transform.TryGetComponent(out _animator);
        _transform.TryGetComponent(out _enemyStatus);
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;

            if (Vector3.Distance(_transform.position, player.position) <= _enemyStatus.GetStats(Enemy.Statistic.FovRange).IntegerValue)
            {
                parent.parent.SetData("target", player);
                _animator.SetFloat("Locomotion", 1f);
                state = NodeState.Success;
                Debug.Log("Å½Áö ¼º°ø Áß");
                return state;
            }

            state = NodeState.Failure;
            Debug.Log("Å½Áö ½ÇÆÐ Áß");
            return state;
        }

        state = NodeState.Success;
        return state;
    }

}
