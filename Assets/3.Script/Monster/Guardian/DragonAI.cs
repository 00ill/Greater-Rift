using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Guardian))]
public class DragonAI : BehaviorTree.Tree
{
    [HideInInspector] public Guardian Guardian;
    [HideInInspector] public NavMeshAgent EnemyAgent;
    [HideInInspector] public Animator EnemyAnimator;
    private PlayerStatus _playerStatus;
    public bool _isAttacking = false;

    private void Awake()
    {
        TryGetComponent(out Guardian);
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
        EnemyAgent.speed = Guardian.GetStats(Enemy.Statistic.MoveSpeed).IntegerValue;
        EnemyAgent.acceleration = 1000f;
        EnemyAgent.angularSpeed = 1000f;
        Guardian.OnDeath -= OnDeath;
        Guardian.OnDeath += OnDeath;
    }

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new BehaviorTree.Sequence(new List<Node>
            {
                new CheckPlayerInBreathRange(transform),
                new TaskBreath(transform)
            }),
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

    private void BreathAnimationEvent()
    {
        GameObject vfx = Managers.Resource.Instantiate("DragonBreath");
        vfx.transform.SetPositionAndRotation(transform.position + transform.forward * 5f, transform.rotation);
        Managers.Sound.Play("DragonBreath");
    }

    private void AttackAnimationEvent()
    {
        _playerStatus.TakeDamage(Guardian.GetStats(Enemy.Statistic.Damage).IntegerValue);
    }
    private void BreathStop()
    {
        EnemyAgent.enabled = true;
        _isAttacking = false;
    }
    private void BreathStart()
    {
        EnemyAgent.enabled = false;
        _isAttacking = true;
    }

    private void OnDeath()
    {
        Guardian.IsDead = true;
        EnemyAnimator.SetTrigger("Death");
        EnemyAgent.enabled = false;
        this.enabled = false;
    }
}
