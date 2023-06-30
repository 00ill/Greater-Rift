using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSlash : MonoBehaviour
{
    private readonly WaitForSeconds _playTime = new(0.25f);
    private PlayerStatus _playerStatus;

    private void Awake()
    {
        _playerStatus = GameObject.FindFirstObjectByType<PlayerStatus>();
    }

    private void OnEnable() => StartCoroutine(Destroy());
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ÆòÅ¸¶û ÀûÀÌ¶û ºÎµúÈû");
        if(other.TryGetComponent(out EnemyStatus enemyStatus))
        {
            enemyStatus.TakeDamage((int)(_playerStatus.GetStats(Statistic.Damage).IntetgerValue * Managers.Skill.GetSkillData(SkillName.ShadowSlash).DamageCoefficient));
        }
    }

    private IEnumerator Destroy()
    {
        yield return _playTime;
        Managers.Resource.Destroy(gameObject);
    }

}
