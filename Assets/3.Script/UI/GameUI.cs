using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : UI_Game, IListener
{
    enum Sliders
    {

    }
    enum Images
    {
        HpFluid,
        ManaFluid
    }


    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Slider>(typeof(Sliders));
        Bind<Image>(typeof(Images));
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerHpChange, this);
        PlayerHpChangeEvent(FindObjectOfType<PlayerStatus>().Health, FindObjectOfType<PlayerStatus>().MaxHealth);
        PlayerManaChangeEvent(FindObjectOfType<PlayerStatus>().Mana, FindObjectOfType<PlayerStatus>().MaxMana);
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
                        PlayerHpChangeEvent(playerStatus.Health, playerStatus.MaxHealth);
                    }
                    break;
                }
            case Define.EVENT_TYPE.PlayerManaChange:
                {
                    if (Sender.TryGetComponent(out PlayerStatus playerStatus))
                    {
                        PlayerManaChangeEvent(playerStatus.Mana, playerStatus.MaxMana);
                    }
                    break;
                }
        }
    }
}
