using Enemy;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Battle
    }
    public State _currentState { get; private set; } = State.Idle;
    private NavMeshAgent _enemyAgent;
    private EnemyStatus _enemyStatus;
    private Animator _animator;
    [HideInInspector] public PlayerControl Target;

    private void Awake()
    {
        TryGetComponent(out _enemyAgent);
        TryGetComponent(out _enemyStatus);
        TryGetComponent(out _animator);
        Target = FindAnyObjectByType<PlayerControl>();
    }
    private void Start()
    {
        _enemyAgent.speed = _enemyStatus.GetStats(Enemy.Statistic.MoveSpeed).value;
    }

    private void Update()
    {
        _animator.SetFloat("Locomotion", _enemyAgent.velocity.magnitude);
        _animator.SetFloat("Turning", _enemyAgent.desiredVelocity.z);
    }
    public bool DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, Target.transform.position);
        if (distance < _enemyStatus.GetStats(Enemy.Statistic.DetectionRange).value)
        {
            return true;
        }
        return false;
    }

    public void SetDestination(Vector3 destinationPosiotion)
    {
        _enemyAgent.SetDestination(destinationPosiotion);
    }

    public void Stop()
    {
        _enemyAgent.isStopped = true;
    }
}
