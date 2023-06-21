//using Enemy;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BladeSlash : MonoBehaviour
//{
//    private void OnEffect()
//    {
//        Debug.Log("§");
//        Managers.Resource.Instantiate("Skill_BladeSlash", FindAnyObjectByType<PlayerAnimate>().transform.position);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if(other.TryGetComponent(out EnemyStatus enemyStatus))
//        {
//            enemyStatus.TakeDamage(10);
//        }
//    }
//}
