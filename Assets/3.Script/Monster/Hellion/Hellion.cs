using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;
using Enemy;
using System.Collections.Generic;
using Unity.VisualScripting;

[RequireComponent(typeof(EnemyStatus))]
[RequireComponent(typeof(NavMeshAgent))]
public class Hellion : BehaviorTree.Tree
{
    [HideInInspector] public EnemyStatus EnemyStatus;
    [HideInInspector] public NavMeshAgent EnemyAgent;
    private void Awake()
    {
        TryGetComponent(out EnemyStatus);
        TryGetComponent(out EnemyAgent);
    }
    protected override void Start()
    {
        base.Start();
        Init();
    }
    private void Init()
    {
        EnemyAgent.speed = EnemyStatus.GetStats(Enemy.Statistic.MoveSpeed).value;
        EnemyAgent.acceleration = 1000f;
        EnemyAgent.angularSpeed = 1000f;
    }
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new BehaviorTree.Sequence(new List<Node>
            {
                new CheckPlayerInFOVRange(transform),
                new TaskChasePlayer(transform)
            })
        });
        return root;
    }
}