using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    GameManager _game = new GameManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    EventManager _eventManager = new EventManager();
    PoolManager _pool = new PoolManager();
    UIManager _ui = new UIManager();
    SkillManager _skill = new SkillManager();
    DataManager _data = new DataManager();
    DBManager _db = new DBManager();
    ItemManager _item = new ItemManager();
    InventoryManager _inventory = new InventoryManager();
    public static GameManager Game { get { return Instance._game; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static EventManager Event { get { return Instance._eventManager; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static SkillManager Skill { get { return Instance._skill; } }
    public static DataManager Data { get { return Instance._data; } }
    public static DBManager DB { get { return Instance._db; } }
    public static ItemManager Item { get { return Instance._item; } }
    public static InventoryManager Inventory { get { return Instance._inventory; } }
    void Start()
    {
        Init();
        Data.Init();
        DB.Init();
        Inventory.Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance._pool.Init();

            //초기화가 필요한 멤버들의 초기화를 진행해 줍니다. ==> 데이터 연관되있으면 무조건 첫 씬에서!!
            //데이터 관련은 첫 씬인 MenMenu에서
        }
    }

    public static void Clear()
    {
        Scene.Clear();
        UI.Clear();
        Event.ClearEventList();
    }
}
