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
        _playerAgent.speed = _playerStatus.MoveSpeed;
    }

    private void Update()
    {
        Interact();
        Move();
        CheckNormalAttack();
    }
    private void Move()
    {
        if (_playerInput.Mouse2Down)
        {
            if (!_playerInput.Hit.Equals(null))
            {
                _playerAgent.SetDestination(_playerInput.Hit.point);
            }
        }
        _playerAnimator.SetFloat("Run", _playerAgent.velocity.magnitude);
    }

    private void CheckNormalAttack()
    {
        //원래는 여러가지 정보로 더 판별을 해야겠지만 일단 임시로 시프트만 구현
        if (_playerInput.Shift && _playerInput.Mouse1 && !_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack"))
        {
            _playerAnimator.SetTrigger("NormalAttack");
        }
    }

    private void Interact()
    {
        if(!_playerInput.Hit.Equals(null))
        {
            if(_playerInput.Hit.transform.TryGetComponent(out InteractableObject interactableObject))
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.CheckInteractableObject, interactableObject);
                if(_playerInput.Mouse1Down)
                {
                    interactableObject.Interact();
                }
            }
            else
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.CheckInteractableObject, this);
            }
        }
    }
}
