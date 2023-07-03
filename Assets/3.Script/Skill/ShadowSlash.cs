using Enemy;
using System.Collections;
using UnityEngine;

public class ShadowSlash : MonoBehaviour
{
    private readonly WaitForSeconds _playTime = new(0.25f);
    private PlayerStatus _playerStatus;
    private readonly int _manaRecovery = 2;

    private void Awake()
    {
        _playerStatus = FindObjectOfType<PlayerStatus>();
    }


    private void OnEnable() => StartCoroutine(Destroy());
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyStatus enemyStatus))
        {
            enemyStatus.TakeDamage((int)(_playerStatus.GetStats(Statistic.Damage).IntetgerValue * Managers.Skill.GetSkillData(SkillName.ShadowSlash).DamageCoefficient));
            _playerStatus.ManaPool.CurrentValue += _manaRecovery;
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerManaChange, _playerStatus);
        }
    }

    private IEnumerator Destroy()
    {
        yield return _playTime;
        Managers.Resource.Destroy(gameObject);
    }

}
