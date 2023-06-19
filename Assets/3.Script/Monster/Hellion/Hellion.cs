using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Hellion : EnemyBase
{
    enum AnimationParameters
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
        yield return null;
        if(DetectPlayer())
        {
            _currentState = States.Move;
        }
    }
    protected IEnumerator Move()
    {
        yield return null;
        _agent.SetDestination(_player.position);
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
        PlayAnimation(AnimationParameters.Attack1);
    }

    private void PlayAnimation(AnimationParameters parameter)
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