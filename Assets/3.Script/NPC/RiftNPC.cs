using UnityEngine;
[RequireComponent(typeof(InteractableObject))]
public class RiftNPC : MonoBehaviour
{
    private InteractableObject _interactableObject;

    private void Awake()
    {
        Managers.Pathfinding.Init();
        TryGetComponent(out _interactableObject);
    }
    private void Start()
    {
        _interactableObject.AddInteract(ShowDialogUI);
        Managers.Sound.Play("TownBGM", Define.Sound.Bgm);
        Managers.Sound.SelectVolume(Define.Sound.Bgm, 0.2f);

    }

    private void ShowDialogUI()
    {
        Managers.Sound.Play("NPCTalk");
        Managers.Game.IsUiPopUp = true;
        Managers.UI.ShowPopupUI<UI_Popup>("EnterRiftUI");
    }

}
