using Enemy;
using System.Collections;
using UnityEngine;

public class ShadowBlast : MonoBehaviour
{
    private PlayerStatus _playerStatus;
    private WaitForSeconds _playTime = new(0.5f);
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
        if (other.TryGetComponent(out EnemyStatus enemyStatus))
        {
            enemyStatus.TakeDamage((int)(_playerStatus.GetStats(Statistic.Damage).IntetgerValue * Managers.Skill.GetSkillData(SkillName.ShadowCleave).DamageCoefficient), _playerStatus);
        }
    }

    private IEnumerator Destroy()
    {
        yield return _playTime;
        Managers.Resource.Destroy(gameObject);
    }


}
