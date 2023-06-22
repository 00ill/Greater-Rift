using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(InteractableObject))]
public class RiftNPC : MonoBehaviour
{
    private InteractableObject _interactableObject;

    private void Awake()
    {
        TryGetComponent(out _interactableObject);
    }
    private void Start()
    {
        _interactableObject.AddInteract(ShowDialogUI);
    }

    private void ShowDialogUI()
    {
        //Managers.UI.ShowPopupUI
        Debug.Log("던전 입장 UI 띄워요");
    }

}
