using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour, ICommandHandle
{
    [SerializeField] float interactRange = 0.5f;


    private void Awake()
    {
    }

    public void ProcessCommand(Command command)
    {
        float distance = Vector3.Distance(transform.position, command.target.transform.position);

        //if (distance < interactRange)
        //{
        //    command.target.GetComponent<InteractableObject>().Interact(inventory);
        //    characterMovement.Stop();
        //    command.isComplete = true;
        //}
        //else
        //{
        //    characterMovement.SetDestination(command.target.transform.position);
        //}
    }
}
