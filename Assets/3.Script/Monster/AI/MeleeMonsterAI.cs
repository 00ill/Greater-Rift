using BehaviorTree;
using Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStatus))]
[RequireComponent(typeof(NavMeshAgent))]
public class MeleeMonsterAI : BehaviorTree.Tree
{
    [HideInInspector] public EnemyStatus EnemyStatus;
    [HideInInspector] public NavMeshAgent EnemyAgent;
    [HideInInspector] public Animator EnemyAnimator;

    private PlayerStatus _playerStatus;

    private void Awake()
    {
        TryGetComponent(out EnemyStatus);
        TryGetComponent(out EnemyAgent);
        TryGetComponent(out EnemyAnimator);
        _playerStatus = FindObjectOfType<PlayerStatus>();
    }
    protected override void Start()
    {
        base.Start();
        Init();
    }
    private void Init()
    {
        EnemyAgent.speed = EnemyStatus.GetStats(Enemy.Statistic.MoveSpeed).IntegerValue;
        EnemyAgent.acceleration = 1000f;
        EnemyAgent.angularSpeed = 1000f;
        EnemyStatus.OnDeath -= OnDeath;
        EnemyStatus.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        EnemyStatus.IsDead = true;
        EnemyAnimator.SetTrigger("Death");
        EnemyAgent.enabled = false;
        this.enabled = false;
    }

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        { 
            new BehaviorTree.Sequence(new List<Node>
            {
                new CheckPlayerInAttackRange(transform),
                new TaskAttack(transform)
            }),
            new BehaviorTree.Sequence(new List<Node>
            {
                new CheckPlayerInFOVRange(transform),
                new TaskChasePlayer(transform)
            })
        });
        return root;
    }
    private void AttackAnimationEvent()
    {
        _playerStatus.TakeDamage(EnemyStatus.GetStats(Enemy.Statistic.Damage).IntegerValue);
    }
}