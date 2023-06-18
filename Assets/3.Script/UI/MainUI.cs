using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainUI : UI_Scene
{
    enum Buttons
    {
        GameStartButton,
        LoadDataButton,
        OptionButton,
        QuitButton

    }
    enum Images
    {
        BackGround,
        MainTitle,

    }
    enum Texts
    {
        TitleText,
        GameStartText,
        LoadDataText,
        OptionText,
    }
    enum GameObjects
    {
        PlayerNamePannel,
        NameInputField,
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        //Bind<Image>(typeof(Images));
        //Bind<GameObject>(typeof(GameObjects));

        GetText((int)Texts.TitleText).text = $"Greater Rift";
        GetText((int)Texts.GameStartText).text = $"New Game";
        GetText((int)Texts.LoadDataText).text = $"Load Data";
        GetText((int)Texts.OptionText).text = $"Option";

        GetButton((int)Buttons.GameStartButton).gameObject
            .BindEvent((PointerEventData data) => GameStartEvent());
        GetButton((int)Buttons.LoadDataButton).gameObject
            .BindEvent((PointerEventData data) => LoadDataEvent());
        GetButton((int)Buttons.OptionButton).gameObject
            .BindEvent((PointerEventData data) => OptionEvent());

    }
    private void Start()
    {
        Init();
    }
    private void GameStartEvent()
    {
        //여기에 씬전환
        Managers.Scene.LoadScene(Define.Scene.Town);
    }
    private void LoadDataEvent()
    {
        //불러오기
    }

    private void OptionEvent()
    {
        //옵션 UI 출력
    }

}
