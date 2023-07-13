using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFlood : MonoBehaviour
{
    private PlayerStatus _playerStatus;
    private readonly WaitForSeconds _playTime = new(4f);
    private readonly WaitForSeconds _tickTime = new(0.2f);
    private List<EnemyStatus> _targetEnemy = new();

    private void Awake()
    {
        _playerStatus = FindObjectOfType<PlayerStatus>();
    }

    private void OnEnable()
    {
        StartCoroutine(Destroy());
        StartCoroutine(AttackEnemy());
    }

    private IEnumerator Destroy()
    {
        yield return _playTime;
        Managers.Resource.Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyStatus enemyStatus))
        {
            _targetEnemy.Add(enemyStatus);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyStatus enemyStatus))
        {
            _targetEnemy.Remove(enemyStatus);
        }
    }

    private IEnumerator AttackEnemy()
    {
        while (true)
        {
            for (int i = 0; i < _targetEnemy.Count; i++)
            {
                _targetEnemy[i].TakeDamage((int)(_playerStatus.GetStats(Statistic.Damage).IntetgerValue * Managers.Skill.GetSkillData(SkillName.BloodFlood).DamageCoefficient), _playerStatus);
            }
            yield return _tickTime;
        }

    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
