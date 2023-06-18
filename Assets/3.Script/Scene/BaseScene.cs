using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.None;
    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
        {
            Debug.Log("처음에 이벤트 매니저가 없어요");
            Managers.Resource.Instantiate("EventSystem").name = "@EventSystem";
            Debug.Log("리소스에서 찾았어요");

        }
    }

    public abstract void Clear();
}
