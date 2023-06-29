using System.Collections.Generic;
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
        FovRange,
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

        public void Init()
        {
            //StatsList.Add(new StatsValue(Statistic.Name, enemyData.Name));
            //StatsList.Add(new StatsValue(Statistic.Life, enemyData.Life));
            //StatsList.Add(new StatsValue(Statistic.Damage, enemyData.Damage));
            //StatsList.Add(new StatsValue(Statistic.Armor, enemyData.Armor));
            //StatsList.Add(new StatsValue(Statistic.MoveSpeed, enemyData.MoveSpeed));
            //StatsList.Add(new StatsValue(Statistic.DetectionRange, enemyData.DetectionRange));
            //StatsList.Add(new StatsValue(Statistic.AttackRange, enemyData.AttackRange));
            //StatsList.Add(new StatsValue(Statistic.AttackCooldown, enemyData.AttackCooldown));
            StatsList.Add(new StatsValue(Statistic.Name, "Hellion"));
            StatsList.Add(new StatsValue(Statistic.Life, 20));
            StatsList.Add(new StatsValue(Statistic.Damage, 3));
            StatsList.Add(new StatsValue(Statistic.Armor, 5));
            StatsList.Add(new StatsValue(Statistic.MoveSpeed, 6));
            StatsList.Add(new StatsValue(Statistic.FovRange, 20));
            StatsList.Add(new StatsValue(Statistic.AttackRange, 5));
            StatsList.Add(new StatsValue(Statistic.AttackCooldown, 3));
        }

        internal StatsValue Get(Statistic statisticToGet)
        {
            return StatsList[(int)statisticToGet];
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
        private void OnEnable()
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
            _enemyAgent.isStopped = true;
            IsDead = true;
            //임시 죽음
        }

        public StatsValue GetStats(Statistic statisticToGet)
        {
            return Stats.Get(statisticToGet);
        }
    }

}
