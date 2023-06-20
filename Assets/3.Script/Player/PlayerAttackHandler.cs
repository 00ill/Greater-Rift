using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private Animator _playerAnimator;
    private PlayerControl _playerControl;
    private PlayerInput _playerInput;

    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerControl);
        TryGetComponent(out _playerInput);

    }

    private void Update()
    {
        CheckNormalAttack();
    }
    private void CheckNormalAttack()
    {
        if(_playerInput.Mouse1Down)
        {
            NormalAttack(_playerControl.TargetObject);
        }
    }

    internal void NormalAttack(InteractableObject interactableObject)
    {
         
    }
}
