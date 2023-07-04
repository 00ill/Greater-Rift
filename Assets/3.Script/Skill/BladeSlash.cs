using Enemy;
using System.Collections;
using UnityEngine;

public class BladeSlash : MonoBehaviour
{
    private PlayerStatus _playerStatus;

    private void Awake()
    {

    }
    private void OnEnable()
    {
        _playerStatus = FindObjectOfType<PlayerStatus>();
        StartCoroutine(DestroySkill());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyStatus enemyStatus))
        {
            enemyStatus.TakeDamage(10, _playerStatus);
        }
    }
    private IEnumerator DestroySkill()
    {
        yield return new WaitForSeconds(0.5f);
        Managers.Resource.Destroy(gameObject);

    }
}
