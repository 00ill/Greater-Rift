using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerControlInput : MonoBehaviour
{

    [HideInInspector] public Vector3 MouseInputPosition;
    [HideInInspector] public Vector3 RayToWorldIntersectionPoint;

    private CommandHandler _commandHandler;
    private InteractInput _interactInput;

    public RaycastHit Hit;
    private LayerMask _layerMask;

    private WaitForSeconds _moveTime = new WaitForSeconds(0.25f);
    private Coroutine _moveCoroutine = null;
    private void Awake()
    {
        TryGetComponent(out _commandHandler);
        TryGetComponent(out _interactInput);
    }

    private void Start()
    {
        _layerMask = (-1) - (1 << LayerMask.NameToLayer("Skill"));
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(MouseInputPosition);
        if (Physics.Raycast(ray, out Hit, float.MaxValue, _layerMask))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                RayToWorldIntersectionPoint = Hit.point;
            }
        }
    }

    public void MousePositionUpdate(InputAction.CallbackContext callbackContext)
    {
        MouseInputPosition = callbackContext.ReadValue<Vector2>();
    }

    public void LMB(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (!Managers.Game.IsUiPopUp)
            {
                if (_interactInput.AttackCheck() && Managers.Skill.M1SkillCooldownRemain <= 0)
                {
                    AttackCommand(_interactInput.AttackTarget.gameObject);
                    return;
                }

                if (_interactInput.InteractCheck())
                {
                    InteractCommand(_interactInput.InteractableObjectTarget.gameObject);
                    return;
                }
                GameObject marker = Managers.Resource.Instantiate("Marker");
                marker.transform.position = Hit.point + Vector3.up * 0.15f;
                MoveCommand(RayToWorldIntersectionPoint);
            }
        }
    }

    public void ForcedMovement(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            if (!Managers.Game.IsUiPopUp)
            {
                _moveCoroutine ??= StartCoroutine(ForcedMove());
            }
        }

        if (callbackContext.canceled)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }

        }

    }


    private void MoveCommand(Vector3 point)
    {
        _commandHandler.SetCommand(new Command(CommandType.Move, point));
    }

    private void InteractCommand(GameObject target)
    {
        _commandHandler.SetCommand(new Command(CommandType.Interact, target));
    }

    private void AttackCommand(GameObject target)
    {
        _commandHandler.SetCommand(new Command(CommandType.Attack, target));
    }

    public void OpenSkillSettingUI(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Managers.Event.PostNotification(Define.EVENT_TYPE.SkillSettingUIOpen, this);
        }
    }

    public void OpenInventoryUI(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Managers.Game.IsUiPopUp = true;
            Managers.UI.ShowPopupUI<UI_Popup>("InventoryUI");
        }
    }
    public void OpenCharacterStatusUI(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Managers.Game.IsUiPopUp = true;
            Managers.UI.ShowPopupUI<UI_Popup>("StatusUI");
        }
    }
    public void Pause(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (Managers.Game.IsUiPopUp)
            {
                Managers.UI.CloseAllPopupUI();
                Managers.Game.IsUiPopUp = false;
                Managers.Event.PostNotification(Define.EVENT_TYPE.AllPopupUIClose, this);
                return;
            }
            Managers.Event.PostNotification(Define.EVENT_TYPE.Pause, this);
        }
    }

    private IEnumerator ForcedMove()
    {
        while (true)
        {
            MoveCommand(RayToWorldIntersectionPoint);
            yield return _moveTime;
        }
    }

    public void Test(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Debug.Log("테스트 버튼");
            FindObjectOfType<PlayerStatus>().LevelUp();

        }
    }
}
