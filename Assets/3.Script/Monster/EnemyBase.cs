using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Enemy Status : MaxHealth, Health, _damage, _detectionRange, _attackRange, _attackCooldown,
/// _attackCooldownRemain, MoveSpeed
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class EnemyBase : MonoBehaviour
{
    //Enemy 상태
    protected enum States
    {
        Idle,
        Move,
        Attack
    }
    protected States _currentState;

    //필요 컴포넌트
    protected NavMeshAgent _agent;
    protected Animator _animator;
    protected AnimatorControllerParameter[] _animatorParameterArr;
    [SerializeField] protected EnemyData _enemyData;

    //EnemyData 변수
    protected string _name;
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
    
    //Enemy 기본 동작을 위한 변수
    private Vector3 _rayOffset = new(0, 0.5f, 0);
    protected Transform _player;
    protected RaycastHit _hit;
    private readonly float _extraRotateSpeed = 4.0f;

    protected virtual void Awake()
    {
        Init();
    }
    protected virtual void Start()
    {
        //_agent.updateRotation = false;
    }
    protected virtual void Update()
    {
        //LookPlayer();
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

    protected void SetEnemyData()
    {
        _name = _enemyData.Name;
        MaxHealth= _enemyData.MaxHealth;
        Health = _enemyData.Health;
        _damage = _enemyData.Damage;
        _detectionRange = _enemyData.DetectionRange;
        _attackRange = _enemyData.AttackRange;
        _attackCooldown = _enemyData.AttackCooldown;
        MoveSpeed = _enemyData.MoveSpeed;

    }

    protected bool DetectPlayer()
    {
        if (_agent.remainingDistance <= _detectionRange)
        {
            if (Physics.Raycast(transform.position + _rayOffset, _player.position + _rayOffset, out _hit, Mathf.Infinity))
            {
                return true;
            }
        }
        return false;
    }
    protected void LookPlayer()
    {
        if (_agent.desiredVelocity.sqrMagnitude >= 0.1f * 0.1f)
        {
            Vector3 moveDirection = _agent.desiredVelocity;
            Quaternion targetAngle = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * _extraRotateSpeed);
        }
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
