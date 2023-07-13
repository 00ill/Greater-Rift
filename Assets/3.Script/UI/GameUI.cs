using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : UI_Scene, IListener
{
    private PlayerControlInput _playerControlInput;

    enum Texts
    {
        WarningText,
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
        SkillM2Script,
        //스킬 추가시마다 추가할 것들
        SkillNum1PanelTitle,
        SkillNum1Script,
        SkillNum2PanelTitle,
        SkillNum2Script,
        SkillNum3PanelTitle,
        SkillNum3Script,
        SkillNum4PanelTitle,
        SkillNum4Script,
        DeathText,
        DeathTownText

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
        SkillM1Cooldown,
        SkillSetingM1_Cutting,
        SkillSetingM1_Kick,
        SkillSetingM2_BladeSlash,
        SkillSetingM2_DarkFlare,
        //스킬 추가시마다 추가할 것들
        SkillSettingNum1_ShadowCleave,
        SkillSettingNum1_ShadowBlast,
        SkillSettingNum2_ShadowRain,
        SkillSettingNum2_ShadowImpulse,
        SkillSettingNum3_Greed,
        SkillSettingNum3_Karma,
        SkillSettingNum4_BloodFlood,
        SkillSettingNum4_ExposeOfDarkness,

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
        SkillM2Exit,
        //스킬 추가시마다 추가할 것들
        SkillNum1Confirm,
        SkillNum1Exit,
        SkillNum2Confirm,
        SkillNum2Exit,
        SkillNum3Confirm,
        SkillNum3Exit,
        SkillNum4Confirm,
        SkillNum4Exit,
        DeathTownButton,
        TutorialConfirm
    }

    enum Objects
    {
        PausePanel,
        SkillSettingUI,
        SkillM1Panel,
        SkillM2Panel,
        //스킬 추가시마다 추가할 것들
        SkillNum1Panel,
        SkillNum2Panel,
        SkillNum3Panel,
        SkillNum4Panel,
        DeathPanel,
        TutorialPanel

    }

    private Canvas _canvas;
    private readonly string _skillPath = "Images/Skill/";
    private readonly WaitForSeconds _warningTime = new(1);
    private Coroutine _warningCoroutine = null;
    private void Awake()
    {
        TryGetComponent(out _canvas);
        _playerControlInput = FindAnyObjectByType<PlayerControlInput>();
    }

    private void Start()
    {
        Init();
        Cursor.visible = false;
        if (Managers.Game.IsPlayerInRift)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("NormalRiftUI");
        }
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
        Managers.Event.AddListener(Define.EVENT_TYPE.NotEnoughMana, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.SkillInCooldown, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.OpenPortalInTown, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerPortalAlreadyOpen, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerDeath, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.FullInventory, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.AllPopupUIClose, this);

        PlayerHpChangeEvent(FindObjectOfType<PlayerStatus>().LifePool.CurrentValue, FindObjectOfType<PlayerStatus>().LifePool.MaxValue);
        PlayerManaChangeEvent(FindObjectOfType<PlayerStatus>().ManaPool.CurrentValue, FindObjectOfType<PlayerStatus>().ManaPool.MaxValue);
        PlayerExpChangeEvent(FindObjectOfType<PlayerStatus>().ExpPool.CurrentValue, FindObjectOfType<PlayerStatus>().ExpPool.MaxValue);

        InitPlayerUI();
        InitInformation();
        InitSkillSettingUI();
        InitPausePanel();
        InitDeathPanel();
        InitTutorialPanel();
    }


    private void InitPlayerUI()
    {
        GetText((int)Texts.Level).text = Managers.DB.CurrentPlayerData.Level.ToString();
        GetText((int)Texts.WarningText).text = "";
        GetImage((int)Images.SkillNum1Cooldown).fillAmount = 0f;
        GetImage((int)Images.SkillNum2Cooldown).fillAmount = 0f;
        GetImage((int)Images.SkillNum3Cooldown).fillAmount = 0f;
        GetImage((int)Images.SkillNum4Cooldown).fillAmount = 0f;
        GetImage((int)Images.SkillM2Cooldown).fillAmount = 0f;
        GetImage((int)Images.SkillM1).sprite = Managers.Resource.Load<Sprite>(_skillPath + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentM1SKillName).ResourceName);
        GetImage((int)Images.SkillM2).sprite = Managers.Resource.Load<Sprite>(_skillPath + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentM2SKillName).ResourceName);
        GetImage((int)Images.SkillNum1).sprite = Managers.Resource.Load<Sprite>(_skillPath + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentNum1SKillName).ResourceName);
        GetImage((int)Images.SkillNum2).sprite = Managers.Resource.Load<Sprite>(_skillPath + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentNum2SKillName).ResourceName);
        GetImage((int)Images.SkillNum3).sprite = Managers.Resource.Load<Sprite>(_skillPath + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentNum3SKillName).ResourceName);
        GetImage((int)Images.SkillNum4).sprite = Managers.Resource.Load<Sprite>(_skillPath + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentNum4SKillName).ResourceName);
    }

    private void InitInformation()
    {
        GetText((int)Texts.InteractableObjectName).text = "";
        Get<Slider>((int)Sliders.EnemyHpBar).gameObject.SetActive(false);
    }
    #region 스킬 설정 UI 초기화
    private void InitSkillSettingUI()
    {
        GetText((int)Texts.SkillSettingTitle).text = "Skill Setting";
        GetText((int)Texts.SkillSetM1Text).text = "<color=white>Mouse 1 Skill Setting</color>";
        GetText((int)Texts.SkillSetM2Text).text = "<color=white>Mouse 2 Skill Setting</color>";
        GetText((int)Texts.SkillSetNum1Text).text = "<color=white>Num 1 Skill Setting</color>";
        GetText((int)Texts.SkillSetNum2Text).text = "<color=white>Num 2 Skill Setting</color>";
        GetText((int)Texts.SkillSetNum3Text).text = "<color=white>Num 3 Skill Setting</color>";
        GetText((int)Texts.SkillSetNum4Text).text = "<color=white>Num 4 Skill Setting</color>";
        GetText((int)Texts.SkillM1PanelTitle).text = "<color=white>Mouse 1 Skill Setting</color>";
        GetText((int)Texts.SkillM2PanelTitle).text = "<color=white>Mouse 2 Skill Setting</color>";
        GetText((int)Texts.SkillNum1PanelTitle).text = "<color=white>Num 1 Skill Setting</color>";
        GetText((int)Texts.SkillNum2PanelTitle).text = "<color=white>Num 2 Skill Setting</color>";
        GetText((int)Texts.SkillNum3PanelTitle).text = "<color=white>Num 3 Skill Setting</color>";
        GetText((int)Texts.SkillNum4PanelTitle).text = "<color=white>Num 4 Skill Setting</color>";
        GetText((int)Texts.SkillM1Script).text = "";
        GetText((int)Texts.SkillM2Script).text = "";
        GetText((int)Texts.SkillNum1Script).text = "";
        GetText((int)Texts.SkillNum2Script).text = "";
        GetText((int)Texts.SkillNum3Script).text = "";
        GetText((int)Texts.SkillNum4Script).text = "";

        //스킬셋 M1
        GetImage((int)Images.SkillSetingM1_Cutting).sprite = Managers.Resource.Load<Sprite>(_skillPath + "ShadowSlash");
        GetImage((int)Images.SkillSetingM1_Cutting).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillM1Script).text = FindSkillScript(Images.SkillSetingM1_Cutting);
            Managers.Skill.CurrentSelectSkillName = SkillName.ShadowSlash;
        });
        GetImage((int)Images.SkillSetingM1_Kick).sprite = Managers.Resource.Load<Sprite>(_skillPath + "Kick");
        GetImage((int)Images.SkillSetingM1_Kick).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillM1Script).text = FindSkillScript(Images.SkillSetingM1_Kick);
            Managers.Skill.CurrentSelectSkillName = SkillName.Kick;
        });
        //스킬셋 M2
        GetImage((int)Images.SkillSetingM2_BladeSlash).sprite = Managers.Resource.Load<Sprite>(_skillPath + "BladeSlash");
        GetImage((int)Images.SkillSetingM2_BladeSlash).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillM2Script).text = FindSkillScript(Images.SkillSetingM2_BladeSlash);
            Managers.Skill.CurrentSelectSkillName = SkillName.BladeSlash;
        });
        GetImage((int)Images.SkillSetingM2_DarkFlare).sprite = Managers.Resource.Load<Sprite>(_skillPath + "DarkFlare");
        GetImage((int)Images.SkillSetingM2_DarkFlare).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillM2Script).text = FindSkillScript(Images.SkillSetingM2_DarkFlare);
            Managers.Skill.CurrentSelectSkillName = SkillName.DarkFlare;
        });
        ///스킬 만들 때마다 바꿀 곳;

        //스킬셋 Num1
        GetImage((int)Images.SkillSettingNum1_ShadowCleave).sprite = Managers.Resource.Load<Sprite>(_skillPath + "ShadowCleave");
        GetImage((int)Images.SkillSettingNum1_ShadowCleave).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillNum1Script).text = FindSkillScript(Images.SkillSettingNum1_ShadowCleave);
            Managers.Skill.CurrentSelectSkillName = SkillName.ShadowCleave;
        });
        GetImage((int)Images.SkillSettingNum1_ShadowBlast).sprite = Managers.Resource.Load<Sprite>(_skillPath + "ShadowBlast");
        GetImage((int)Images.SkillSettingNum1_ShadowBlast).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillNum1Script).text = FindSkillScript(Images.SkillSettingNum1_ShadowBlast);
            Managers.Skill.CurrentSelectSkillName = SkillName.ShadowBlast;
        });
        //스킬셋 Num2
        GetImage((int)Images.SkillSettingNum2_ShadowRain).sprite = Managers.Resource.Load<Sprite>(_skillPath + "ShadowRain");
        GetImage((int)Images.SkillSettingNum2_ShadowRain).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillNum2Script).text = FindSkillScript(Images.SkillSettingNum2_ShadowRain);
            Managers.Skill.CurrentSelectSkillName = SkillName.ShadowRain;
        });
        GetImage((int)Images.SkillSettingNum2_ShadowImpulse).sprite = Managers.Resource.Load<Sprite>(_skillPath + "None");
        GetImage((int)Images.SkillSettingNum2_ShadowImpulse).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillNum2Script).text = FindSkillScript(Images.SkillSettingNum2_ShadowImpulse);
            Managers.Skill.CurrentSelectSkillName = SkillName.ShadowImpulse;
        });
        //스킬셋 Num3
        GetImage((int)Images.SkillSettingNum3_Karma).sprite = Managers.Resource.Load<Sprite>(_skillPath + "Karma");
        GetImage((int)Images.SkillSettingNum3_Karma).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillNum3Script).text = FindSkillScript(Images.SkillSettingNum3_Karma);
            Managers.Skill.CurrentSelectSkillName = SkillName.Karma;
        });
        GetImage((int)Images.SkillSettingNum3_Greed).sprite = Managers.Resource.Load<Sprite>(_skillPath + "Greed");
        GetImage((int)Images.SkillSettingNum3_Greed).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillNum3Script).text = FindSkillScript(Images.SkillSettingNum3_Greed);
            Managers.Skill.CurrentSelectSkillName = SkillName.Greed;
        });
        //스킬셋 Num4
        GetImage((int)Images.SkillSettingNum4_BloodFlood).sprite = Managers.Resource.Load<Sprite>(_skillPath + "BloodFlood");
        GetImage((int)Images.SkillSettingNum4_BloodFlood).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillNum4Script).text = FindSkillScript(Images.SkillSettingNum4_BloodFlood);
            Managers.Skill.CurrentSelectSkillName = SkillName.BloodFlood;
        });
        GetImage((int)Images.SkillSettingNum4_ExposeOfDarkness).sprite = Managers.Resource.Load<Sprite>(_skillPath + "ExposeOfDarkness");
        GetImage((int)Images.SkillSettingNum4_ExposeOfDarkness).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GetText((int)Texts.SkillNum4Script).text = FindSkillScript(Images.SkillSettingNum4_ExposeOfDarkness);
            Managers.Skill.CurrentSelectSkillName = SkillName.ExposeOfDarkness;
        });

        GetButton((int)Buttons.SkillSettingExit).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); CloseSkillSettingUI(); });
        GetButton((int)Buttons.SkillSetM1).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SKillSetM1Open(); });
        GetButton((int)Buttons.SkillM1Confirm).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillM1PanelConfirm(); });
        GetButton((int)Buttons.SkillM1Exit).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillM1PanelExit(); });
        GetButton((int)Buttons.SkillSetM2).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SKillSetM2Open(); });
        GetButton((int)Buttons.SkillM2Confirm).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillM2PanelConfirm(); });
        GetButton((int)Buttons.SkillM2Exit).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillM2PanelExit(); });
        GetButton((int)Buttons.SkillSetNum1).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SKillSetNum1Open(); });
        GetButton((int)Buttons.SkillNum1Confirm).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillNum1PanelConfirm(); });
        GetButton((int)Buttons.SkillNum1Exit).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillNum1PanelExit(); });
        GetButton((int)Buttons.SkillSetNum2).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SKillSetNum2Open(); });
        GetButton((int)Buttons.SkillNum2Confirm).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillNum2PanelConfirm(); });
        GetButton((int)Buttons.SkillNum2Exit).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillNum2PanelExit(); });
        GetButton((int)Buttons.SkillSetNum3).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SKillSetNum3Open(); });
        GetButton((int)Buttons.SkillNum3Confirm).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillNum3PanelConfirm(); });
        GetButton((int)Buttons.SkillNum3Exit).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillNum3PanelExit(); });
        GetButton((int)Buttons.SkillSetNum4).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SKillSetNum4Open(); });   
        GetButton((int)Buttons.SkillNum4Confirm).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillNum4PanelConfirm(); });
        GetButton((int)Buttons.SkillNum4Exit).gameObject.BindEvent((PointerEventData data) => { Managers.Sound.Play("ButtonClick"); SkillNum4PanelExit(); });

        CloseSkillSettingUI();
        SkillM1PanelExit();
        SkillM2PanelExit();
        SkillNum1PanelExit();
        SkillNum2PanelExit();
        SkillNum3PanelExit();
        SkillNum4PanelExit();
    }
    #endregion
    private void InitPausePanel()
    {
        GetText((int)Texts.PauseText).text = "Pause";
        GetText((int)Texts.ContinueText).text = "Continue";
        GetText((int)Texts.SaveAndQuitText).text = "Save Exit";

        GetButton((int)Buttons.Continue).gameObject.BindEvent((PointerEventData data) =>
            {
                Managers.Sound.Play("ButtonClick");
                _canvas.sortingOrder = 0;
                Time.timeScale = 1.0f;
                GetObject((int)Objects.PausePanel).SetActive(false);
            });
        GetButton((int)Buttons.SaveAndQuit).gameObject.BindEvent((PointerEventData data) =>
            {
                Managers.Sound.Play("ButtonClick");
                _canvas.sortingOrder = 0;
                Time.timeScale = 1.0f;
                Managers.DB.SaveData(new DBManager.PlayerData(Managers.DB.CurrentPlayerData.Name,
                    FindObjectOfType<PlayerStatus>().GetStats(Statistic.Level).IntetgerValue,
                    FindObjectOfType<PlayerStatus>().ExpPool.CurrentValue));
                Managers.DB.ResetLoadData();
                Managers.Skill.ResetSkillCooldown();
                GetObject((int)Objects.PausePanel).SetActive(false);
                Managers.Scene.LoadScene(Define.Scene.MainMenu);
            });
        GetObject((int)Objects.PausePanel).SetActive(false);
    }

    private void InitDeathPanel()
    {
        GetText((int)Texts.DeathText).text = "You Die";
        GetText((int)Texts.DeathTownText).text = "Return Town";
        GetObject((int)Objects.DeathPanel).SetActive(false);

        GetButton((int)Buttons.DeathTownButton).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            _canvas.sortingOrder = 0;
            Managers.Event.PostNotification(Define.EVENT_TYPE.TurnBackTown, this);
            Managers.Scene.LoadScene(Define.Scene.Town);
        });
    }

    private void InitTutorialPanel()
    {
        if (FindObjectOfType<PlayerStatus>().GetStats(Statistic.Level).IntetgerValue > 1)
        {
            GetObject((int)Objects.TutorialPanel).SetActive(false);
        }
        else
        {
            _canvas.sortingOrder = 99;
            Managers.Game.IsUiPopUp = true;
        }
        GetButton((int)Buttons.TutorialConfirm).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            _canvas.sortingOrder = 0;
            GetObject((int)Objects.TutorialPanel).SetActive(false);
            Managers.Game.IsUiPopUp = false;
        });
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
                    return "Shoot a sphere and give 120% damage to 10 enemies around the sphere.";
                }
            case Images.SkillSettingNum1_ShadowCleave:
                {
                    return "200% damage to the enemy in a straight line.";
                }
            case Images.SkillSettingNum1_ShadowBlast:
                {
                    return "250% damage to the enemy in a straight line.";
                }
            case Images.SkillSettingNum2_ShadowRain:
                {
                    return "Rain for 10 seconds and damage 180% to enemies within a certain range.";
                }
            case Images.SkillSettingNum2_ShadowImpulse:
                {
                    return "360% damage to the enemy in a straight line.";
                }
            case Images.SkillSettingNum3_Karma:
                {
                    return "Increase the MoveSpeed by 3 for 45 seconds.";
                }
            case Images.SkillSettingNum3_Greed:
                {
                    return "Increases attack and Armor by 50";
                }
            case Images.SkillSettingNum4_BloodFlood:
                {
                    return "Spread attacks that cause 500% damage.";
                }
            case Images.SkillSettingNum4_ExposeOfDarkness:
                {
                    return "Spread attacks in large Area that cause 300% damage.";
                } 
        }

        return "";
    }
    #region 스킬 세팅 UI Open/Close/Confirm
    private void SKillSetM1Open()
    {
        GetText((int)Texts.SkillM1Script).text = "";
        Managers.Skill.CurrentChangeSkillType = SkillType.M1Skill;
        GetObject((int)Objects.SkillM1Panel).SetActive(true);
    }
    private void SkillM1PanelConfirm()
    {
        //스킬등록 이벤트 넣을 곳
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
        //스킬등록 이벤트 넣을 곳
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
    private void SKillSetNum1Open()
    {
        GetText((int)Texts.SkillNum1Script).text = "";
        Managers.Skill.CurrentChangeSkillType = SkillType.SkillSet1;
        GetObject((int)Objects.SkillNum1Panel).SetActive(true);
    }
    private void SkillNum1PanelConfirm()
    {
        //스킬등록 이벤트 넣을 곳
        GetText((int)Texts.SkillNum1Script).text = "";
        if (SkillLevelCheck(Texts.SkillNum1Script) && Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).Type == SkillType.SkillSet1)
        {
            GetImage((int)Images.SkillNum1).sprite = Managers.Resource.Load<Sprite>(_skillPath
                + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).ResourceName);
            Managers.Skill.CurrentNum1SKillName = Managers.Skill.CurrentSelectSkillName;
            GetObject((int)Objects.SkillM1Panel).SetActive(false);
        }
    }
    private void SkillNum1PanelExit()
    {
        GetObject((int)Objects.SkillNum1Panel).SetActive(false);
        GetText((int)Texts.SkillNum1Script).text = "";
    }

    private void SKillSetNum2Open()
    {
        GetText((int)Texts.SkillNum2Script).text = "";
        Managers.Skill.CurrentChangeSkillType = SkillType.SkillSet2;
        GetObject((int)Objects.SkillNum2Panel).SetActive(true);
    }
    private void SkillNum2PanelConfirm()
    {
        //스킬등록 이벤트 넣을 곳
        GetText((int)Texts.SkillNum2Script).text = "";
        if (SkillLevelCheck(Texts.SkillNum2Script) && Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).Type == SkillType.SkillSet2)
        {
            GetImage((int)Images.SkillNum2).sprite = Managers.Resource.Load<Sprite>(_skillPath
                + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).ResourceName);
            Managers.Skill.CurrentNum2SKillName = Managers.Skill.CurrentSelectSkillName;
            GetObject((int)Objects.SkillNum2Panel).SetActive(false);
        }
    }
    private void SkillNum2PanelExit()
    {
        GetObject((int)Objects.SkillNum2Panel).SetActive(false);
        GetText((int)Texts.SkillNum2Script).text = "";
    }
    private void SKillSetNum3Open()
    {
        GetText((int)Texts.SkillNum3Script).text = "";
        Managers.Skill.CurrentChangeSkillType = SkillType.SkillSet3;
        GetObject((int)Objects.SkillNum3Panel).SetActive(true);
    }
    private void SkillNum3PanelConfirm()
    {
        //스킬등록 이벤트 넣을 곳
        GetText((int)Texts.SkillNum3Script).text = "";
        if (SkillLevelCheck(Texts.SkillNum3Script) && Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).Type == SkillType.SkillSet3)
        {
            GetImage((int)Images.SkillNum3).sprite = Managers.Resource.Load<Sprite>(_skillPath
                + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).ResourceName);
            Managers.Skill.CurrentNum3SKillName = Managers.Skill.CurrentSelectSkillName;
            GetObject((int)Objects.SkillNum3Panel).SetActive(false);
        }
    }
    private void SkillNum3PanelExit()
    {
        GetObject((int)Objects.SkillNum3Panel).SetActive(false);
        GetText((int)Texts.SkillNum3Script).text = "";
    }
    private void SKillSetNum4Open()
    {
        GetText((int)Texts.SkillNum4Script).text = "";
        Managers.Skill.CurrentChangeSkillType = SkillType.SkillSet4;
        GetObject((int)Objects.SkillNum4Panel).SetActive(true);
    }
    private void SkillNum4PanelConfirm()
    {
        //스킬등록 이벤트 넣을 곳
        GetText((int)Texts.SkillNum4Script).text = "";
        if (SkillLevelCheck(Texts.SkillNum4Script) && Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).Type == SkillType.SkillSet4)
        {
            GetImage((int)Images.SkillNum4).sprite = Managers.Resource.Load<Sprite>(_skillPath
                + Managers.Skill.Skills.GetSkillData(Managers.Skill.CurrentSelectSkillName).ResourceName);
            Managers.Skill.CurrentNum4SKillName = Managers.Skill.CurrentSelectSkillName;
            GetObject((int)Objects.SkillNum4Panel).SetActive(false);
        }
    }
    private void SkillNum4PanelExit()
    {
        GetObject((int)Objects.SkillNum4Panel).SetActive(false);
        GetText((int)Texts.SkillNum4Script).text = "";
    }


    private void OpenSkillSettingUI()
    {
        GetObject((int)Objects.SkillSettingUI).SetActive(true);
        Managers.Game.IsUiPopUp = true;
    }
    private void CloseSkillSettingUI()
    {
        SkillM1PanelExit();
        SkillM2PanelExit();
        SkillNum1PanelExit();
        SkillNum2PanelExit();
        SkillNum3PanelExit();
        SkillNum4PanelExit();
        GetObject((int)Objects.SkillSettingUI).SetActive(false);
        Managers.Game.IsUiPopUp = false;
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
        GetImage((int)Images.SkillM1Cooldown).fillAmount = Managers.Skill.M1SkillCooldownRemain / Managers.Skill.GetSkillData(Managers.Skill.CurrentM1SKillName).Cooldown;
        GetImage((int)Images.SkillM2Cooldown).fillAmount = Managers.Skill.M2SkillCooldownRemain / Managers.Skill.GetSkillData(Managers.Skill.CurrentM2SKillName).Cooldown;
    }

    private IEnumerator WarningText()
    {
        yield return _warningTime;
        GetText((int)Texts.WarningText).text = "";
        _warningCoroutine = null;

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
                        if (!Managers.Game.isGuardianSpawn)
                        {
                            if (Sender.TryGetComponent(out Enemy.EnemyStatus enemyStatus) && !enemyStatus.IsDead)
                            {
                                //몬스터이고 죽지 않았다면
                                GetText((int)Texts.InteractableObjectName).text = enemyStatus.GetStats(Enemy.Statistic.Name).strValue;
                                Get<Slider>((int)Sliders.EnemyHpBar).gameObject.SetActive(true);
                                Get<Slider>((int)Sliders.EnemyHpBar).value = enemyStatus.LifePool.CurrentValue / (float)enemyStatus.LifePool.MaxValue;
                            }
                            else if (Sender.TryGetComponent(out InteractableObject interactableObject))
                            {
                                //상호작용이 가능하다면
                                GetText((int)Texts.InteractableObjectName).text = interactableObject.ObjectName;
                                Get<Slider>((int)Sliders.EnemyHpBar).gameObject.SetActive(false);
                            }

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
                    _canvas.sortingOrder = 99;
                    GetObject((int)Objects.PausePanel).SetActive(true);
                    Time.timeScale = 0f;
                    break;
                }
            case Define.EVENT_TYPE.LevelUp:
                {
                    GetText((int)Texts.Level).text = FindObjectOfType<PlayerStatus>().GetStats(Statistic.Level).IntetgerValue.ToString();
                    break;
                }
            case Define.EVENT_TYPE.SkillInCooldown:
                {
                    GetText((int)Texts.WarningText).text = "Ability in Cooldown!";
                    if (_warningCoroutine != null)
                    {
                        StopCoroutine(_warningCoroutine);
                        _warningCoroutine = null;
                    }
                    _warningCoroutine = StartCoroutine(WarningText());
                    break;
                }
            case Define.EVENT_TYPE.NotEnoughMana:
                {
                    GetText((int)Texts.WarningText).text = "There is not enough mana!";
                    if (_warningCoroutine != null)
                    {
                        StopCoroutine(_warningCoroutine);
                        _warningCoroutine = null;
                    }
                    _warningCoroutine = StartCoroutine(WarningText());
                    break;
                }
            case Define.EVENT_TYPE.OpenPortalInTown:
                {
                    GetText((int)Texts.WarningText).text = "Can't Open Portal In Town!";
                    if (_warningCoroutine != null)
                    {
                        StopCoroutine(_warningCoroutine);
                        _warningCoroutine = null;
                    }
                    _warningCoroutine = StartCoroutine(WarningText());
                    break;
                }
            case Define.EVENT_TYPE.PlayerPortalAlreadyOpen:
                {
                    GetText((int)Texts.WarningText).text = "Portal is Already Open!";
                    if (_warningCoroutine != null)
                    {
                        StopCoroutine(_warningCoroutine);
                        _warningCoroutine = null;
                    }
                    _warningCoroutine = StartCoroutine(WarningText());
                    break;
                }
            case Define.EVENT_TYPE.PlayerDeath:
                {
                    _canvas.sortingOrder = 99;
                    GetObject((int)Objects.DeathPanel).SetActive(true);
                    break;
                }
            case Define.EVENT_TYPE.FullInventory:
                {
                    GetText((int)Texts.WarningText).text = "Inventory is Full!";
                    if (_warningCoroutine != null)
                    {
                        StopCoroutine(_warningCoroutine);
                        _warningCoroutine = null;
                    }
                    _warningCoroutine = StartCoroutine(WarningText());
                    break;
                }
            case Define.EVENT_TYPE.AllPopupUIClose:
                {
                    CloseSkillSettingUI();
                    break;
                }
        }
    }
}