using Enemy;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NormalRiftUI : UI_Popup, IListener
{
    enum Texts
    {
        RiftText
    }
    enum Sliders
    {
        RiftGage,
        GuardianHPBar
    }

    enum Objects
    {
        GuardianPanel
    }

    private ValuePool _monsterPool;
    private Canvas _canvas;
    private Guardian _guardian;
    private void Awake()
    {
        TryGetComponent(out _canvas);
    }
    private void OnEnable()
    {
        _monsterPool = new ValuePool(Managers.Game.NormalRiftClearMonsterNum, 0);
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        _canvas.sortingOrder = 2;
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(Objects));
        Bind<Slider>(typeof(Sliders));

        Get<Slider>((int)Sliders.RiftGage).value = (_monsterPool.CurrentValue / _monsterPool.MaxValue);
        Get<Slider>((int)Sliders.GuardianHPBar).value = 1f;
        GetObject((int)Objects.GuardianPanel).SetActive(false);

        Managers.Event.AddListener(Define.EVENT_TYPE.CountEnemyDeath, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.GuardianHpChange, this);
    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type) 
        {
            case Define.EVENT_TYPE.CountEnemyDeath:
                {
                    _monsterPool.CurrentValue++;
                    Get<Slider>((int)Sliders.RiftGage).value = _monsterPool.CurrentValue / (float)_monsterPool.MaxValue;
                    if(_monsterPool.CurrentValue == _monsterPool.MaxValue)
                    {
                        Managers.Game.isGuardianSpawn = true;
                        GetObject((int)Objects.GuardianPanel).SetActive(true);
                        GameObject guardian = GameObject.Find("Guardian").transform.GetChild(0).gameObject;
                        guardian.SetActive(true);
                        guardian.TryGetComponent(out _guardian);

                        //보스 소환 이벤트 호출
                    }
                    break;
                }
            case Define.EVENT_TYPE.GuardianHpChange:
                {
                    Get<Slider>((int)Sliders.GuardianHPBar).value = _guardian.LifePool.CurrentValue /(float) _guardian.LifePool.MaxValue;
                    if(_guardian.LifePool.CurrentValue <= 0)
                    {
                        GetObject((int)Objects.GuardianPanel).SetActive(false);
                        GetText((int)Texts.RiftText).text = "Rift Clear! Go back to town.";
                    }
                    break;
                }
        }
    }

}
