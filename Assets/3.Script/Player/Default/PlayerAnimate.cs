using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityStandardAssets.Cameras;

public class PlayerAnimate : MonoBehaviour
{
    private NavMeshAgent _playerAgent;
    private Animator _playerAnimator;
    private PlayerStatus _playerStatus;
    private PlayerControlInput _playerControlInput;
    private void Awake()
    {
        TryGetComponent(out _playerAgent);
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerStatus);
        TryGetComponent(out _playerControlInput);
    }
    private void Update()
    {
        _playerAnimator.SetFloat("Run", _playerAgent.velocity.magnitude);
        CheckAllCooldown();
    }

    private void LookAtTarget()
    {
        _playerAgent.updateRotation = false;
        Quaternion lookDirection = Quaternion.LookRotation(_playerControlInput.Hit.point - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 1000);
        _playerAgent.updateRotation = true;
    }

    public void AbilityRMB(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (Managers.Skill.M2SkillCooldownRemain > 0f)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.SkillInCooldown, this);
                return;
            }
            if (_playerStatus.ManaPool.CurrentValue < Managers.Skill.GetSkillData(Managers.Skill.CurrentM2SKillName).ManaCost)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.NotEnoughMana, this);
                return;
            }
            LookAtTarget();
            Managers.Skill.StartM2Cooldown();
            _playerStatus.ManaPool.CurrentValue -= Managers.Skill.GetSkillData(Managers.Skill.CurrentM2SKillName).ManaCost;
            _playerAnimator.Play(Managers.Skill.GetSkillData(Managers.Skill.CurrentM2SKillName).ResourceName);
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerManaChange, this);

        }
    }
    public void AbilityNum1(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (Managers.Skill.Num1SkillCooldownRemain <= 0)
            {
                Managers.Skill.StartNum1Cooldown();
                _playerAnimator.Play(Managers.Skill.GetSkillData(Managers.Skill.CurrentNum1SKillName).ResourceName);
            }
        }
    }
    public void AbilityNum2(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (Managers.Skill.Num2SkillCooldownRemain <= 0)
            {
                Managers.Skill.StartNum2Cooldown();
                _playerAnimator.Play(Managers.Skill.GetSkillData(Managers.Skill.CurrentNum2SKillName).ResourceName);
            }
        }
    }
    public void AbilityNum3(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (Managers.Skill.Num3SkillCooldownRemain <= 0)
            {
                Managers.Skill.StartNum3Cooldown();
                _playerAnimator.Play(Managers.Skill.GetSkillData(Managers.Skill.CurrentNum3SKillName).ResourceName);
            }
        }
    }
    public void AbilityNum4(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (Managers.Skill.Num4SkillCooldownRemain <= 0)
            {
                Managers.Skill.StartNum4Cooldown();
                _playerAnimator.Play(Managers.Skill.GetSkillData(Managers.Skill.CurrentNum4SKillName).ResourceName);
            }
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
        CheckCooldown(ref Managers.Skill.M1SkillCooldownRemain);
        CheckCooldown(ref Managers.Skill.M2SkillCooldownRemain);
        CheckCooldown(ref Managers.Skill.Num1SkillCooldownRemain);
        CheckCooldown(ref Managers.Skill.Num2SkillCooldownRemain);
        CheckCooldown(ref Managers.Skill.Num3SkillCooldownRemain);
        CheckCooldown(ref Managers.Skill.Num4SkillCooldownRemain);

    }

    private void BladeSlash()
    {
        GameObject go = Managers.Resource.Instantiate("Skill_BladeSlash");
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(new Vector3(180, transform.rotation.y, 0f));

    }

    private void DarkFlare()
    {
        GameObject go = Managers.Resource.Instantiate("DarkFlare");
        go.transform.position = transform.position + Vector3.up * 0.5f;
        go.transform.rotation = transform.rotation;
        go.GetComponent<Projectile>().ShootForward();
    }
}
