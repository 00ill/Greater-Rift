using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainUI : UI_Scene, IListener
{
    enum Buttons
    {
        NewStartButton,
        LoadDataButton,
        OptionButton,
        QuitButton,
        CharacterConfirm,
        CharacterCancel
    }
    enum Images
    {
        BackGround,
        MainTitle,

    }
    enum Texts
    {
        TitleText,
        NewStartText,
        LoadDataText,
        OptionText,
        CharacterName,
        NameInputText,
        CharacterConfirmText,
        CharacterCancelText,
        WarningText
    }
    enum GameObjects
    {
        MenuPanel,
        NewCharacter
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        //Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        GetText((int)Texts.TitleText).text = $"Greater Rift";
        GetText((int)Texts.NewStartText).text = $"New Game";
        GetText((int)Texts.LoadDataText).text = $"Load Data";
        GetText((int)Texts.OptionText).text = $"Option";
        GetText((int)Texts.CharacterName).text = $"Character Name";
        GetText((int)Texts.CharacterConfirmText).text = $"New Start";
        GetText((int)Texts.CharacterCancelText).text = $"Cancel";
        GetText((int)Texts.WarningText).text = "";


        GetObject((int)GameObjects.NewCharacter).SetActive(false);

        GetButton((int)Buttons.NewStartButton).gameObject
            .BindEvent((PointerEventData data) => NewStartEvent());
        GetButton((int)Buttons.LoadDataButton).gameObject
            .BindEvent((PointerEventData data) => LoadDataEvent());
        GetButton((int)Buttons.OptionButton).gameObject
            .BindEvent((PointerEventData data) => OptionEvent());
        GetButton((int)Buttons.CharacterConfirm).gameObject
           .BindEvent((PointerEventData data) => CharacterConfirm());
        GetButton((int)Buttons.CharacterCancel).gameObject
     .BindEvent((PointerEventData data) => CharacterCancel());

        Managers.Event.AddListener(Define.EVENT_TYPE.DuplicateNickname, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.SuccessCreateNewPlayer, this);

    }
    private void Start()
    {
        Init();
    }
    private void NewStartEvent()
    {
        //여기에 씬전환
        //Managers.Scene.LoadScene(Define.Scene.Town);
        GetObject((int)GameObjects.MenuPanel).SetActive(false);
        GetObject((int)GameObjects.NewCharacter).SetActive(true);

    }
    private void LoadDataEvent()
    {
        //불러오기
    }

    private void OptionEvent()
    {
        //옵션 UI 출력
    }
    private void CharacterConfirm()
    {
        if (GetText((int)Texts.NameInputText).text != string.Empty)
        {
            Managers.DB.CreatePlayerData(GetText((int)Texts.NameInputText).text);
        }
        else
        {
            GetText((int)Texts.WarningText).text = "Please enter your name";
        }
    }
    private void CharacterCancel()
    {
        GetObject((int)GameObjects.MenuPanel).SetActive(true);
        GetObject((int)GameObjects.NewCharacter).SetActive(false);
    }
    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case Define.EVENT_TYPE.DuplicateNickname:
                {
                    Debug.Log("중복닉네임 이벤트");
                    GetText((int)Texts.WarningText).text = "Duplicate Name!";
                    break;
                }
            case Define.EVENT_TYPE.SuccessCreateNewPlayer:
                {
                    Debug.Log("데이터 생성 성공 이벤트");
                    Managers.Scene.LoadScene(Define.Scene.Town);
                    Debug.Log("로드씬 후");
                    break;
                }
        }
    }
}
