using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<string, Data.PlayerSaveData> PlayerSaveDataDict { get; private set; } = new Dictionary<string, Data.PlayerSaveData>();
    public Dictionary<int, Data.PlayerStatus> PlayerStatusDataDict { get; private set; } = new Dictionary<int, Data.PlayerStatus>();
    public void Init()
    {

        Debug.Log("데이터 매니저 초기화");
        //PlayerSaveDataDict = LoadJson<Data.PlayerSaveDataLoader, string, Data.PlayerSaveData>("PlayerSaveData").MakeDict();
        PlayerStatusDataDict = LoadJson<Data.PlayerStatusLoader, int, Data.PlayerStatus>("PlayerStatusData").MakeDict();
        //Debug.Log(PlayerStatusDataDict[30].Life);
        Debug.Log("데이터 매니저 초기화 끝");
    }   


    //지정해둔 Loader형식을 받는 LoadJSon 함수 
    // Loader,와 Key,Value를 받은 후 , JsonUtility를 통해 Loader 형식으로 반환 해줍니다.
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}