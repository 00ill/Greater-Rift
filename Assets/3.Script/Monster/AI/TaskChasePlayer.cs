using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class TaskChasePlayer : Node
{
    private Animator m_Animator;
    private NavMeshAgent _enemyAgent;

    public TaskChasePlayer(Transform transform)
    {
        transform.TryGetComponent(out _enemyAgent);
        transform.TryGetComponent(out m_Animator);
    }

    public override NodeState Evaluate()
    {
        m_Animator.SetFloat("Locomotion", 1f);
        Transform target = (Transform)GetData("target");
        if(_enemyAgent.enabled)
        {
            _enemyAgent.avoidancePriority = 50;
            _enemyAgent.SetDestination(target.position);
            _enemyAgent.isStopped = false;
        }
        state = NodeState.Running;
        return state;
    }
}
