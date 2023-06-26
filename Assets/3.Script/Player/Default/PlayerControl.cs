using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
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

    private void Start()
    {
        _playerAgent.speed = _playerStatus.GetStats(Statistic.MoveSpeed).FloatValue;
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
        SetDestination(command.worldPoint);
    }
}
