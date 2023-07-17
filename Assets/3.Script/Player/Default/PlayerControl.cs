using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerControl : MonoBehaviour, ICommandHandle
{
    private NavMeshAgent _playerAgent;
    private PlayerStatus _playerStatus;
    [HideInInspector] public InteractableObject TargetObject;

    private void Awake()
    {
        TryGetComponent(out _playerAgent);
        TryGetComponent(out _playerStatus);
    }

    public void SetDestination(Vector3 destinationPosition)
    {
        _playerAgent.isStopped = false;
        _playerAgent.SetDestination(destinationPosition);
    }

    internal void Stop()
    {
        _playerAgent.isStopped = true;
    }

    public void ProcessCommand(Command command)
    {
        float distance = Vector3.Distance(transform.position, command.worldPoint);

        if (distance <= 0.2f)
        {
            Stop();
            command.isComplete = true;
        }
        else
        {
            SetDestination(command.worldPoint);
        }

    }

    public void UpdateSpeed()
    {
        if (_playerStatus != null)
        {
            _playerAgent.speed = _playerStatus.GetStats(Statistic.MoveSpeed).FloatValue;
        }
        else if (TryGetComponent(out _playerStatus))
        {
            _playerAgent.speed = _playerStatus.GetStats(Statistic.MoveSpeed).FloatValue;
        }

    }
}
