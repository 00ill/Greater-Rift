using Enemy;
using System.Collections;
using UnityEngine;

public class Breath : MonoBehaviour
{
    private EnemyStatus _enemyStatus;
    private PlayerStatus _playerStatus;

    private WaitForSeconds _playTime = new WaitForSeconds(4.5f);
    private WaitForSeconds _tickTime = new WaitForSeconds(0.75f);
    private void Awake()
    {
        _enemyStatus = GameObject.Find("Dragon").GetComponent<EnemyStatus>();

    }

    private void OnEnable()
    {
        StartCoroutine(Attack());
        StartCoroutine(Destroy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.TryGetComponent(out _playerStatus);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerStatus = null;
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            if (_playerStatus != null)
            {
                _playerStatus.TakeDamage((int)(_enemyStatus.GetStats(Enemy.Statistic.Damage).IntegerValue));
            }
            yield return _tickTime;
        }
    }

    private IEnumerator Destroy()
    {
        yield return _playTime;
        Managers.Resource.Destroy(this.gameObject);
    }
}
