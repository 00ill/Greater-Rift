using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerAnimate : MonoBehaviour
{
    private NavMeshAgent _playerAgent;
    private Animator _playerAnimator;

    private void Awake()
    {
        TryGetComponent(out _playerAgent);
        TryGetComponent(out _playerAnimator);
    }
    private void Update()
    {
        _playerAnimator.SetFloat("Run", _playerAgent.velocity.magnitude);
    }

    public void AbilityRMB(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed)
        {
            _playerAnimator.Play("Skill_BladeSlash");
        }
    }
    
    private void BladeSlash()
    {
        Managers.Resource.Instantiate("Skill_BladeSlash", transform.position);
    }
}
