using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public enum Statistic
{
    Level,
    Exp,
    Life,
    Mana,
    Damage,
    Armor,
    AttackSpeed,
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

    public void Init()
    {
        StatsList.Add(new StatsValue(Statistic.Level, Managers.DB.CurrentPlayerData.Level));
        StatsList.Add(new StatsValue(Statistic.Exp, Managers.DB.CurrentPlayerData.CurExp));
        StatsList.Add(new StatsValue(Statistic.Life, Managers.Data.PlayerStatusDataDict[Managers.DB.CurrentPlayerData.Level].Life));
        StatsList.Add(new StatsValue(Statistic.Mana, Managers.Data.PlayerStatusDataDict[Managers.DB.CurrentPlayerData.Level].Mana));
        StatsList.Add(new StatsValue(Statistic.Damage, Managers.Data.PlayerStatusDataDict[Managers.DB.CurrentPlayerData.Level].Damage));
        StatsList.Add(new StatsValue(Statistic.Armor, Managers.Data.PlayerStatusDataDict[Managers.DB.CurrentPlayerData.Level].Armor));
        StatsList.Add(new StatsValue(Statistic.AttackSpeed, 1f));
        StatsList.Add(new StatsValue(Statistic.MoveSpeed, 10f));
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

public enum Attribute
{
    Strength,
    Dexterity,
    Intelligence
}

[Serializable]
public class AttributeValue
{
    public Attribute AttributeType;
    public int value;
    public AttributeValue(Attribute attributeType, int value = 0)
    {
        AttributeType = attributeType;
        this.value = value;
    }
}

[Serializable]
public class AttributeGroup
{
    public List<AttributeValue> AttributesValueList;

    public AttributeGroup()
    {
        AttributesValueList = new List<AttributeValue>();
    }

    public void Init()
    {
        AttributesValueList.Add(new AttributeValue(Attribute.Strength));
        AttributesValueList.Add(new AttributeValue(Attribute.Dexterity));
        AttributesValueList.Add(new AttributeValue(Attribute.Intelligence));
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
        this.CurrentValue = cuttentValue;
    }
}

public class PlayerStatus : MonoBehaviour, IDamageable
{
    public AttributeGroup Attributes;
    public StatsGroup Stats;
    public ValuePool LifePool;
    public ValuePool ManaPool;
    public ValuePool ExpPool;

    //private void Awake()
    //{
    //    Attributes = new AttributeGroup();
    //    Attributes.Init();
    //    Stats = new StatsGroup();
    //    Stats.Init();

    //    LifePool = new ValuePool(Stats.Get(Statistic.Life));
    //    ManaPool = new ValuePool(Stats.Get(Statistic.Mana));
    //}

    private void OnEnable()
    {
        Attributes = new AttributeGroup();
        Attributes.Init();
        Stats = new StatsGroup();
        Stats.Init();

        LifePool = new ValuePool(Stats.Get(Statistic.Life));
        ManaPool = new ValuePool(Stats.Get(Statistic.Mana));
        ExpPool = new ValuePool((Managers.Data.PlayerStatusDataDict[Stats.Get(Statistic.Level).IntetgerValue].RequireExp), Stats.Get(Statistic.Exp).IntetgerValue);
    }
    //private void Start()
    //{
    //    Attributes = new AttributeGroup();
    //    Attributes.Init();
    //    Stats = new StatsGroup();
    //    Stats.Init();

    //    LifePool = new ValuePool(Stats.Get(Statistic.Life));
    //    ManaPool = new ValuePool(Stats.Get(Statistic.Mana));
    //}

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

    private void LevelUp()
    {
        GameObject go = Managers.Resource.Instantiate("LevelUp");
        go.transform.position = transform.position;

        Managers.DB.CurrentPlayerData.Level++;
        Stats.Set(Statistic.Level, Managers.DB.CurrentPlayerData.Level);
        Stats.Set(Statistic.Life, Managers.Data.PlayerStatusDataDict[Stats.Get(Statistic.Level).IntetgerValue].Life);
        Stats.Set(Statistic.Mana, Managers.Data.PlayerStatusDataDict[Stats.Get(Statistic.Level).IntetgerValue].Mana);
        Stats.Set(Statistic.Armor, Managers.Data.PlayerStatusDataDict[Stats.Get(Statistic.Level).IntetgerValue].Armor);
        Stats.Set(Statistic.Damage, Managers.Data.PlayerStatusDataDict[Stats.Get(Statistic.Level).IntetgerValue].Damage);
        Stats.Set(Statistic.Exp, 0);

        ExpPool = new ValuePool((Managers.Data.PlayerStatusDataDict[Stats.Get(Statistic.Level).IntetgerValue].RequireExp), Stats.StatsList[(int)Statistic.Exp].IntetgerValue);
        LifePool = new ValuePool(Stats.Get(Statistic.Life));
        ManaPool = new ValuePool(Stats.Get(Statistic.Mana));
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerManaChange, this);
        Managers.Event.PostNotification(Define.EVENT_TYPE.LevelUp, this);
    }
}
