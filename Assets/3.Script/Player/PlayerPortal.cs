using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(InteractableObject))]
public class PlayerPortal : MonoBehaviour
{
    private InteractableObject _interactableObject;
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
        _interactableObject.AddInteract(ExitDungeon);
    }
    private IEnumerator OpenPortal()
    {
        
        _openPortal = Managers.Resource.Instantiate("PlayerPortalOpen");
        _openPortal.transform.SetParent(transform, false);
        _openPortal.transform.localPosition = Vector3.up;

        yield return _animTime;
        _openPortal.SetActive(false);
        _idlePortal = Managers.Resource.Instantiate("PlayerPortalIdle");
        _idlePortal.transform.SetParent(transform, false);
        _idlePortal.transform.localPosition = Vector3.up;
    }

    private IEnumerator ClosePortal()
    {
        _closePortal = Managers.Resource.Instantiate("PlayerPortalClose");
        _closePortal.transform.SetParent(transform, false);
        _closePortal.transform.localPosition = Vector3.up;
        _idlePortal.SetActive(false);
        yield return _animTime;
        _closePortal.SetActive(false);
        Managers.Game.isPlayerPortalOpen = false;
        Managers.Scene.LoadScene(Define.Scene.Town);
    }

    private void ExitDungeon()
    {
        Managers.Game.IsPlayerInRift = false;
        StartCoroutine(ClosePortal());  
    }
}
