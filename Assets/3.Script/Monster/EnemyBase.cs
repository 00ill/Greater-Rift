using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Enemy Status : MaxHealth, Health, _damage, _detectionRange, _attackRange, _attackCooldown, _attackCooldownRemain
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class EnemyBase : MonoBehaviour
{
    protected enum States
    {
        Idle,
        Move,
        Attack
    }

    protected States _currentState;

    protected NavMeshAgent _agent;
    protected Animator _animator;
    protected AnimatorControllerParameter[] _animatorParameterArr;
    public float MaxHealth { get; private set; } = 200f;
    private float _health = 100f;
    public float Health
    {
        get => _health;
        set { _health = Mathf.Clamp(value, 0, MaxHealth); }
    }
    public float MoveSpeed { get; set; } = 15f;
    protected float _damage;
    protected float _detectionRange;
    protected float _attackRange;
    protected float _attackCooldown;
    protected float _attackCooldownReamain;
    private Vector3 _rayOffset = new Vector3(0, 0.5f, 0);
    protected Transform _player;
    protected RaycastHit _hit;

    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void Update()
    {
        CheckAllCooldown();
    }
    protected virtual void Init()
    {
        TryGetComponent(out _agent);
        TryGetComponent(out _animator);
        _animatorParameterArr = _animator.parameters;
        _player = FindObjectOfType<PlayerControl>().transform;
        _currentState = States.Idle;
    }

    protected bool DetectPlayer()
    {
        if (_agent.remainingDistance <= _detectionRange)
        {
            if (Physics.Raycast(transform.position + _rayOffset, _player.position + _rayOffset, out _hit, _detectionRange))
            {
                return true;
            }
        }
        return false;
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
        CheckCooldown(ref _attackCooldownReamain);
    }
}
