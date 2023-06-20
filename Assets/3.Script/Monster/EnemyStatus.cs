using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Enemy
{
    public enum Statistic
    {
        Life,
        Damage,
        Armor,
        MoveSpeed,
        DetectionRange
    }

    public class StatsValue
    {
        public Statistic StatisticType;
        public int value;
        public StatsValue(Statistic statisticType, int value = 0)
        {
            this.StatisticType = statisticType;
            this.value = value;
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
            StatsList.Add(new StatsValue(Statistic.Life, 100));
            StatsList.Add(new StatsValue(Statistic.Damage, 25));
            StatsList.Add(new StatsValue(Statistic.Armor, 5));
            StatsList.Add(new StatsValue(Statistic.MoveSpeed, 10));
            StatsList.Add(new StatsValue(Statistic.DetectionRange, 30));
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
            this.MaxValue = maxValue.value;
            this.CurrentValue = MaxValue;
        }
    }
    [RequireComponent(typeof(InteractableObject))]
    public class EnemyStatus : MonoBehaviour
    {
        public AttributeGroup Attributes;
        public StatsGroup Stats;
        public ValuePool LifePool;
        private InteractableObject _interactableObject;

        private void Awake()
        {
            TryGetComponent(out  _interactableObject);
        }
        private void Start()
        {
            Attributes = new AttributeGroup();
            Attributes.Init();
            Stats = new StatsGroup();
            Stats.Init();

            LifePool = new ValuePool(Stats.Get(Statistic.Life));
        }

        public void TakeDamage(int damage)
        {
            damage = ApplyDefence(damage);
            Debug.Log(string.Format($"{transform.name}가 {damage} 데미지 입음"));
            Debug.Log(string.Format($"{transform.name}의 현재 체력 {LifePool.CurrentValue}, 최대체력 {LifePool.MaxValue}"));
            LifePool.CurrentValue -= damage;
            CheckDeath();
        }

        private int ApplyDefence(int damage)
        {
            damage -= Stats.Get(Statistic.Armor).value;
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
                Debug.Log("에너미 사망");
                OnDeath();
            }
        }

        private void OnDeath()
        {
            //임시 죽음
            _interactableObject.enabled = false;
        }

        public StatsValue GetStats(Statistic statisticToGet)
        {
            return Stats.Get(statisticToGet);
        }
    }

}
