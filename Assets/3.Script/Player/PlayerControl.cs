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

    //마우스 이동, 공격을 위한 변수
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
        //_playerAgent.updateRotation = false;
        _playerAgent.speed = _playerStatus.MoveSpeed;
    }

    private void Update()
    {
        Move();
        CheckNormalAttack();
    }
    private void Move()
    {
        if (_playerInput.Mouse2Down)
        {
            _ray = Camera.main.ScreenPointToRay(_playerInput.MousePosition);
            if (Physics.Raycast(_ray, out _hit))
            {
                _playerAgent.SetDestination(_hit.point);
            }
        }
        _playerAnimator.SetFloat("Run", _playerAgent.velocity.magnitude);
        //if (_playerAgent.velocity.magnitude >= 0.1)
        //{
        //    Vector3 moveDirection = _playerAgent.desiredVelocity;
        //    Quaternion targetAngle = Quaternion.LookRotation(moveDirection);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * _extraRotateSpeed);
        //}
        //if (_playerAgent.velocity.sqrMagnitude <= 1 && _playerAgent.remainingDistance <= 0.1f)
        //{
        //    _playerAnimator.SetBool("IsRun", false);
        //    //Debug.Log(string.Format($"멈출 때 : {_playerAgent.velocity.sqrMagnitude}"));
        //}
        //else if (_playerAgent.desiredVelocity.sqrMagnitude >= 0.1f * 0.1f && _playerAgent.remainingDistance > 0.1f)
        //{
        //    Vector3 moveDirection = _playerAgent.desiredVelocity;
        //    Quaternion targetAngle = Quaternion.LookRotation(moveDirection);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * _extraRotateSpeed);
        //    _playerAnimator.SetBool("IsRun", true);
        //    //Debug.Log(string.Format($"움직일 때 : {_playerAgent.velocity.sqrMagnitude}"));
        //}
        //Debug.Log(_playerAgent.remainingDistance);
    }

    private void CheckNormalAttack()
    {
        //원래는 여러가지 정보로 더 판별을 해야겠지만 일단 임시로 시프트만 구현
        if (_playerInput.Shift && _playerInput.Mouse1 && !_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack"))
        {
            _playerAnimator.SetTrigger("NormalAttack");
        }
    }
}
