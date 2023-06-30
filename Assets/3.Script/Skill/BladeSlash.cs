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
        StartCoroutine(DestroySkill());
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�ε���");
        if (other.TryGetComponent(out EnemyStatus enemyStatus))
        {
            Debug.Log("������ ����");
            enemyStatus.TakeDamage(10);
        }
    }
    private IEnumerator DestroySkill()
    {
        yield return new WaitForSeconds(0.5f);
        Managers.Resource.Destroy(gameObject);

    }
}
