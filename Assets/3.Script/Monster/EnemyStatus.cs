using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public enum Statistic
    {
        Name,
        Life,
        Damage,
        Armor,
        MoveSpeed,
        DetectionRange,
        AttackRange,
        AttackCooldown,
    }

    public class StatsValue
    {
        public Statistic StatisticType;
        public int value;
        public string strValue;
        public StatsValue(Statistic statisticType, int value = 0)
        {
            this.StatisticType = statisticType;
            this.value = value;
        }
        public StatsValue(Statistic statisticType, string value = "")
        {
            this.StatisticType = statisticType;
            this.strValue = value;
        }
    }

    public class StatsGroup
    {
        public List<StatsValue> StatsList;
        public StatsGroup()
        {
            StatsList = new List<StatsValue>();
        }

        public void Init(EnemyData enemyData)
        {
            StatsList.Add(new StatsValue(Statistic.Name, enemyData.Name));
            StatsList.Add(new StatsValue(Statistic.Life, enemyData.Life));
            StatsList.Add(new StatsValue(Statistic.Damage, enemyData.Damage));
            StatsList.Add(new StatsValue(Statistic.Armor, enemyData.Armor));
            StatsList.Add(new StatsValue(Statistic.MoveSpeed, enemyData.MoveSpeed));
            StatsList.Add(new StatsValue(Statistic.DetectionRange, enemyData.DetectionRange));
            StatsList.Add(new StatsValue(Statistic.AttackRange, enemyData.AttackRange));
            StatsList.Add(new StatsValue(Statistic.AttackCooldown, enemyData.AttackCooldown));
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

    //[RequireComponent(typeof(EnemyAI))]
    public class EnemyStatus : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyData _enemyData;
        public bool IsDead;
        private NavMeshAgent _enemyAgent;
        public AttributeGroup Attributes;
        public StatsGroup Stats;
        public ValuePool LifePool;

        private void Awake()
        {
            TryGetComponent(out _enemyAgent);
        }
        private void Start()
        {
            Attributes = new AttributeGroup();
            Attributes.Init();
            Stats = new StatsGroup();
            Stats.Init(_enemyData);

            LifePool = new ValuePool(Stats.Get(Statistic.Life));
        }

        public void TakeDamage(int damage)
        {
            damage = ApplyDefence(damage);
            Debug.Log(string.Format($"{transform.name}�� {damage} ������ ����"));
            Debug.Log(string.Format($"{transform.name}�� ���� ü�� {LifePool.CurrentValue}, �ִ�ü�� {LifePool.MaxValue}"));
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
                Debug.Log("���ʹ� ���");
                OnDeath();
            }
        }

        private void OnDeath()
        {
            _enemyAgent.isStopped = true;
            IsDead = true;
            //�ӽ� ����
        }

        public StatsValue GetStats(Statistic statisticToGet)
        {
            return Stats.Get(statisticToGet);
        }
    }

}
