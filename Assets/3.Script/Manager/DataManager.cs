using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static DBManager;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public void Init()
    {
    }


    //�����ص� Loader������ �޴� LoadJSon �Լ� 
    // Loader,�� Key,Value�� ���� �� , JsonUtility�� ���� Loader �������� ��ȯ ���ݴϴ�.
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}