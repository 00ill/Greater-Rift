using Enemy;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : UI_Game, IListener
{
    enum Texts
    {
        InteractableObjectName
    }

    enum Images
    {
        HpFluid,
        ManaFluid,
        Cursor
    }
    enum Sliders
    {
        EnemyHpBar
    }


    private void Start()
    {
        Init();
        Cursor.visible = false;
    }

    private void Update()
    {
        //GetImage((int)Images.Cursor).transform.position = Input.mousePosition + new Vector3(13.3f,-31f,0);
    }
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Slider>(typeof(Sliders));
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerHpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerManaChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.CheckInteractableObject, this);
        PlayerHpChangeEvent(FindObjectOfType<PlayerStatus>().LifePool.CurrentValue, FindObjectOfType<PlayerStatus>().LifePool.MaxValue);
        PlayerManaChangeEvent(FindObjectOfType<PlayerStatus>().ManaPool.CurrentValue, FindObjectOfType<PlayerStatus>().ManaPool.MaxValue);

        InitTexts();
        InitSliders();
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
                        if(Sender.TryGetComponent(out EnemyStatus enemyStatus))
                        {
                            GetText((int)Texts.InteractableObjectName).text = enemyStatus.Name;
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
