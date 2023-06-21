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
    public void AbilityRMB(InputAction.CallbackContext callbackContext)
    {
        _playerAnimator.Play("Skill_BladeSlash");
        Managers.Resource.Instantiate("Skill_BladeSlash", transform.position);
    }
    private void Update()
    {
        _playerAnimator.SetFloat("Run", _playerAgent.velocity.magnitude);
    }
}
