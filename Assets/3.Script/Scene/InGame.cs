public class InGame : BaseScene
{
    void Start()
    {
        SceneType = Define.Scene.Town;

        Init();
        Managers.UI.ShowSceneUI<GameUI>();
        
    }

    protected override void Init()
    {
        base.Init();
    }
    public override void Clear()
    {

    }
}
