using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Hellion : EnemyBase
{
    enum AnimatorParameters
    {
        Locomotion,
        Turning,
        Attack1,
        Attack2,
        Block,
        Death,
        EatStart,
        EatStop,
        GotHitHead,
        GotHitBody,
        IdleBreak,
        Roar
    }
    protected override void Awake()
    {
        _name = "Hellion";
        base.Awake();
        SetEnemyData();

    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(FSM());
        Debug.Log(Enum.GetName(typeof(AnimatorParameters), AnimatorParameters.Locomotion));
    }

    protected virtual IEnumerator FSM()
    {
        yield return null;
        while (true)
        {
            yield return StartCoroutine(_currentState.ToString());
        }
    }

    protected IEnumerator Idle()
    {
        _agent.SetDestination(_player.position);
        _agent.isStopped = true;
        yield return null;
        if(DetectPlayer())
        {
            _currentState = States.Move;
        }
        else
        {
            Debug.Log("플레이어를 못 찾음");
        }
    }
    protected IEnumerator Move()
    {
        _animator.SetFloat(Enum.GetName(typeof(AnimatorParameters), AnimatorParameters.Locomotion), 1f);
        _agent.SetDestination(_player.position);
        _agent.isStopped = false;
        yield return null;
        if(_agent.remainingDistance <= _attackRange)
        {
            _currentState = States.Attack;
        }
        else if(_agent.remainingDistance > _detectionRange)
        {
            _currentState = States.Idle;
        }
    }

    protected IEnumerator Attack()
    {
        yield return null;
        PlayAnimation(AnimatorParameters.Attack1);
    }

    private void PlayAnimation(AnimatorParameters parameter)
    {
        for(int i =0; i<_animatorParameterArr.Length; i++) 
        {
            if(_animatorParameterArr[i].type == AnimatorControllerParameterType.Bool)
            {
                if ((int)parameter != i)
                {
                    _animator.SetBool(_animatorParameterArr[i].name, false);
                }
                else
                {
                    _animator.SetBool(_animatorParameterArr[i].name, true);
                }
            }
        }
    }
}