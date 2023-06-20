using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [HideInInspector] public string ObjectName;

    private void Start()
    {
        ObjectName = transform.name;
    }
    public void Interact()
    {
        Debug.Log("테스트용이에용");
    }
}