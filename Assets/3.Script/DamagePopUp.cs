using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    private WaitForSeconds _playTime = new(0.25f);
    private Animation _animation;


    private void Start()
    {
        TryGetComponent(out  _animation);
    }
    private void OnEnable()
    {
        //_animation.Play();
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return _playTime;
        Managers.Resource.Destroy(gameObject);
    }
}
