using Enemy;
using System.Collections;
using UnityEngine;

public class BladeSlash : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(DestroySkill());
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ºÎµúÈû");
        if (other.TryGetComponent(out EnemyStatus enemyStatus))
        {
            Debug.Log("µ¥¹ÌÁö ÀÔÈû");
            enemyStatus.TakeDamage(10);
        }
    }
    private IEnumerator DestroySkill()
    {
        yield return new WaitForSeconds(0.5f);
        Managers.Resource.Destroy(gameObject);

    }
}
