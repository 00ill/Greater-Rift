using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Statistic
{
    Level,
    Exp,
    Life,
    Mana,
    Damage,
    Armor,
    MoveSpeed
}

public class StatsValue
{
    public Statistic StatisticType;
    public bool TypeFloat;
    public int IntetgerValue;
    public float FloatValue;
    public StatsValue(Statistic statisticType, int value = 0)
    {
        this.StatisticType = statisticType;
        this.IntetgerValue = value;
    }

    public StatsValue(Statistic statisticType, float value = 0)
    {
        this.StatisticType = statisticType;
        this.FloatValue = value;
        TypeFloat = true;
    }
}

public class StatsGroup
{
    public List<StatsValue> StatsList;
    public StatsGroup()
    {
        StatsList = new List<StatsValue>();
    }

    public void InitLevel()
    {
        StatsList.Add(new StatsValue(Statistic.Level, Managers.DB.CurrentPlayerData.Level));
        StatsList.Add(new StatsValue(Statistic.Exp, Managers.DB.CurrentPlayerData.CurExp));
        StatsList.Add(new StatsValue(Statistic.Life, Managers.Data.PlayerStatusDataDict[Managers.DB.CurrentPlayerData.Level].Life));
        StatsList.Add(new StatsValue(Statistic.Mana, Managers.Data.PlayerStatusDataDict[Managers.DB.CurrentPlayerData.Level].Mana));
        StatsList.Add(new StatsValue(Statistic.Damage, Managers.Data.PlayerStatusDataDict[Managers.DB.CurrentPlayerData.Level].Damage));
        StatsList.Add(new StatsValue(Statistic.Armor, Managers.Data.PlayerStatusDataDict[Managers.DB.CurrentPlayerData.Level].Armor));
        StatsList.Add(new StatsValue(Statistic.MoveSpeed, 10f));
    }

    public void InitItemStatus()
    {
        StatsList.Add(new StatsValue(Statistic.Level, 0));
        StatsList.Add(new StatsValue(Statistic.Exp, 0));
        StatsList.Add(new StatsValue(Statistic.Life, Managers.Inventory.ItemTotal.Life));
        StatsList.Add(new StatsValue(Statistic.Mana, Managers.Inventory.ItemTotal.Mana));
        StatsList.Add(new StatsValue(Statistic.Damage, Managers.Inventory.ItemTotal.Damage));
        StatsList.Add(new StatsValue(Statistic.Armor, Managers.Inventory.ItemTotal.Armor));
        StatsList.Add(new StatsValue(Statistic.MoveSpeed, Managers.Inventory.ItemTotal.MoveSpeed));
    }

    public void UpdateItemStatus()
    {
        Set(Statistic.Level, 0);
        Set(Statistic.Exp, 0);
        Set(Statistic.Life, Managers.Inventory.ItemTotal.Life);
        Set(Statistic.Mana, Managers.Inventory.ItemTotal.Mana);
        Set(Statistic.Damage, Managers.Inventory.ItemTotal.Damage);
        Set(Statistic.Armor, Managers.Inventory.ItemTotal.Armor);
        Set(Statistic.MoveSpeed, Managers.Inventory.ItemTotal.MoveSpeed);
    }

    public void Init()
    {
        StatsList.Add(new StatsValue(Statistic.Level, 0));
        StatsList.Add(new StatsValue(Statistic.Exp, 0));
        StatsList.Add(new StatsValue(Statistic.Life, 0));
        StatsList.Add(new StatsValue(Statistic.Mana, 0));
        StatsList.Add(new StatsValue(Statistic.Damage, 0));
        StatsList.Add(new StatsValue(Statistic.Armor, 0));
        StatsList.Add(new StatsValue(Statistic.MoveSpeed, 0));
    }
    internal StatsValue Get(Statistic statisticToGet)
    {
        return StatsList[(int)statisticToGet];
    }

    internal void Set(Statistic statisticToSet, int value)
    {
        StatsList[(int)statisticToSet].IntetgerValue = value;
    }
    internal void Set(Statistic statisticToSet, float value)
    {
        StatsList[(int)statisticToSet].FloatValue = value;
    }
}

public class ValuePool
{
    public int MaxValue;
    public int CurrentValue;

    public ValuePool(StatsValue maxValue)
    {
        this.MaxValue = maxValue.IntetgerValue;
        this.CurrentValue = MaxValue;
    }

    public ValuePool(int maxValue, int cuttentValue)
    {
        this.MaxValue = maxValue;
        this.CurrentValue = Mathf.Clamp(cuttentValue, 0, maxValue);
    }
}

public class PlayerStatus : MonoBehaviour, IDamageable, IListener
{
    public StatsGroup Stats;
    public ValuePool LifePool;
    public ValuePool ManaPool;
    public ValuePool ExpPool;

    public StatsGroup LevelStatus;
    public StatsGroup ItemStatus;

    // 플레이이 이동속도 변경위한 변수
    private NavMeshAgent _playerAgent;


