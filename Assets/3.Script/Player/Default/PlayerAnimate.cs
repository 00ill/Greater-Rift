using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerAnimate : MonoBehaviour, IListener
{
    private NavMeshAgent _playerAgent;
    private Animator _playerAnimator;
    private PlayerStatus _playerStatus;
    private PlayerControlInput _playerControlInput;
    private CommandHandler _commandHandler;
    private void Awake()
    {
        TryGetComponent(out _playerAgent);
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerStatus);
        TryGetComponent(out _playerControlInput);
        TryGetComponent(out _commandHandler);
    }
    private void Start()
    {
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerDeath, this);
    }

    private void Update()
    {
        if (_commandHandler != null)
        {
            _playerAnimator.SetFloat("Run", _playerAgent.velocity.magnitude);
        }
        else
        {
            _playerAnimator.SetFloat("Run", 0f);
        }


        CheckAllCooldown();
    }

    private void LookAtTarget()
    {
        _commandHandler.currentCommand = null;
        _playerAgent.ResetPath();
        _playerAgent.updateRotation = false;
        Quaternion lookDirection = Quaternion.LookRotation(_playerControlInput.Hit.point - transform.position);
        transform.rotation = lookDirection;
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 1000);
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
            if (Managers.Skill.Num1SkillCooldownRemain > 0f)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.SkillInCooldown, this);
                return;
            }
            if (_playerStatus.ManaPool.CurrentValue < Managers.Skill.GetSkillData(Managers.Skill.CurrentNum1SKillName).ManaCost)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.NotEnoughMana, this);
                return;
            }
            LookAtTarget();
            Managers.Skill.StartNum1Cooldown();
            _playerStatus.ManaPool.CurrentValue -= Managers.Skill.GetSkillData(Managers.Skill.CurrentNum1SKillName).ManaCost;
            _playerAnimator.Play(Managers.Skill.GetSkillData(Managers.Skill.CurrentNum1SKillName).ResourceName);
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerManaChange, this);
        }
    }
    public void AbilityNum2(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (Managers.Skill.Num2SkillCooldownRemain > 0f)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.SkillInCooldown, this);
                return;
            }
            if (_playerStatus.ManaPool.CurrentValue < Managers.Skill.GetSkillData(Managers.Skill.CurrentNum2SKillName).ManaCost)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.NotEnoughMana, this);
                return;
            }
            LookAtTarget();
            Managers.Skill.StartNum2Cooldown();
            _playerStatus.ManaPool.CurrentValue -= Managers.Skill.GetSkillData(Managers.Skill.CurrentNum2SKillName).ManaCost;
            _playerAnimator.Play(Managers.Skill.GetSkillData(Managers.Skill.CurrentNum2SKillName).ResourceName);
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerManaChange, this);
        }
    }
    public void AbilityNum3(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (Managers.Skill.Num3SkillCooldownRemain > 0f)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.SkillInCooldown, this);
                return;
            }
            if (_playerStatus.ManaPool.CurrentValue < Managers.Skill.GetSkillData(Managers.Skill.CurrentNum3SKillName).ManaCost)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.NotEnoughMana, this);
                return;
            }
            LookAtTarget();
            Managers.Skill.StartNum3Cooldown();
            _playerStatus.ManaPool.CurrentValue -= Managers.Skill.GetSkillData(Managers.Skill.CurrentNum3SKillName).ManaCost;
            _playerAnimator.Play(Managers.Skill.GetSkillData(Managers.Skill.CurrentNum3SKillName).ResourceName);
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerManaChange, this);
        }
    }
    public void AbilityNum4(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (Managers.Skill.Num4SkillCooldownRemain > 0f)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.SkillInCooldown, this);
                return;
            }
            if (_playerStatus.ManaPool.CurrentValue < Managers.Skill.GetSkillData(Managers.Skill.CurrentNum4SKillName).ManaCost)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.NotEnoughMana, this);
                return;
            }
            LookAtTarget();
            Managers.Skill.StartNum4Cooldown();
            _playerStatus.ManaPool.CurrentValue -= Managers.Skill.GetSkillData(Managers.Skill.CurrentNum4SKillName).ManaCost;
            _playerAnimator.Play(Managers.Skill.GetSkillData(Managers.Skill.CurrentNum4SKillName).ResourceName);
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerManaChange, this);
        }
    }

    public void PlayerPotal(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (!Managers.Game.IsPlayerInRift)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.OpenPortalInTown, this);
                return;
            }
            if (Managers.Game.isPlayerPortalOpen)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerPortalAlreadyOpen, this);
                return;
            }
            Managers.Game.isPlayerPortalOpen = true;
            Managers.Resource.Instantiate("PlayerPortal").transform.position = transform.position + Vector3.up;
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
        CheckCooldown(ref Managers.Skill.M2SkillCooldownRemain);
        CheckCooldown(ref Managers.Skill.Num1SkillCooldownRemain);
        CheckCooldown(ref Managers.Skill.Num2SkillCooldownRemain);
        CheckCooldown(ref Managers.Skill.Num3SkillCooldownRemain);
        CheckCooldown(ref Managers.Skill.Num4SkillCooldownRemain);
    }

