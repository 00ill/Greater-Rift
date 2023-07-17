using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class NormalRiftPortal : MonoBehaviour
{
    private InteractableObject _interactableObject;
    private GameObject _openPortal;
    private GameObject _idlePortal;
    private GameObject _closePortal;

    private WaitForSeconds _animTime = new WaitForSeconds(0.6f);

    private void Awake()
    {
        TryGetComponent(out _interactableObject);
    }
    private void OnEnable()
    {
        _interactableObject.enabled = true;
        StartCoroutine(OpenPortal());
    }

    private void Start()
    {
        _interactableObject.AddInteract(EntranceDungeon);
    }

    private IEnumerator OpenPortal()
    {
        _openPortal = Managers.Resource.Instantiate("NormalPortalOpen");
        _openPortal.transform.SetParent(transform, false);
        _openPortal.transform.localPosition = Vector3.up;
        yield return _animTime;
        _openPortal.SetActive(false);
        _idlePortal = Managers.Resource.Instantiate("NormalPortalIdle");
        _idlePortal.transform.SetParent(transform, false);
        _idlePortal.transform.localPosition = Vector3.up;
    }

    private IEnumerator ClosePortal()
    {
        _closePortal = Managers.Resource.Instantiate("NormalPortalClose");
        _closePortal.transform.SetParent(transform, false);
        _closePortal.transform.localPosition = Vector3.up;
        _idlePortal.SetActive(false);
        yield return _animTime;
        _closePortal.SetActive(false);
        Managers.Scene.LoadScene(SelectRandomScene());
    }

    private void EntranceDungeon()
    {
        _interactableObject.enabled = false;
        Managers.Game.IsPlayerInRift = true;
        StartCoroutine(ClosePortal());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private Define.Scene SelectRandomScene()
    {
        if (Util.Probability(0))
        {
            return Define.Scene.Desert;
        }
        return Define.Scene.NRDungeon;
    }


}
