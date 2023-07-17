public class MainMenu : BaseScene
{
    void Start()
    {
        SceneType = Define.Scene.MainMenu;
        Init();
        Managers.UI.ShowSceneUI<MainUI>();
    }

    void StartLoaded()
    {
        Init();
        Managers.UI.ShowSceneUI<MainUI>();
    }
    protected override void Init()
    {
        base.Init();


    }
    public override void Clear()
    {

    }
}
