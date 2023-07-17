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

    private void ShadowSlash()
    {
        Managers.Sound.Play("ShadowSlash");
        GameObject go = Managers.Resource.Instantiate("ShadowSlash");
        Quaternion goDirection = Quaternion.LookRotation(transform.position - go.transform.position);
        go.transform.SetPositionAndRotation(transform.position + transform.forward + transform.up * 0.5f, transform.rotation);
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