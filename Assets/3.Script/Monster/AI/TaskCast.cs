using BehaviorTree;
using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Cameras;

public class TaskCast : BehaviorTree.Node
{
    private Animator _enemyAnimator;
    private EnemyStatus _enemyStatus;
    private NavMeshAgent _enemyAgent;
    private PlayerStatus _playerStatus;
    private float _attackCooldown;
    private float _attackCooldownRemain = 0f;

    public TaskCast(Transform transform)
    {
        transform.TryGetComponent(out _enemyAnimator);
        if (transform.TryGetComponent(out _enemyStatus))
        {
            _attackCooldown = _enemyStatus.GetStats(Enemy.Statistic.AttackCooldown).IntegerValue;
        }
        transform.TryGetComponent(out _enemyAgent);
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        target.TryGetComponent(out _playerStatus);
        if(!_enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Cast"))
        {
            if (_attackCooldownRemain <= 0f)
            {
                _attackCooldownRemain = _attackCooldown;
                LookAtTarget();
                _enemyAnimator.SetTrigger("Cast");
            }
            else
            {
                _attackCooldownRemain -= Time.deltaTime;
            }
        }
        state = NodeState.Running;
        return state;
    }
    private void LookAtTarget()
    {
        _enemyAgent.ResetPath();
        _enemyAgent.updateRotation = false;
        Quaternion lookDirection = Quaternion.LookRotation(_playerStatus.transform.position - _enemyAgent.transform.position);
        _enemyAgent.transform.rotation = Quaternion.Slerp(_enemyAgent.transform.rotation, lookDirection, Time.deltaTime * 10000);
        _enemyAgent.updateRotation = true;
    }

}
