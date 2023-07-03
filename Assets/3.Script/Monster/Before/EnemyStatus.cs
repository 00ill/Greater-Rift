using System;
using System.Collections.Generic;
using TMPro;
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
        public int IntegerValue;
        public float FloatValue;
        public string strValue;
        public StatsValue(Statistic statisticType, int value = 0)
        {
            this.StatisticType = statisticType;
            this.IntegerValue = value;
        }
        public StatsValue(Statistic statisticType, float value = 0)
        {
            this.StatisticType = statisticType;
            this.FloatValue = value;
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
            StatsList.Add(new StatsValue(Statistic.FovRange, enemyData.DetectionRange));
            StatsList.Add(new StatsValue(Statistic.AttackRange, enemyData.AttackRange));
            StatsList.Add(new StatsValue(Statistic.AttackCooldown, enemyData.AttackCooldown));
            //StatsList.Add(new StatsValue(Statistic.Name, "Hellion"));
            //StatsList.Add(new StatsValue(Statistic.Life, 20));
            //StatsList.Add(new StatsValue(Statistic.Damage, 3));
            //StatsList.Add(new StatsValue(Statistic.Armor, 5));
            //StatsList.Add(new StatsValue(Statistic.MoveSpeed, 6));
            //StatsList.Add(new StatsValue(Statistic.FovRange, 20));
            //StatsList.Add(new StatsValue(Statistic.AttackRange, 2.4f));
            //StatsList.Add(new StatsValue(Statistic.AttackCooldown, 3f));
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
            this.MaxValue = maxValue.IntegerValue;
            this.CurrentValue = MaxValue;
        }
    }

    public class EnemyStatus : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyData _enemyData;
        private Animator _enemyAnimator;
        public bool IsDead;
        private NavMeshAgent _enemyAgent;
        public AttributeGroup Attributes;
        public StatsGroup Stats;
        public ValuePool LifePool;
        public Action OnDeath;


        //Damage PopUp
        private static GameObject _damagePopUpText;
        private static Canvas _gameUI;
        private void Awake()
        {
            TryGetComponent(out _enemyAgent);
            TryGetComponent(out _enemyAnimator);
        }
        private void OnEnable()
        {
            Attributes = new AttributeGroup();
            Attributes.Init();
            Stats = new StatsGroup();
            Stats.Init(_enemyData);

            LifePool = new ValuePool(Stats.Get(Statistic.Life));
            //_gameUI = GameObject.FindObjectOfType<Canvas>();
            //_damagePopUpText = Managers.Resource.Load<GameObject>("DamagePopUp");
        }

        public void TakeDamage(int damage)
        {
            if (!IsDead)
            {
                damage = ApplyDefence(damage);
                LifePool.CurrentValue -= damage;
                GameObject go = Managers.Resource.Instantiate("DamagePopUp");
                _gameUI = FindObjectOfType<Canvas>();
                go.transform.SetParent(_gameUI.transform, false);
                go.GetComponent<TextMeshProUGUI>().text = damage.ToString();
                go.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
                CheckDeath();
            }
        }

        private int ApplyDefence(int damage)
        {
            damage -= GetStats(Enemy.Statistic.Armor).IntegerValue;
            if (damage <= 0)
            {
                damage = 1;
            }
            return damage;
        }

        private void CheckDeath()
        {
            if (LifePool.CurrentValue <= 0 && !IsDead)
            {
                Debug.Log("¿¡³Ê¹Ì »ç¸Á");
                OnDeath?.Invoke();
            }
        }

        //private void OnDeath()
        //{
        //    _enemyAgent.isStopped = true;
        //    IsDead = true;
        //    _enemyAnimator.SetTrigger("Death");
        //    //ÀÓ½Ã Á×À½
        //}

        public StatsValue GetStats(Statistic statisticToGet)
        {
            return Stats.Get(statisticToGet);
        }
    }

}
