using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class NormalRiftPortal : MonoBehaviour
{
    private InteractableObject _interactableObject;
    //��Ż ����Ʈ 3�� �������� ����Ʈ�� �����ֱ⸸ �ϰ�, ��ȣ�ۿ븸 �� �ϸ� �ɵ�
    private GameObject _openPortal;
    private GameObject _idlePortal;
    private GameObject _closePortal;

    private WaitForSeconds _animTime = new WaitForSeconds(0.6f);

    private void Awake()
    {
        TryGetComponent(out  _interactableObject);
    }
    private void OnEnable()
    {
        StartCoroutine(OpenPortal());
    }

    private void Start()
    {
        _interactableObject.AddInteract(EntranceDungeon);
    }

    private IEnumerator OpenPortal()
    {
        _openPortal = Managers.Resource.Instantiate("NormalPortalOpen");
        _openPortal.transform.position = transform.position;
        yield return _animTime;
        _openPortal.SetActive(false);
        _idlePortal = Managers.Resource.Instantiate("NormalPortalIdle");
        _idlePortal.transform.position = transform.position;    
    }

    private IEnumerator ClosePortal()
    {
        _idlePortal.SetActive(false);
        _closePortal = Managers.Resource.Instantiate("NormalPortalClose");
        _closePortal.transform.position = transform.position;
        yield return _animTime;
        _closePortal.SetActive(false);
        Managers.Resource.Destroy(gameObject);
    }

    private void EntranceDungeon()
    {
        Managers.Scene.LoadScene(Define.Scene.NRDungeon);
    }
}
