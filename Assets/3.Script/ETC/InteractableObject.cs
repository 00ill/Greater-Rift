using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Action _interact;
    [HideInInspector] public string ObjectName;

    private void Start()
    {
        ObjectName = transform.name;
    }

    public void AddInteract(Action action)
    {
        _interact -= action;
        _interact += action;
    }

    public void Interact()
    {
        _interact?.Invoke();
    }
}