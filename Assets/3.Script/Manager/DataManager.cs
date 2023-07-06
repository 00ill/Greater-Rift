using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.PlayerStatus> PlayerStatusDataDict { get; private set; } = new Dictionary<int, Data.PlayerStatus>();
    public Dictionary<int, Data.ItemStatus> ItemStatusDataDict { get; private set; } = new Dictionary<int, Data.ItemStatus>();

    public void Init()
    {
        PlayerStatusDataDict = LoadJson<Data.PlayerStatusLoader, int, Data.PlayerStatus>("PlayerStatusData").MakeDict();
        ItemStatusDataDict = LoadJson<Data.ItemStatusLoader, int, Data.ItemStatus>("ItemStatusData").MakeDict();
    }


    //지정해둔 Loader형식을 받는 LoadJSon 함수 
    // Loader,와 Key,Value를 받은 후 , JsonUtility를 통해 Loader 형식으로 반환 해줍니다.
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}