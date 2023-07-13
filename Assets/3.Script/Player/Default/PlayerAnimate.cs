using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerAnimate : MonoBehaviour, IListener
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
    private void Start()
    {
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerDeath, this);
    }
    private void Update()
    {
        _playerAnimator.SetFloat("Run", _playerAgent.velocity.magnitude);
        CheckAllCooldown();
    }

    private void LookAtTarget()
    {
        _playerAgent.ResetPath();
        _playerAgent.updateRotation = false;
        Quaternion lookDirection = Quaternion.LookRotation(_playerControlInput.Hit.point - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 10000);
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
                Debug.Log("Äð");
                Managers.Event.PostNotification(Define.EVENT_TYPE.SkillInCooldown, this);
                return;
            }
            if (_playerStatus.ManaPool.CurrentValue < Managers.Skill.GetSkillData(Managers.Skill.CurrentNum2SKillName).ManaCost)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.NotEnoughMana, this);
                return;
            }
            Debug.Log("µÇ³ª");
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

            //GameObject go = Managers.Resource.Instantiate("PlayerPortal");
            //go.transform.position = transform.position;
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

    private void BladeSlash()
    {
        GameObject go = Managers.Resource.Instantiate("Skill_BladeSlash");
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(new Vector3(180, transform.rotation.y, 0f));

    }

    private void DarkFlare()
    {
        GameObject go = Managers.Resource.Instantiate("DarkFlare");
        go.transform.SetPositionAndRotation(transform.position + Vector3.up * 0.5f, transform.rotation);
        go.GetComponent<Projectile>().ShootForward();
    }

    private void ShadowCleave()
    {
        GameObject col = Managers.Resource.Instantiate("ShadowCleaveCol");
        col.transform.SetPositionAndRotation(transform.position + Vector3.up * 0.5f, transform.rotation);
        col.GetComponent<ShadowCleave>().ShootShadowCol();
        GameObject vfx = Managers.Resource.Instantiate("ShadowCleaveEffect");
        vfx.transform.SetPositionAndRotation(transform.position + Vector3.up * 0.5f, transform.rotation);
    }

    private void ShadowRain()
    {
        GameObject go = Managers.Resource.Instantiate("ShadowRain");
        go.transform.position = _playerControlInput.Hit.point;
    }

    private void Greed()
    {
        GameObject go = Managers.Resource.Instantiate("Greed");
        go.transform.position = transform.position;
    }

    private void BloodFlood()
    {
        GameObject bloodFlood = Managers.Resource.Instantiate("BloodFlood");
        bloodFlood.transform.position = transform.position + Vector3.up *0.5f;
    }

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
