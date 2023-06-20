using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Battle
    }
    public State _currentState { get; private set; } = State.Idle;
    private NavMeshAgent _enemyAgent;
    private float _playerDetectionRange;
    private Transform _target;

    private void Awake()
    {
        TryGetComponent(out _enemyAgent);
    }

    public bool DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, _target.position);
        if(distance < _playerDetectionRange)
        {
            return true;
        }
        return false;
    }

    public void SetDestination(Vector3 destinationPosiotion)
    {
        _enemyAgent.SetDestination(destinationPosiotion);
    }

    public void Stop()
    {
        _enemyAgent.isStopped = true;
    }
}
