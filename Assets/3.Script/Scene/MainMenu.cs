public class MainMenu : BaseScene
{
    void Start()
    {
        SceneType = Define.Scene.MainMenu;

        //��.. ��� Addserable�� ���� Asynchronous_Load �� ���ʿ� ����,
        //���� ������ �Ѿ �� ��,, �� �̷������� ���ҽ� �׶����� �ε� �ص� �Ǵµ�
        //������Ʈ�� �۱⵵ �ϰ� �ϴϱ� �ϴ� ���⼭�� �ѹ��� ��� ���� �ε� 
        //Managers.Resource.LoadAllAsync<Object>("Asynchronous_Load", (key, count, totalCount) =>
        //{
        //    //  Debug.Log($"{key} {count}/{totalCount}");

        //    if (count == totalCount)
        //    {
        //        Debug.Log("������ �ε� �Ϸ�!");
        //        StartLoaded();
        //    }
        //});

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
