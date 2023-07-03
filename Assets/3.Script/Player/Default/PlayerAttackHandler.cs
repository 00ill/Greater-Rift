using UnityEngine;
using UnityEngine.AI;

public class PlayerAttackHandler : MonoBehaviour, ICommandHandle
{
    //컴포넌트
    private Animator _playerAnimator;
    private PlayerControl _playerControl;
    private PlayerStatus _playerStatus;
    private PlayerControlInput _playerControlInput;
    private NavMeshAgent _playerAgent;

    //공격관련 변수
    [SerializeField] private float _normalAttackRange;

    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerControl);
        TryGetComponent(out _playerStatus);
        TryGetComponent(out _playerControlInput);
        TryGetComponent(out _playerAgent);
    }
    private void LookAtTarget()
    {
        _playerAgent.updateRotation = false;
        Quaternion lookDirection = Quaternion.LookRotation(_playerControlInput.Hit.point - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 1000);
        _playerAgent.updateRotation = true;
    }

    public void ProcessCommand(Command command)
    {
        float distance = Vector3.Distance(transform.position, command.target.transform.position);

        if (distance < _normalAttackRange)
        {
            if (!_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack"))
            {
                LookAtTarget();
                _playerAnimator.SetTrigger("NormalAttack");
                //DealDamage(command);
                command.isComplete = true;
            }
            else
            {
                command.isComplete = true;
                return;
            }
        }
        else
        {
            _playerControl.SetDestination(command.target.transform.position);
        }
    }

    //이거 애니메이션 이벤트에 넣던가
    //이펙트에 집어 넣던가0000000
    private void DealDamage(Command command)
    {
        IDamageable target = command.target.GetComponent<IDamageable>();
        int damage = _playerStatus.GetStats(Statistic.Damage).IntetgerValue;
        target.TakeDamage(damage);
    }

    private void ShadowSlash()
    {
        GameObject go = Managers.Resource.Instantiate("ShadowSlash");
        Quaternion goDirection = Quaternion.LookRotation(transform.position - go.transform.position);
        go.transform.SetPositionAndRotation(transform.position + transform.forward + transform.up * 0.5f, transform.rotation);
        //go.transform.position = transform.position + transform.forward;
        //go.transform.rotation = goDirection;

    }
}