using DG.Tweening;
using Enemy;
using System.Collections;
using UnityEngine;

public class ShadowCleave : MonoBehaviour
{
    private PlayerStatus _playerStatus;
    private WaitForSeconds _playTime = new(0.5f);
    private void Awake()
    {
        _playerStatus = FindObjectOfType<PlayerStatus>();
    }

    //private void OnEnable()
    //{
    //    //transform.DOMoveZ(18, 0.5f).SetRelative();
    //    transform.DOLocalMoveZ(18, 0.5f);
    //    //transform.DOMove(transform.position + transform.forward * 18, 0.5f);
    //    transform.DOMove(transform.position + transform.forward * 18, 0.5f);
    //}
    public void ShootShadowCol()
    {
        transform.DOMove(transform.position + transform.forward * 18, 0.5f);
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

