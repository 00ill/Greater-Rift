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
        Managers.Sound.Play("TownBGM", Define.Sound.Bgm);
    }

    private void ShowDialogUI()
    {
        Managers.Game.IsUiPopUp = true;
        Managers.UI.ShowPopupUI<UI_Popup>("EnterRiftUI");
    }

}
