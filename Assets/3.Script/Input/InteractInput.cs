using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractInput : MonoBehaviour
{

    GameObject currentHoverOverObject;

    [HideInInspector] public InteractableObject hoveringOverObject;
    [HideInInspector] public IDamageable attackTarget;

    private InteractHandler _interactHandler;

    private PlayerControlInput _playerControlInput;
    private InteractableObject _interactableObject;
    private void Awake()
    {
        TryGetComponent(out  _interactHandler);
        TryGetComponent(out _playerControlInput);
    }

    void Update()
    {
        CheckInteractObject();
    }

    private void CheckInteractObject()
    {
        if(!_playerControlInput.Hit.Equals(null))
        {
            if(_playerControlInput.Hit.transform.TryGetComponent(out _interactableObject))
            {
            }
            UpdateInteractableObject(_playerControlInput.Hit);
            Managers.Event.PostNotification(Define.EVENT_TYPE.CheckInteractableObject, _interactableObject);

        }
    }

    internal bool InteractCheck()
    {
        return _interactableObject != null;
    }

    private void UpdateInteractableObject(RaycastHit hit)
    {
        InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
        if (interactableObject != null)
        {
            hoveringOverObject = interactableObject;
            attackTarget = interactableObject.GetComponent<IDamageable>();
        }
        else
        {
            attackTarget = null;
            hoveringOverObject = null;
        }
    }
}