    private void Awake()
    {
        TryGetComponent(out _playerAgent);

    }
    private void OnEnable()
    {
        Stats = new StatsGroup();
        LevelStatus = new StatsGroup();
        ItemStatus = new StatsGroup();

        Stats.Init();
        LevelStatus.InitLevel();
        ItemStatus.InitItemStatus();
        UpdateStatus(true);

        Managers.Event.AddListener(Define.EVENT_TYPE.ChangeStatus, this);
    }

    public void TakeDamage(int damage)
    {
        damage = ApplyDefence(damage);
        LifePool.CurrentValue -= damage;
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
        CheckDeath();
    }

    private int ApplyDefence(int damage)
    {
        damage -= Stats.Get(Statistic.Armor).IntetgerValue;
        if (damage <= 0)
        {
            damage = 1;
        }
        return damage;
    }

    private void CheckDeath()
    {
        if (LifePool.CurrentValue <= 0)
        {
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerDeath, this);
            enabled = false;
        }
    }

    public StatsValue GetStats(Statistic statisticToGet)
    {
        return Stats.Get(statisticToGet);
    }

    //외부에서 플레이어 스테이터스 변경이 필요한 경우에 쓸 수 있음
    public void SetStats(Statistic statisticToSet, int value)
    {
        Stats.Set(statisticToSet, value);
    }
    public void SetStats(Statistic statisticToSet, float value)
    {
        Stats.Set(statisticToSet, value);
    }
    public void GainExp(int exp)
    {
        ExpPool.CurrentValue += exp;
        if (ExpPool.CurrentValue >= ExpPool.MaxValue)
        {
            LevelUp();
        }
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerExpChange, this);
    }

    public void LevelUp()
    {
        GameObject go = Managers.Resource.Instantiate("LevelUp");
        go.transform.position = transform.position;

        Managers.DB.CurrentPlayerData.Level++;
        LevelStatus.Set(Statistic.Level, Managers.DB.CurrentPlayerData.Level);
        LevelStatus.Set(Statistic.Life, Managers.Data.PlayerStatusDataDict[LevelStatus.Get(Statistic.Level).IntetgerValue].Life);
        LevelStatus.Set(Statistic.Mana, Managers.Data.PlayerStatusDataDict[LevelStatus.Get(Statistic.Level).IntetgerValue].Mana);
        LevelStatus.Set(Statistic.Armor, Managers.Data.PlayerStatusDataDict[LevelStatus.Get(Statistic.Level).IntetgerValue].Armor);
        LevelStatus.Set(Statistic.Damage, Managers.Data.PlayerStatusDataDict[LevelStatus.Get(Statistic.Level).IntetgerValue].Damage);
        LevelStatus.Set(Statistic.Exp, 0);

        UpdateStatus(true);
    }

    private void UpdateStatus(bool isLevelUp)
    {
        Managers.Inventory.CalcItemTotal();
        ItemStatus.UpdateItemStatus();
        SetStats(Statistic.Level, LevelStatus.Get(Statistic.Level).IntetgerValue);
        Managers.Game.PlayerLevel = LevelStatus.Get(Statistic.Level).IntetgerValue;
        SetStats(Statistic.Exp, LevelStatus.Get(Statistic.Exp).IntetgerValue);
        SetStats(Statistic.Life, LevelStatus.Get(Statistic.Life).IntetgerValue + ItemStatus.Get(Statistic.Life).IntetgerValue);
        SetStats(Statistic.Mana, LevelStatus.Get(Statistic.Mana).IntetgerValue + ItemStatus.Get(Statistic.Mana).IntetgerValue);
        SetStats(Statistic.Damage, LevelStatus.Get(Statistic.Damage).IntetgerValue + ItemStatus.Get(Statistic.Damage).IntetgerValue + Managers.Skill.AdditionalDamage);
        SetStats(Statistic.Armor, LevelStatus.Get(Statistic.Armor).IntetgerValue + ItemStatus.Get(Statistic.Armor).IntetgerValue + Managers.Skill.AdditionalArmor);
        SetStats(Statistic.MoveSpeed, LevelStatus.Get(Statistic.MoveSpeed).FloatValue + ItemStatus.Get(Statistic.MoveSpeed).FloatValue);
        _playerAgent.speed = GetStats(Statistic.MoveSpeed).FloatValue;

        ExpPool = new ValuePool((Managers.Data.PlayerStatusDataDict[Stats.Get(Statistic.Level).IntetgerValue].RequireExp), Stats.StatsList[(int)Statistic.Exp].IntetgerValue);
        if (isLevelUp)
        {
            LifePool = new ValuePool(Stats.Get(Statistic.Life));
            ManaPool = new ValuePool(Stats.Get(Statistic.Mana));
        }
        else
        {
            LifePool = new ValuePool(Stats.Get(Statistic.Life).IntetgerValue, LifePool.CurrentValue);
            ManaPool = new ValuePool(Stats.Get(Statistic.Mana).IntetgerValue, ManaPool.CurrentValue);
        }

        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerManaChange, this);
        Managers.Event.PostNotification(Define.EVENT_TYPE.LevelUp, this);
    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case Define.EVENT_TYPE.ChangeStatus:
                {
                    UpdateStatus(false);
                    break;
                }
        }
    }
}