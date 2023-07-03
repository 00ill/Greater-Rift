using System.Collections;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    private WaitForSeconds _playTime = new(0.4f);
    private void OnEnable()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return _playTime;
        Managers.Resource.Destroy(gameObject);
    }
}
