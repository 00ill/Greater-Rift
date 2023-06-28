using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : UI_Scene, IListener
{
    private PlayerControlInput _playerControlInput;

    enum Texts
    {
        PauseText,
        ContinueText,
        SaveAndQuitText,
        Level,
        InteractableObjectName,
        SkillSettingTitle,
        SkillSetM1Text,
        SkillSetM2Text,
        SkillSetNum1Text,
        SkillSetNum2Text,
        SkillSetNum3Text,
        SkillSetNum4Text,
        SkillM1PanelTitle,
        SkillM1Script,
        SkillM2PanelTitle,
        SkillM2Script
    }

    enum Images
    {
        HpFluid,
        ManaFluid,
        Cursor,
        SkillNum1,
        SkillNum2,
        SkillNum3,
        SkillNum4,
        SkillM1,
        SkillM2,
        SkillNum1Cooldown,
        SkillNum2Cooldown,
        SkillNum3Cooldown,
        SkillNum4Cooldown,
        SkillM2Cooldown,
        SkillSetingM1_Cutting,
        SkillSetingM1_Kick,
        SkillSetingM2_BladeSlash,
        SkillSetingM2_DarkFlare

    }
    enum Sliders
    {
        ExpBar,
        EnemyHpBar
    }
    enum Buttons
    {
        Continue,
        SaveAndQuit,
        SkillSettingExit,
        SkillSetM1,
        SkillSetM2,
        SkillSetNum1,
        SkillSetNum2,
        SkillSetNum3,
        SkillSetNum4,
        SkillM1Confirm,
        SkillM1Exit,
        SkillM2Confirm,
        SkillM2Exit
    }

    enum Objects
    {
        PausePanel,
        SkillSettingUI,
        SkillM1Panel,
        SkillM2Panel
    }


    private readonly string _skillPath = "Images/Skill/";


    private void Awake()
    {
        _playerControlInput = FindAnyObjectByType<PlayerControlInput>();
    }

    private void Start()
    {
        Init();
        Cursor.visible = false;
    }

    private void Update()
    {
        GetImage((int)Images.Cursor).transform.position = _playerControlInput.MouseInputPosition + new Vector3(13.3f, -31f, 0);
        UpdateSkillCooldown();
    }
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Slider>(typeof(Sliders));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(Objects));

        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerHpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerManaChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerExpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.CheckInteractableObject, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.SkillSettingUIOpen, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.Pause, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.LevelUp, this);

        PlayerHpChangeEvent(FindObjectOfType<PlayerStatus>().LifePool.CurrentValue, FindObjectOfType<PlayerStatus>().LifePool.MaxValue);
        PlayerManaChangeEvent(FindObjectOfType<PlayerStatus>().ManaPool.CurrentValue, FindObjectOfType<PlayerStatus>().ManaPool.MaxValue);
        PlayerExpChangeEvent(FindObjectOfType<PlayerStatus>().ExpPool.CurrentValue, FindObjectOfType<PlayerStatus>().ExpPool.MaxValue);

        InitTexts();
        InitSliders();
        InitPausePanel();
        InitSkillimages();
        InitButtonEvent();
        InitPanel();
    }


    private void InitTexts()
    {
        GetText((int)Texts.InteractableObjectName).text = "";
        GetText((int)Texts.SkillSettingTitle).text = "Skill Setting";
        GetText((int)Texts.SkillSetM1Text).text = "<color=white>Mouse 1 Skill Setting</color>";
        GetText((int)Texts.SkillSetM2Text).text = "<color=white>Mouse 2 Skill Setting</color>";
        GetText((int)Texts.SkillSetNum1Text).text = "<color=white>num 1 Skill Setting</color>";
        GetText((int)Texts.SkillSetNum2Text).text = "<color=white>num 2 Skill Setting</color>";
        GetText((int)Texts.SkillSetNum3Text).text = "<color=white>num 3 Skill Setting</color>";
        GetText((int)Texts.SkillSetNum4Text).text = "<color=white>num 4 Skill Setting</color>";
        GetText((int)Texts.SkillM1PanelTitle).text = "<color=white>Mouse 1 Skill Setting</color>";
        GetText((int)Texts.SkillM2PanelTitle).text = "<color=white>Mouse 2 Skill Setting</color>";
        GetText((int)Texts.SkillM1Script).text = "";
        GetText((int)Texts.SkillM2Script).text = "";
        GetText((int)Texts.Level).text = Managers.DB.CurrentPlayerData.Level.ToString();
    }

    private void InitSliders()
    {
        Get<Slider>((int)Sliders.EnemyHpBar).gameObject.SetActive(false);
    }
    private void InitSkillimages()
    {
        GetImage((int)Images.SkillNum1Cooldown).fillAmount = 0f;
        GetImage((int)Images.SkillNum2Cooldown).fillAmount = 0f;
        GetImage((int)Images.SkillNum3Cooldown).fillAmount = 0f;
        GetImage((int)Images.SkillNum4Cooldown).fillAmount = 0f;
        GetImage((int)Images.SkillM2Cooldown).fillAmount = 0f;
        GetImage((int)Images.SkillM1).sprite = Managers.Resource.Load<Sprite>(_skillPath + "Cutting");
        GetImage((int)Images.SkillM2).sprite = Managers.Resource.Load<Sprite>(_skillPath + "BladeSlash");
        GetImage((int)Images.SkillNum1).sprite = Managers.Resource.Load<Sprite>(_skillPath + "None");
        GetImage((int)Images.SkillNum2).sprite = Managers.Resource.Load<Sprite>(_skillPath + "None");
        GetImage((int)Images.SkillNum3).sprite = Managers.Resource.Load<Sprite>(_skillPath + "None");
        GetImage((int)Images.SkillNum4).sprite = Managers.Resource.Load<Sprite>(_skillPath + "None");
        //��ų�� M1
        GetImage((int)Images.SkillSetingM1_Cutting).sprite = Managers.Resource.Load<Sprite>(_skillPath + "Cutting");
        GetImage((int)Images.SkillSetingM1_Cutting).gameObject.BindEvent((PointerEventData data) =>
        {
            GetText((int)Texts.SkillM1Script).text = FindSkillScript(Images.SkillSetingM1_Cutting);
            Managers.Skill.CurrentSelectSkillName = SkillName.Cutting;
        });
        GetImage((int)Images.SkillSetingM1_Kick).sprite = Managers.Resource.Load<Sprite>(_skillPath + "Kick");
        GetImage((int)Images.SkillSetingM1_Kick).gameObject.BindEvent((PointerEventData data) =>
        {
            GetText((int)Texts.SkillM1Script).text = FindSkillScript(Images.SkillSetingM1_Kick);
            Managers.Skill.CurrentSelectSkillName = SkillName.Kick;
        });
        //��ų�� M2
        GetImage((int)Images.SkillSetingM2_BladeSlash).sprite = Managers.Resource.Load<Sprite>(_skillPath + "BladeSlash");
        GetImage((int)Images.SkillSetingM2_BladeSlash).gameObject.BindEvent((PointerEventData data) =>
        {
            GetText((int)Texts.SkillM2Script).text = FindSkillScript(Images.SkillSetingM2_BladeSlash);
            Managers.Skill.CurrentSelectSkillName = SkillName.BladeSlash;
        });
        GetImage((int)Images.SkillSetingM2_DarkFlare).sprite = Managers.Resource.Load<Sprite>(_skillPath + "DarkFlare");
        GetImage((int)Images.SkillSetingM2_DarkFlare).gameObject.BindEvent((PointerEventData data) =>
        {
            GetText((int)Texts.SkillM2Script).text = FindSkillScript(Images.SkillSetingM2_DarkFlare);
            Managers.Skill.CurrentSelectSkillName = SkillName.DarkFlare;
        });
    }

    private void InitPanel()
    {
        CloseSkillSettingUI();
        SkillM1PanelExit();
        SkillM2PanelExit();
    }

    private void InitPausePanel()
    {
        GetText((int)Texts.PauseText).text = "Pause";
        GetText((int)Texts.ContinueText).text = "Continue";
        GetText((int)Texts.SaveAndQuitText).text = "Save Exit";

        GetButton((int)Buttons.Continue).gameObject
            .BindEvent((PointerEventData data) => { Time.timeScale = 1.0f; GetObject((int)Objects.PausePanel).SetActive(false); });
        GetButton((int)Buttons.SaveAndQuit).gameObject
            .BindEvent((PointerEventData data) => { Time.timeScale = 1.0f; Managers.DB
                .SaveData(new DBManager.PlayerData(Managers.DB.CurrentPlayerData.Name, FindObjectOfType<PlayerStatus>().GetStats(Statistic.Level).IntetgerValue,
                FindObjectOfType<PlayerStatus>().GetStats(Statistic.Exp).IntetgerValue));
                GetObject((int)Objects.PausePanel).SetActive(false); Managers.Scene.LoadScene(Define.Scene.MainMenu);
            });
        GetObject((int)Objects.PausePanel).SetActive(false);
    }


    private void InitButtonEvent()
    {
        GetButton((int)Buttons.SkillSettingExit).gameObject.BindEvent((PointerEventData data) => CloseSkillSettingUI());
        GetButton((int)Buttons.SkillSetM1).gameObject.BindEvent((PointerEventData data) => SKillSetM1Open());
        GetButton((int)Buttons.SkillM1Confirm).gameObject.BindEvent((PointerEventData data) => SkillM1PanelConfirm());
        GetButton((int)Buttons.SkillM1Exit).gameObject.BindEvent((PointerEventData data) => SkillM1PanelExit());
        GetButton((int)Buttons.SkillSetM2).gameObject.BindEvent((PointerEventData data) => SKillSetM2Open());
        GetButton((int)Buttons.SkillM2Confirm).gameObject.BindEvent((PointerEventData data) => SkillM2PanelConfirm());
        GetButton((int)Buttons.SkillM2Exit).gameObject.BindEvent((PointerEventData data) => SkillM2PanelExit());
    }

    private void PlayerHpChangeEvent(float curHp, float maxHp)
    {
        GetImage((int)Images.HpFluid).material.SetFloat("_FillLevel", Mathf.Clamp(curHp / maxHp, 0, 1));
    }

    private void PlayerManaChangeEvent(float curMana, float maxMana)
    {
        GetImage((int)Images.ManaFluid).material.SetFloat("_FillLevel", Mathf.Clamp(curMana / maxMana, 0, 1));
    }

    private void PlayerExpChangeEvent(float curExp, float MaxExp)
    {
        Get<Slider>((int)Sliders.ExpBar).value = Mathf.Clamp(curExp / MaxExp, 0, 1);    
    }
    private string FindSkillScript(Images image)
    {
        switch (image)
        {
            case Images.SkillSetingM1_Cutting:
                {
                    return "Damage as much as 120% of the player's attack by cutting enemies";
                }
            case Images.SkillSetingM1_Kick:
                {
                    return "Damage as much as 120% of the player's attack by Kicking enemies";
                }
            case Images.SkillSetingM2_BladeSlash:
                {
                    return "Swing a knife in a circle, dealing 200% damage to the enemy";
                }
            case Images.SkillSetingM2_DarkFlare:
                {
                    return "Fire a sphere that deals 200% damage";
                }
        }

        return "";
    }
    #region ��ų ���� UI Open/Close/Confirm
    private void SKillSetM1Open()
    {
        GetText((int)Texts.SkillM1Script).text = "";
        Managers.Skill.CurrentChangeSkillType = SkillType.M1Skill;
        GetObject((int)Objects.SkillM1Panel).SetActive(true);
    }
    private void SkillM1PanelConfirm()
    {
        //��ų��� �̺�Ʈ ���� ��
        GetText((int)Texts.SkillM1Script).text = "";
        if (SkillLevelCheck(Texts.SkillM1Script) && Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).Type == SkillType.M1Skill)
        {
            GetImage((int)Images.SkillM1).sprite = Managers.Resource.Load<Sprite>(_skillPath
                + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).ResourceName);
            Managers.Skill.CurrentM1SKillName = Managers.Skill.CurrentSelectSkillName;
            GetObject((int)Objects.SkillM1Panel).SetActive(false);
        }
    }
    private void SkillM1PanelExit()
    {
        GetObject((int)Objects.SkillM1Panel).SetActive(false);
        GetText((int)Texts.SkillM1Script).text = "";
    }

    private void SKillSetM2Open()
    {
        GetText((int)Texts.SkillM2Script).text = "";
        Managers.Skill.CurrentChangeSkillType = SkillType.M2Skill;
        GetObject((int)Objects.SkillM2Panel).SetActive(true);
    }
    private void SkillM2PanelConfirm()
    {
        //��ų��� �̺�Ʈ ���� ��
        GetText((int)Texts.SkillM2Script).text = "";
        if (SkillLevelCheck(Texts.SkillM2Script) && Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).Type == SkillType.M2Skill)
        {
            GetImage((int)Images.SkillM2).sprite = Managers.Resource.Load<Sprite>(_skillPath
                + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).ResourceName);
            Managers.Skill.CurrentM2SKillName = Managers.Skill.CurrentSelectSkillName;
            GetObject((int)Objects.SkillM2Panel).SetActive(false);
        }
    }
    private void SkillM2PanelExit()
    {
        GetObject((int)Objects.SkillM2Panel).SetActive(false);
        GetText((int)Texts.SkillM2Script).text = "";
    }
    private void OpenSkillSettingUI()
    {
        GetObject((int)Objects.SkillSettingUI).SetActive(true);
    }
    private void CloseSkillSettingUI()
    {
        GetObject((int)Objects.SkillSettingUI).SetActive(false);
    }
    #endregion

    private bool SkillLevelCheck(Texts text)
    {
        if (Managers.Skill.GetSkillData(Managers.Skill.CurrentSelectSkillName).LevelLimit > FindObjectOfType<PlayerStatus>().GetStats(Statistic.Level).IntetgerValue)
        {
            GetText((int)text).text = string.Format($"This skill is available from level " +
                $"{Managers.Skill.GetSkillData(Managers.Skill.CurrentSelectSkillName).LevelLimit}");
            return false;
        }
        return true;
    }

    private void UpdateSkillCooldown()
    {
        GetImage((int)Images.SkillNum1Cooldown).fillAmount = Managers.Skill.Num1SkillCooldownRemain / Managers.Skill.GetSkillData(Managers.Skill.CurrentNum1SKillName).Cooldown;
        GetImage((int)Images.SkillNum2Cooldown).fillAmount = Managers.Skill.Num2SkillCooldownRemain / Managers.Skill.GetSkillData(Managers.Skill.CurrentNum2SKillName).Cooldown;
        GetImage((int)Images.SkillNum3Cooldown).fillAmount = Managers.Skill.Num3SkillCooldownRemain / Managers.Skill.GetSkillData(Managers.Skill.CurrentNum3SKillName).Cooldown;
        GetImage((int)Images.SkillNum4Cooldown).fillAmount = Managers.Skill.Num4SkillCooldownRemain / Managers.Skill.GetSkillData(Managers.Skill.CurrentNum4SKillName).Cooldown;
        GetImage((int)Images.SkillM2Cooldown).fillAmount = Managers.Skill.M2SkillCooldownRemain / Managers.Skill.GetSkillData(Managers.Skill.CurrentM2SKillName).Cooldown;

    }
    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case Define.EVENT_TYPE.PlayerHpChange:
                {
                    if (Sender.TryGetComponent(out PlayerStatus playerStatus))
                    {
                        PlayerHpChangeEvent(playerStatus.LifePool.CurrentValue, playerStatus.LifePool.MaxValue);
                    }
                    break;
                }
            case Define.EVENT_TYPE.PlayerManaChange:
                {
                    if (Sender.TryGetComponent(out PlayerStatus playerStatus))
                    {
                        PlayerManaChangeEvent(playerStatus.ManaPool.CurrentValue, playerStatus.ManaPool.MaxValue);
                    }
                    break;
                }
            case Define.EVENT_TYPE.PlayerExpChange:
                {
                    if (Sender.TryGetComponent(out PlayerStatus playerStatus))
                    {
                        PlayerExpChangeEvent(playerStatus.ExpPool.CurrentValue, playerStatus.ExpPool.MaxValue);
                    }
                    break;
                }
            case Define.EVENT_TYPE.CheckInteractableObject:
                {
                    if (Sender != null)
                    {
                        if (Sender.TryGetComponent(out Enemy.EnemyStatus enemyStatus) && !enemyStatus.IsDead)
                        {
                            //����
                            GetText((int)Texts.InteractableObjectName).text = enemyStatus.GetStats(Enemy.Statistic.Name).strValue;
                            Get<Slider>((int)Sliders.EnemyHpBar).gameObject.SetActive(true);
                            Get<Slider>((int)Sliders.EnemyHpBar).value = enemyStatus.LifePool.CurrentValue / (float)enemyStatus.LifePool.MaxValue;
                        }
                        else if (Sender.TryGetComponent(out InteractableObject interactableObject))
                        {
                            GetText((int)Texts.InteractableObjectName).text = interactableObject.ObjectName;
                            Get<Slider>((int)Sliders.EnemyHpBar).gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        GetText((int)Texts.InteractableObjectName).text = "";
                        Get<Slider>((int)Sliders.EnemyHpBar).gameObject.SetActive(false);
                    }
                    break;
                }
            case Define.EVENT_TYPE.SkillSettingUIOpen:
                {
                    OpenSkillSettingUI();
                    break;
                }
            case Define.EVENT_TYPE.Pause:
                {
                    GetObject((int)Objects.PausePanel).SetActive(true);
                    Time.timeScale = 0f;
                    break;
                }
            case Define.EVENT_TYPE.LevelUp:
                {
                    GetText((int)Texts.Level).text = FindObjectOfType<PlayerStatus>().GetStats(Statistic.Level).IntetgerValue.ToString();
                    break;
                }
        }
    }
}