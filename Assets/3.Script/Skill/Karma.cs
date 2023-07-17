using System.Collections;
using UnityEngine;

public class Karma : MonoBehaviour
{
    private WaitForSeconds _playTime = new WaitForSeconds(45);
    private Transform _playerTransform;

    private void OnEnable()
    {
        _playerTransform = GameObject.Find("Player").transform;
        Managers.Skill.AdditionalMoveSpeed += 3;
        Managers.Event.PostNotification(Define.EVENT_TYPE.ChangeStatus, this);
    }

    private void Update()
    {
        transform.position = _playerTransform.position;
    }

    private IEnumerator Destroy()
    {
        yield return _playTime;
        Managers.Skill.AdditionalMoveSpeed -= 3;
        Managers.Event.PostNotification(Define.EVENT_TYPE.ChangeStatus, this);
        Managers.Resource.Destroy(gameObject);
    }

}
