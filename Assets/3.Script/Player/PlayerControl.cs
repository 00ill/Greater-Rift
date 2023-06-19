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

    //���콺 �̵�, ������ ���� ����
    private Ray _ray;
    private RaycastHit _hit;
    private readonly float _extraRotateSpeed = 8.0f;

    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerAgent);
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerStatus);
        
    }

    private void Start()
    {
        _playerAgent.updateRotation = false;
        _playerAgent.speed = _playerStatus.MoveSpeed;
    }

    private void Update()
    {
        Move();
        CheckNormalAttack();
    }
    private void Move()
    {
        if (_playerInput.Mouse2)
        {
            _ray = Camera.main.ScreenPointToRay(_playerInput.MousePosition);
            if (Physics.Raycast(_ray, out _hit))
            {
                _playerAgent.SetDestination(_hit.point);
            }
        }

        if (_playerAgent.velocity.sqrMagnitude <= 1 && _playerAgent.remainingDistance <= 0.1f)
        {
            _playerAnimator.SetBool("IsRun", false);
        }
        else if (_playerAgent.desiredVelocity.sqrMagnitude >= 0.1f * 0.1f)
        {
            Vector3 moveDirection = _playerAgent.desiredVelocity;
            Quaternion targetAngle = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * _extraRotateSpeed);
            _playerAnimator.SetBool("IsRun", true);
        }
    }

    private void CheckNormalAttack()
    {
        //������ �������� ������ �� �Ǻ��� �ؾ߰����� �ϴ� �̹Է� ����Ʈ�� ����
        if(_playerInput.Shift &&_playerInput.Mouse1 && !_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack"))
        {
            _playerAnimator.SetTrigger("NormalAttack");
        }
    }
}
