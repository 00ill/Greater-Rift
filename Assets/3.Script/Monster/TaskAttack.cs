using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Enemy;
using UnityEditorInternal;

public class TaskAttack : BehaviorTree.Node
{
    private Animator _enemyAnimator;
    private EnemyStatus _enemyStatus;
    private PlayerStatus _playerStatus;
    private float _attackCooldown;
    private float _attackCooldownRemain = 0f;

    public TaskAttack(Transform transform)
    {
        transform.TryGetComponent(out  _enemyAnimator);
        if (transform.TryGetComponent(out _enemyStatus))
        {
            _attackCooldown = _enemyStatus.GetStats(Enemy.Statistic.AttackCooldown).IntegerValue;
        }

    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        target.TryGetComponent(out _playerStatus);
        if (_attackCooldownRemain <= 0f)
        {
            _attackCooldownRemain = _attackCooldown;
            _enemyAnimator.SetTrigger("Attack1");
            _playerStatus.TakeDamage(_enemyStatus.GetStats(Enemy.Statistic.Damage).IntegerValue);
        }
        else
        {
            _attackCooldownRemain -=Time.deltaTime;
        }
        state = NodeState.Running;
        return state;
    }
}
