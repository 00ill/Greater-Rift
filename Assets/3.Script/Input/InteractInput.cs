using Enemy;
using UnityEngine;

public class InteractInput : MonoBehaviour
{
    [HideInInspector] public EnemyStatus AttackTarget;
    [HideInInspector] public InteractableObject InteractableObjectTarget;
    private PlayerControlInput _playerControlInput;
    private void Awake()
    {
        TryGetComponent(out _playerControlInput);
    }

    void Update()
    {
        CheckInteractObject();
    }

    private void CheckInteractObject()
    {
        if (!_playerControlInput.Hit.Equals(null))
        {
            if (_playerControlInput.Hit.transform.TryGetComponent(out AttackTarget))
            {
                if (!AttackTarget.IsDead)
                {
                    Managers.Event.PostNotification(Define.EVENT_TYPE.CheckInteractableObject, AttackTarget);
                }
                else
                {
                    AttackTarget = null;
                }
            }
            else
            {
                AttackTarget = null;
            }
            if (_playerControlInput.Hit.transform.TryGetComponent(out InteractableObjectTarget))
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.CheckInteractableObject, InteractableObjectTarget);
            }
            else
            {
                InteractableObjectTarget = null;
            }
            if (InteractableObjectTarget == null && AttackTarget == null)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.CheckInteractableObject, null);
            }
        }
    }

    internal bool AttackCheck()
    {
        return AttackTarget != null;
    }

    internal bool InteractCheck()
    {
        return InteractableObjectTarget != null;
    }
}
