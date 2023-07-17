using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

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

    PathfindingManager _pathfinding = new();

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

    public static PathfindingManager Pathfinding { get { return Instance._pathfinding; } }
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
