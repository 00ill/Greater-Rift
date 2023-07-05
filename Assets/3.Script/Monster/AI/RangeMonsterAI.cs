using BehaviorTree;
using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStatus))]
[RequireComponent(typeof(NavMeshAgent))]
public class RangeMonsterAI : BehaviorTree.Tree
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
        { new BehaviorTree.Sequence(new List<Node>
            {
                new CheckPlayerInAttackRange(transform),
                new TaskCast(transform)
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
        GameObject projectile = Managers.Resource.Instantiate($"{transform.name}_Projectile");
        projectile.transform.position = transform.position + Vector3.up * 0.5f;
        Vector3 shootDirection = _playerStatus.transform.position - transform.position;
        projectile.transform.rotation = Quaternion.LookRotation(shootDirection.normalized, Vector3.up);
        //projectile.transform.SetParent(transform, true);
        projectile.GetComponent<Projectile>().ShootForward();
        //projectile.GetOrAddComponent<Projectile>().ShootForward();
    }
}
