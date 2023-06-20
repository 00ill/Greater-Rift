using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttackHandler : MonoBehaviour
{
    //컴포넌트
    private Animator _playerAnimator;
    private PlayerControl _playerControl;
    private PlayerInput _playerInput;
    private PlayerStatus _playerStatus;
    //공격관련 변수
    [SerializeField] private float NormalAttackRange;
    private InteractableObject _target;
    private float _normalAttackCooldown = 1.25f;
    private float _normalAttackCooldownRemain;

    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerControl);
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerStatus);

    }

    private void Update()
    {
        CheckNormalAttack();
        CheckAllCooldown();
    }


    private void CheckNormalAttack()
    {
        if (_playerControl.TargetObject != null)
        {
            NormalAttack(_playerControl.TargetObject);
        }
        if (_target != null)
        {
            ProcessNormalAttack();
        }
    }

    internal void NormalAttack(InteractableObject target)
    {
        _target = target;
        ProcessNormalAttack();
    }

    private void ProcessNormalAttack()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        if (distance < NormalAttackRange)
        {
            if(_normalAttackCooldownRemain > 0f)
            {
                return;
            }
            _normalAttackCooldownRemain = GetAttackTime(_normalAttackCooldown);
            _playerControl.Stop();
            _playerAnimator.SetTrigger("NormalAttack");

            if (_target.TryGetComponent(out EnemyStatus enemyStatus))
            {
                enemyStatus.TakeDamage(_playerStatus.GetStats(Statistic.Damage).IntetgerValue);
            }
            _target = null;
        }
        else
        {
            _playerControl.SetDestination(_target.transform.position);
        }
    }

    private float GetAttackTime(float cooldown)
    {
        float attackTime = _normalAttackCooldown;
        attackTime *= _playerStatus.GetStats(Statistic.AttackSpeed).FloatValue;
        return attackTime;
    }
    private void CheckCooldown(ref float skillCooldownRemain)
    {
        if (skillCooldownRemain > 0)
        {
            skillCooldownRemain -= Time.deltaTime;
        }
        else
        {
            skillCooldownRemain = 0f;
        }
    }

    private void CheckAllCooldown()
    {
        CheckCooldown(ref _normalAttackCooldownRemain);
       
    }
}
