using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlInput : MonoBehaviour
{

    [HideInInspector] public Vector3 MouseInputPosition;
    [HideInInspector] public Vector3 RayToWorldIntersectionPoint;

    private PlayerControl _playerControl;
    private CommandHandler _commandHandler;
    private AttackInput _attackInput;
    private InteractInput _interactInput;

    public RaycastHit Hit;
    private void Awake()
    {
        TryGetComponent(out _playerControl);
        TryGetComponent(out _commandHandler);
        TryGetComponent(out _attackInput);
        TryGetComponent(out _interactInput);
    }


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(MouseInputPosition);
        if (Physics.Raycast(ray, out Hit, float.MaxValue))
        {
        RaycastHit hit;
            RayToWorldIntersectionPoint = Hit.point;
        }
    }

    public void MousePositionUpdate(InputAction.CallbackContext callbackContext)
    {
        MouseInputPosition = callbackContext.ReadValue<Vector2>();
    }

    public void LMB(InputAction.CallbackContext callbackContext)
    {
        if (_attackInput.AttackCheck())
        {
            AttackCommand(_interactInput.hoveringOverObject.gameObject);
            return;
        }

        if (_interactInput.InteractCheck())
        {
            InteractCommand(_interactInput.hoveringOverObject.gameObject);
            return;
        }

        MoveCommand(RayToWorldIntersectionPoint);
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
}
