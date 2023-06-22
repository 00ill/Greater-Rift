using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : UI_Game, IListener
{
    private PlayerControlInput _playerControlInput;

    enum Texts
    {
        InteractableObjectName
    }

    enum Images
    {
        HpFluid,
        ManaFluid,
        Cursor,
        Test
    }
    enum Sliders
    {
        EnemyHpBar
    }
    enum GameObjects
    {
        DungeonNPC
    }

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
        GetImage((int)Images.Cursor).transform.position = _playerControlInput.MouseInputPosition + new Vector3(13.3f,-31f,0);
    }
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Slider>(typeof(Sliders));
        Bind<GameObject>(typeof(GameObjects));

        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerHpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerManaChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.CheckInteractableObject, this);
        PlayerHpChangeEvent(FindObjectOfType<PlayerStatus>().LifePool.CurrentValue, FindObjectOfType<PlayerStatus>().LifePool.MaxValue);
        PlayerManaChangeEvent(FindObjectOfType<PlayerStatus>().ManaPool.CurrentValue, FindObjectOfType<PlayerStatus>().ManaPool.MaxValue);

        InitTexts();
        InitSliders();

        //GetObject((int)GameObjects.DungeonNPC).BindEvent((PointerEventData data) => PopUpEnterDungeonUI(), Define.UIEvent.PointerEnter);
        //GetObject((int)GameObjects.DungeonNPC).BindEvent((PointerEventData data) => PopUpEnterDungeonUI());
        GameObject ggo = GetImage((int)Images.Test).gameObject;
        ggo.BindEvent((PointerEventData data) => PopUpEnterDungeonUI(), Define.UIEvent.PointerEnter);
        ggo.BindEvent(((PointerEventData data) => { ggo.transform.position = data.position; }), Define.UIEvent.OnDrag);
    }

    private void InitTexts()
    {
        GetText((int)Texts.InteractableObjectName).text = "";
    }

    private void InitSliders()
    {
        Get<Slider>((int)Sliders.EnemyHpBar).gameObject.SetActive(false);
    }

    private void PlayerHpChangeEvent(float curHp, float maxHp)
    {
        GetImage((int)Images.HpFluid).material.SetFloat("_FillLevel", Mathf.Clamp(curHp / maxHp, 0, 1));
    }

    private void PlayerManaChangeEvent(float curMana, float maxMana)
    {
        GetImage((int)Images.ManaFluid).material.SetFloat("_FillLevel", Mathf.Clamp(curMana / maxMana, 0, 1));
    }

    private void PopUpEnterDungeonUI()
    {
        Debug.Log("npc한테 마우스 올라감");
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
            case Define.EVENT_TYPE.CheckInteractableObject:
                {
                    if(Sender != null)
                    {
                        if(Sender.TryGetComponent(out Enemy.EnemyStatus enemyStatus) && !enemyStatus.IsDead)
                        {
                            GetText((int)Texts.InteractableObjectName).text = enemyStatus.GetStats(Enemy.Statistic.Name).strValue;
                            Get<Slider>((int)Sliders.EnemyHpBar).gameObject.SetActive(true);
                            Get<Slider>((int)Sliders.EnemyHpBar).value = enemyStatus.LifePool.CurrentValue / (float)enemyStatus.LifePool.MaxValue;
                        }
                        else
                        {
                            GetText((int)Texts.InteractableObjectName).text = "";
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
        }
    }
}
