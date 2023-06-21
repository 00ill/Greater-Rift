using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class PlayerAttackHandler : MonoBehaviour, ICommandHandle
{
    //컴포넌트
    private Animator _playerAnimator;
    private PlayerControl _playerControl;
    private PlayerStatus _playerStatus;
    //공격관련 변수
    [SerializeField] private float _normalAttackRange;
    private InteractableObject _target;
    private float _normalAttackCooldown = 1.25f;
    private float _normalAttackCooldownRemain;

    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerControl);
        TryGetComponent(out _playerStatus);

    }

    private void Update()
    {
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
        if (distance < _normalAttackRange)
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

    public void ProcessCommand(Command command)
    {
        float distance = Vector3.Distance(transform.position, command.target.transform.position);

        if (distance < _normalAttackRange)
        {
            if (_normalAttackCooldownRemain > 0f) { return; }

            _normalAttackCooldownRemain = _normalAttackCooldown;

            _playerAnimator.SetTrigger("NormalAttack");
            DealDamage(command);
            command.isComplete = true;
        }
        else
        {
            _playerControl.SetDestination(command.target.transform.position);
        }
    }

    private void DealDamage(Command command)
    {
        IDamageable target = command.target.GetComponent<IDamageable>();
        int damage = _playerStatus.GetStats(Statistic.Damage).IntetgerValue;
        target.TakeDamage(damage);
    }
}
