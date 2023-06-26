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

        Debug.Log("������ �Ŵ��� �ʱ�ȭ");
        //PlayerSaveDataDict = LoadJson<Data.PlayerSaveDataLoader, string, Data.PlayerSaveData>("PlayerSaveData").MakeDict();
        PlayerStatusDataDict = LoadJson<Data.PlayerStatusLoader, int, Data.PlayerStatus>("PlayerStatusData").MakeDict();
        //Debug.Log(PlayerStatusDataDict[30].Life);
        Debug.Log("������ �Ŵ��� �ʱ�ȭ ��");
    }


    //�����ص� Loader������ �޴� LoadJSon �Լ� 
    // Loader,�� Key,Value�� ���� �� , JsonUtility�� ���� Loader �������� ��ȯ ���ݴϴ�.
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}