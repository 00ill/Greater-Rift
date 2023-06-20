using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    //컴포넌트
    private Animator _enemyAnimator;
    //private PlayerControl _enemyControl;
    private EnemyStatus _enemyStatus;
    //공격관련 변수
    [SerializeField] private float NormalAttackRange;
    private InteractableObject _target;
    private float _normalAttackCooldown = 1.25f;
    private float _normalAttackCooldownRemain;

    private void Awake()
    {
        TryGetComponent(out _enemyAnimator);
        //TryGetComponent(out _enemyControl);
        TryGetComponent(out _enemyStatus);

    }

    private void Update()
    {
        CheckNormalAttack();
        CheckAllCooldown();
    }


    private void CheckNormalAttack()
    {
        //if (_enemyControl.TargetObject != null)
        //{
        //    NormalAttack(_enemyControl.TargetObject);
        //}
        //if (_target != null)
        //{
        //    ProcessNormalAttack();
        //}
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
            if (_normalAttackCooldownRemain > 0f)
            {
                return;
            }
            _normalAttackCooldownRemain = GetAttackTime(_normalAttackCooldown);
            _enemyAnimator.SetTrigger("NormalAttack");

            if (_target.TryGetComponent(out EnemyStatus enemyStatus))
            {
                //enemyStatus.TakeDamage(_enemyStatus.GetStats(Statistic.Damage).value);
            }
            _target = null;
        }
        else
        {
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
