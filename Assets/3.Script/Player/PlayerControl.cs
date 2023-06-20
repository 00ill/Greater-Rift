using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerControl : MonoBehaviour
{
    private Animator _playerAnimator;
    private NavMeshAgent _playerAgent;

    private PlayerInput _playerInput;
    private PlayerStatus _playerStatus;
    [HideInInspector] public InteractableObject TargetObject;

    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerAgent);
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerStatus);
    }

    private void Start()
    {
        _playerAgent.speed = _playerStatus.GetStats(Statistic.MoveSpeed).FloatValue;
    }

    private void Update()
    {
        Interact();
        Move();
    }
    private void Move()
    {
        if (_playerInput.Mouse2Down)
        {
            if (!_playerInput.Hit.Equals(null))
            {
                _playerAgent.isStopped = false;
                _playerAgent.SetDestination(_playerInput.Hit.point);
            }
        }
        _playerAnimator.SetFloat("Run", _playerAgent.velocity.magnitude);
    }

    private void Interact()
    {
        if (!_playerInput.Hit.Equals(null))
        {
            if (_playerInput.Hit.transform.TryGetComponent(out InteractableObject interactableObject))
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.CheckInteractableObject, interactableObject);
                if (_playerInput.Mouse1Down)
                {
                    TargetObject = interactableObject;
                    interactableObject.Interact();
                }
                else
                {
                    TargetObject = null;
                }
            }
            else
            {
                TargetObject = null;
                Managers.Event.PostNotification(Define.EVENT_TYPE.CheckInteractableObject, this);
            }
        }
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
}
