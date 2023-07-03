using System.Collections;
using UnityEngine;

public class Marker : MonoBehaviour
{
    private WaitForSeconds _playTime = new WaitForSeconds(0.5f);
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
