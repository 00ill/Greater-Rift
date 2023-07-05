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
            //    //��ȣ�ۿ��� ������ Ÿ���̴�.

            //    if (!(InteractableObjectTarget.TryGetComponent(out AttackTarget) && !AttackTarget.IsDead))
            //    {
            //        //���Ͱ� �׾��ų�, ���Ͱ� �ƴҶ� // ���ݴ������ ���� �ʴ´�.
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
