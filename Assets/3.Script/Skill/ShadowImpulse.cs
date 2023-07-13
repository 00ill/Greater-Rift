using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowImpulse : MonoBehaviour
{
    private PlayerStatus _playerStatus;
    private readonly WaitForSeconds _playTime = new(0.2f);


    private void Awake()
    {
        _playerStatus = FindObjectOfType<PlayerStatus>();
    }

    private void OnEnable()
    {
        StartCoroutine(Destroy());
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out EnemyStatus enemyStatus))
        {
            enemyStatus.TakeDamage((int)(_playerStatus.GetStats(Statistic.Damage).IntetgerValue * Managers.Skill.GetSkillData(SkillName.ShadowImpulse).DamageCoefficient), _playerStatus);
        }
    }

    private IEnumerator Destroy()
    {
        yield return _playTime;
        Managers.Resource.Destroy(gameObject);
    }


}
