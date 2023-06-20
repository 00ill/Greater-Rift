using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    //컴포넌트
    private Animator _enemyAnimator;
    private EnemyStatus _enemyStatus;
    private EnemyAI _enemyAI;
    //공격관련 변수
    [SerializeField] private float NormalAttackRange;
    [HideInInspector] private PlayerControl _target;
    private float _normalAttackCooldown = 1.25f;
    private float _normalAttackCooldownRemain;

    private void Awake()
    {
        TryGetComponent(out _enemyAnimator);
        TryGetComponent(out _enemyStatus);
        TryGetComponent(out _enemyAI);
    }

    private void Update()
    {
        CheckNormalAttack();
        CheckAllCooldown();
    }


    private void CheckNormalAttack()
    {
        if(_enemyAI.DetectPlayer() )
        {
            NormalAttack(_enemyAI.Target);
        }
        
        if (_target != null)
        {
            ProcessNormalAttack();
        }
        //if (_enemyControl.TargetObject != null)
        //{
        //    NormalAttack(_enemyControl.TargetObject);
        //}
        //if (_target != null)
        //{
        //    ProcessNormalAttack();
        //}
    }

    internal void NormalAttack(PlayerControl target)
    {
        _target = target;
        ProcessNormalAttack();
    }

    private void ProcessNormalAttack()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        if (distance < NormalAttackRange)
        {
            if (_normalAttackCooldownRemain > 0f)
            {
                return;
            }
            _normalAttackCooldownRemain = GetAttackTime(_normalAttackCooldown);
            //_enemyAnimator.SetTrigger("NormalAttack");
            _enemyAnimator.SetBool("Attack2", true);


            if (_target.TryGetComponent(out PlayerStatus playerStatus))
            {
                playerStatus.TakeDamage(_enemyStatus.GetStats(Enemy.Statistic.Damage).value);
                //enemyStatus.TakeDamage(_enemyStatus.GetStats(Statistic.Damage).value);
            }
            _target = null;
        }
        else
        {
            _enemyAI.SetDestination(_enemyAI.Target.transform.position);
            //_enemyAnimator.SetFloat("Locomotion", Convert.ToSingle(_enemyAI.DetectPlayer()));
            _enemyAnimator.SetBool("Attack2", false);
            //_enemyControl.SetDestination(_target.transform.position);
        }
    }

    private float GetAttackTime(float cooldown)
    {
        float attackTime = _normalAttackCooldown;
        //attackTime *= _enemyStatus.GetStats(Statistic.AttackSpeed).FloatValue;
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
