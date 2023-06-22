using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour, ICommandHandle
{
    [SerializeField] float interactRange = 0.5f;
    private PlayerControl _playerControl;

    private void Awake()
    {
        _playerControl = GetComponent<PlayerControl>();
    }

    public void ProcessCommand(Command command)
    {
        float distance = Vector3.Distance(transform.position, command.target.transform.position);

        if (distance < interactRange)
        {
            command.target.GetComponent<InteractableObject>().Interact();
            _playerControl.Stop();
            command.isComplete = true;
        }
        else
        {
            _playerControl.SetDestination(command.target.transform.position);
        }
    }
}
