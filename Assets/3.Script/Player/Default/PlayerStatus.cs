using System;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log(Managers.Data.PlayerStatusDataDict[Managers.DB.CurrentPlayerData.Level].Damage);
    }

    internal StatsValue Get(Statistic statisticToGet)
    {
        return StatsList[(int)statisticToGet];
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
        Debug.Log(string.Format($"{damage} 데미지 입음"));
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
            Debug.Log("플레이어 사망");
        }
    }

    public StatsValue GetStats(Statistic statisticToGet)
    {
        return Stats.Get(statisticToGet);
    }

    public void GainExp(int exp)
    {
        ExpPool.CurrentValue += exp;
        if (ExpPool.CurrentValue >= ExpPool.MaxValue)
        {
            Stats.StatsList[(int)Statistic.Level].IntetgerValue += 1;
            Stats.StatsList[(int)Statistic.Exp].IntetgerValue = 0;
            ExpPool = new ValuePool((Managers.Data.PlayerStatusDataDict[Stats.Get(Statistic.Level).IntetgerValue].RequireExp), Stats.StatsList[(int)Statistic.Exp].IntetgerValue);
            Managers.Event.PostNotification(Define.EVENT_TYPE.LevelUp, this);
        }
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerExpChange, this);
    }
}
