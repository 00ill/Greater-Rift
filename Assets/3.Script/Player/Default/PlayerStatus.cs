using System;
using System.Collections.Generic;
using UnityEngine;

public enum Statistic
{
    Level,
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
        StatsList.Add(new StatsValue(Statistic.Level, 1));
        StatsList.Add(new StatsValue(Statistic.Life, 100));
        StatsList.Add(new StatsValue(Statistic.Mana, 100));
        StatsList.Add(new StatsValue(Statistic.Damage, 25));
        StatsList.Add(new StatsValue(Statistic.Armor, 5));
        StatsList.Add(new StatsValue(Statistic.AttackSpeed, 1f));
        StatsList.Add(new StatsValue(Statistic.MoveSpeed, 10f));
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
}

public class PlayerStatus : MonoBehaviour, IDamageable
{
    public AttributeGroup Attributes;
    public StatsGroup Stats;
    public ValuePool LifePool;
    public ValuePool ManaPool;

    private void Awake()
    {
        Attributes = new AttributeGroup();
        Attributes.Init();
        Stats = new StatsGroup();
        Stats.Init();

        LifePool = new ValuePool(Stats.Get(Statistic.Life));
        ManaPool = new ValuePool(Stats.Get(Statistic.Mana));
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

    private void Update()
    {

    }

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
}
