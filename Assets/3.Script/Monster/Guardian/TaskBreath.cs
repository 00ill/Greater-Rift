using BehaviorTree;
using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskBreath : BehaviorTree.Node
{
    private Animator _enemyAnimator;
    private EnemyStatus _enemyStatus;
    private NavMeshAgent _enemyAgent;
    private PlayerStatus _playerStatus;
    private WaitForSeconds _breathTime = new WaitForSeconds(7.5f);
    public TaskBreath(Transform transform)
    {
        transform.TryGetComponent(out _enemyAnimator);
        transform.TryGetComponent(out _enemyStatus);
        transform.TryGetComponent(out _enemyAgent);
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        target.TryGetComponent(out _playerStatus);
        if (!_enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Breath"))
        {
            LookAtTarget();
            _enemyAnimator.SetTrigger("Breath");
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
