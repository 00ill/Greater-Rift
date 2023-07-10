using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
            //PlayerStatus playerStatus = GameObject.FindObjectOfType<PlayerStatus>();
            //float AscentCoefficient = playerStatus.GetStats(global::Statistic.Level).IntetgerValue * 0.7f;

            StatsList.Add(new StatsValue(Statistic.Name, enemyData.Name));
            StatsList.Add(new StatsValue(Statistic.Life, enemyData.Life * Managers.Game.PlayerLevel));
            StatsList.Add(new StatsValue(Statistic.Damage, enemyData.Damage * Managers.Game.PlayerLevel ));
            StatsList.Add(new StatsValue(Statistic.Armor, enemyData.Armor * Managers.Game.PlayerLevel));
            StatsList.Add(new StatsValue(Statistic.MoveSpeed, enemyData.MoveSpeed));
            StatsList.Add(new StatsValue(Statistic.FovRange, enemyData.DetectionRange));
            StatsList.Add(new StatsValue(Statistic.AttackRange, enemyData.AttackRange));
            StatsList.Add(new StatsValue(Statistic.AttackCooldown, enemyData.AttackCooldown));
            
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

    public class EnemyStatus : MonoBehaviour
    {
        [SerializeField] private EnemyData _enemyData;
        public bool IsDead;
        public StatsGroup Stats;
        public ValuePool LifePool;
        public Action OnDeath;
        private PlayerStatus _playerStatus;
        private Collider _enemyCollider;

        //Damage PopUp
        private static Canvas _gameUI;

        protected virtual void OnEnable()
        {
            Stats = new StatsGroup();
            Stats.Init(_enemyData);
            TryGetComponent(out _enemyCollider);

            LifePool = new ValuePool(Stats.Get(Statistic.Life));
            OnDeath -= CheckItemSpawn;
            OnDeath += CheckItemSpawn;
        }

        public virtual void TakeDamage(int damage, PlayerStatus playerStatus)
        {
            if (!IsDead)
            {
                _playerStatus = playerStatus;
                damage = ApplyDefence(damage);
                LifePool.CurrentValue -= damage;
                GameObject go = Managers.Resource.Instantiate("DamagePopUp");
                _gameUI = FindObjectOfType<Canvas>();
                go.transform.SetParent(_gameUI.transform, false);
                go.GetComponent<TextMeshProUGUI>().text = damage.ToString();
                go.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
                CheckDeath(playerStatus);
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

        private void CheckDeath(PlayerStatus playerStatus)
        {
            if (LifePool.CurrentValue <= 0 && !IsDead)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.CountEnemyDeath, this);
                playerStatus.GainExp(2 + playerStatus.GetStats(global::Statistic.Level).IntetgerValue / 5);
                OnDeath?.Invoke();
            }
        }


        public StatsValue GetStats(Statistic statisticToGet)
        {
            return Stats.Get(statisticToGet);
        }


        private void CheckItemSpawn()
        {
            _enemyCollider.enabled = false;
            int itemDropProb = 100;
            if (Util.Probability(itemDropProb))
            {
                Item item = Managers.Item.GenerateItem(_playerStatus.GetStats(global::Statistic.Level).IntetgerValue);
                GameObject itemBox = Managers.Resource.Instantiate("ItemBox");
                itemBox.transform.position = transform.position;
                itemBox.GetComponent<ItemBox>().ItemData = item;
            }
        }
    }
}
