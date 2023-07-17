using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EnterRiftUI : UI_Popup
{

    enum Buttons
    {
        NormalRift,
        GreaterRift,
        Exit
    }

    enum Texts
    {
        NPCName,
        Description,
        NormalRiftText,
        GreaterRiftText
    }
    enum Images
    {
        Cursor,
        BlockMove
    }

    private PlayerControlInput _playerControlInput;
    private Vector3 _normalRiftPortalPosition = new(-2, 1.5f, 16);
    private void Awake()
    {
        _playerControlInput = FindAnyObjectByType<PlayerControlInput>();
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        GetImage((int)Images.Cursor).transform.position = _playerControlInput.MouseInputPosition + new Vector3(13.3f, -31f, 0);
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetText((int)Texts.NPCName).text = "Adria";
        GetText((int)Texts.Description).text = "Enter Normal Rift, level up and obtain equipment.\r\nEnter the Greater Rift, set a record, and get a special item.";
        GetText((int)Texts.NormalRiftText).text = "Normal Rift";
        GetText((int)Texts.GreaterRiftText).text = "Greater Rift";

        GetButton((int)Buttons.NormalRift).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            GameObject go = Managers.Resource.Instantiate("NormalRiftPortal");
            go.transform.position = _normalRiftPortalPosition;
            Managers.Game.IsUiPopUp = false;
            Managers.UI.ClosePopupUI();
        });
        GetButton((int)Buttons.GreaterRift).gameObject.BindEvent((PointerEventData data) =>
        {
        });
        GetButton((int)Buttons.Exit).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            Managers.UI.ClosePopupUI();
            Managers.Game.IsUiPopUp = false;
        });
        GetImage((int)Images.BlockMove).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Sound.Play("ButtonClick");
            Managers.UI.ClosePopupUI();
            Managers.Game.IsUiPopUp = false;
        });
    }
}