#pragma warning disable IDE0051
    private void BladeSlash()
    {
        Managers.Sound.Play("BladeSlash");
        GameObject go = Managers.Resource.Instantiate("Skill_BladeSlash");
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(new Vector3(180, transform.rotation.y, 0f));

    }

    private void DarkFlare()
    {
        Managers.Sound.Play("DarkFlare");
        GameObject go = Managers.Resource.Instantiate("DarkFlare");
        go.transform.SetPositionAndRotation(transform.position + Vector3.up * 0.5f, transform.rotation);
        go.GetComponent<Projectile>().ShootForward();
    }

    private void ShadowCleave()
    {
        Managers.Sound.Play("ShadowCleave");
        GameObject col = Managers.Resource.Instantiate("ShadowCleaveCol");
        col.transform.SetPositionAndRotation(transform.position + Vector3.up * 0.5f, transform.rotation);
        col.GetComponent<ShadowCleave>().ShootShadowCol();
        GameObject vfx = Managers.Resource.Instantiate("ShadowCleaveEffect");
        vfx.transform.SetPositionAndRotation(transform.position + Vector3.up * 0.5f, transform.rotation);
    }

    private void ShadowBlast()
    {
        Managers.Sound.Play("ShadowBlast");
        GameObject go = Managers.Resource.Instantiate("ShadowBlast");
        go.transform.SetLocalPositionAndRotation(transform.position + Vector3.up * 0.5f, transform.rotation);
    }

    private void ShadowRain()
    {
        Managers.Sound.Play("ShadowRain");
        GameObject go = Managers.Resource.Instantiate("ShadowRain");
        go.transform.position = _playerControlInput.Hit.point + Vector3.up * 0.2f;
        if (Physics.Raycast(_playerControlInput.Hit.point, Vector3.down, out RaycastHit hit, 9))
        {
            go.transform.position = hit.point + Vector3.up * 0.2f;
        }
    }

    private void ShadowImpulse()
    {
        Managers.Sound.Play("ShadowImpulse");
        GameObject go = Managers.Resource.Instantiate("ShadowImpulse");
        go.transform.position = _playerControlInput.Hit.point + Vector3.up * 0.2f;
        if (Physics.Raycast(_playerControlInput.Hit.point, Vector3.down, out RaycastHit hit, 9))
        {
            go.transform.position = hit.point + Vector3.up * 0.2f;
        }
    }

    private void Greed()
    {
        Managers.Sound.Play("Buff");
        GameObject go = Managers.Resource.Instantiate("Greed");
        go.transform.position = transform.position;
    }

    private void Karma()
    {
        Managers.Sound.Play("Buff");
        GameObject go = Managers.Resource.Instantiate("Karma");
        go.transform.position = transform.position;
    }
    private void BloodFlood()
    {
        Managers.Sound.Play("BloodFlood", Define.Sound.Effect, 0.7f);
        GameObject bloodFlood = Managers.Resource.Instantiate("BloodFlood");
        bloodFlood.transform.position = transform.position + Vector3.up * 0.1f;
    }

    private void ExposeOfDarkness()
    {
        Managers.Sound.Play("ExposeOfDarkness", Define.Sound.Effect, 1.8f);
        GameObject go = Managers.Resource.Instantiate("ExposeOfDarkness");
        go.transform.position = transform.position + Vector3.down * 0.9f;
    }

    private void FootStep()
    {
        //Managers.Sound.Play("FootStep");
    }


#pragma warning restore IDE0051
    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case Define.EVENT_TYPE.PlayerDeath:
                {
                    _playerControlInput.enabled = false;
                    _playerAnimator.SetTrigger("Die");
                    enabled = false;
                    break;
                }
        }
    }
}
