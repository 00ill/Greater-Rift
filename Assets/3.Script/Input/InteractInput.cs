using Enemy;
using UnityEngine;

public class InteractInput : MonoBehaviour
{
    [HideInInspector] public EnemyStatus AttackTarget;
    [HideInInspector] public InteractableObject InteractableObjectTarget;
    private InteractHandler _interactHandler;
    private PlayerControlInput _playerControlInput;
    private void Awake()
    {
        TryGetComponent(out _interactHandler);
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
            //if (_playerControlInput.Hit.transform.TryGetComponent(out InteractableObjectTarget))
            //{
            //    //상호작용이 가능한 타겟이다.

            //    if (!(InteractableObjectTarget.TryGetComponent(out AttackTarget) && !AttackTarget.IsDead))
            //    {
            //        //몬스터가 죽었거나, 몬스터가 아닐때 // 공격대상으로 보지 않는다.
            //        AttackTarget = null;
            //    }
            //}
            //else
            //{
            //    InteractableObjectTarget = null;
            //    AttackTarget = null;
            //}
            if (_playerControlInput.Hit.transform.TryGetComponent(out AttackTarget))
            {
                if(!AttackTarget.IsDead) 
                {
                    Managers.Event.PostNotification(Define.EVENT_TYPE.CheckInteractableObject, AttackTarget);
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
        //Managers.Event.PostNotification(Define.EVENT_TYPE.CheckInteractableObject, InteractableObjectTarget);
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
