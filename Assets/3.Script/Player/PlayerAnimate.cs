using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
}
