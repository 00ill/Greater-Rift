using I18N.Common;
using System.Resources;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Rendering;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // ���ϼ��� ����ȴ�
    static Managers Instance { get { Init(); return s_instance; } } // ������ �Ŵ����� ����´�

    GameManager _game =  new GameManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    EventManager _eventManager = new EventManager();
    PoolManager _pool = new PoolManager();
    UIManager _ui = new UIManager();
    SkillManager _skill = new SkillManager();
    public static GameManager Game { get { return Instance._game; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static EventManager Event { get { return Instance._eventManager; } }
    public static PoolManager Pool { get { return Instance._pool; } }    
    public static UIManager UI { get { return Instance._ui; } }
    public static SkillManager Skill { get {  return Instance._skill; } }
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

            //�ʱ�ȭ�� �ʿ��� ������� �ʱ�ȭ�� ������ �ݴϴ�. ==> ������ ������������ ������ ù ������!!
            //������ ������ ù ���� MenMenu����
        }
    }

    public static void Clear()
    {
        Scene.Clear();
        UI.Clear();
        Event.ClearEventList();

    }
}
