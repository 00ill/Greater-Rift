using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    DataManager _data = new();
    DBManager _db = new();
    EventManager _eventManager = new();
    GameManager _game = new();
    InventoryManager _inventory = new();
    ItemManager _item = new();
    PoolManager _pool = new();
    ResourceManager _resource = new();
    SceneManagerEx _scene = new();
    SkillManager _skill = new();
    SoundManager _sound = new();
    UIManager _ui = new();

    public static DataManager Data { get { return Instance._data; } }
    public static DBManager DB { get { return Instance._db; } }
    public static EventManager Event { get { return Instance._eventManager; } }
    public static GameManager Game { get { return Instance._game; } }
    public static InventoryManager Inventory { get { return Instance._inventory; } }
    public static ItemManager Item { get { return Instance._item; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SkillManager Skill { get { return Instance._skill; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    void Start()
    {
        Init();
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
            s_instance._data.Init();
            s_instance._db.Init();
            s_instance._inventory.Init();
            s_instance._sound.Init();

            //초기화가 필요한 멤버들의 초기화를 진행해 줍니다. ==> 데이터 연관되있으면 무조건 첫 씬에서!!
            //데이터 관련은 첫 씬인 MenMenu에서
        }
    }

    public static void Clear()
    {
        Scene.Clear();
        UI.Clear();
        Event.ClearEventList();
        Sound.Clear();
    }
}
