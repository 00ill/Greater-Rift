using UnityEngine;
using UnityEngine.AI;

public class PlayerAttackHandler : MonoBehaviour, ICommandHandle
{
    //������Ʈ
    private Animator _playerAnimator;
    private PlayerControl _playerControl;
    private PlayerStatus _playerStatus;
    private PlayerControlInput _playerControlInput;
    private NavMeshAgent _playerAgent;

    //���ݰ��� ����
    [SerializeField] private float _normalAttackRange;

    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerControl);
        TryGetComponent(out _playerStatus);
        TryGetComponent(out _playerControlInput);
        TryGetComponent(out _playerAgent);
    }

    private void Update()
    {
        CheckCooldown(ref Managers.Skill.M1SkillCooldownRemain);
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
            if (Managers.Skill.M1SkillCooldownRemain <= 0)
            {
                Managers.Skill.StartM1Cooldown();
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

    //�̰� �ִϸ��̼� �̺�Ʈ�� �ִ���
    //����Ʈ�� ���� �ִ���0000000
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
}