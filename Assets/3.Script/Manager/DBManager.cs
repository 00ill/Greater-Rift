using Firebase;
using Firebase.Database;
using System;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public string DBurl = "https://greater-rift-2b932-default-rtdb.firebaseio.com/";
    DatabaseReference reference;
    public void Init()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        //WriteDB();
        //DeleteDB();
        //PrintDB();
    }


    public void CreatePlayerData(string name)
    {
        DatabaseReference dataRef = reference.Child("PlayerData").Child(name);
        dataRef.GetValueAsync().ContinueWith(tast =>
        {
            if (tast.IsFaulted)
            {
                Debug.Log("������ ���� �� ���� �߻�");
                return;
            }
            if (tast.IsCompleted)
            {
                DataSnapshot snapshot = tast.Result;
                if (snapshot.Exists)
                {
                    Managers.Event.PostNotification(Define.EVENT_TYPE.DuplicateNickname, this);
                }
                else
                {
                    PlayerData playerData = new(1, 0f);
                    string playerDataJson = JsonUtility.ToJson(playerData);
                    reference.Child("PlayerData").Child(name).SetRawJsonValueAsync(playerDataJson);
                    Managers.Event.PostNotification(Define.EVENT_TYPE.SuccessCreateNewPlayer, this);
                }
            }
        });
    }

    /// <summary>
    /// �߰��ϴ� ��
    /// </summary>
    public void WriteDB()
    {
        PlayerData test = new PlayerData(10, 50);
        string jsontest = JsonUtility.ToJson(test);
        reference.Child("PlayerData").Child("Youngil").SetRawJsonValueAsync(jsontest);
    }

    /// <summary>
    /// �����ϴ� ��
    /// </summary>
    public void DeleteDB()
    {
        DatabaseReference playerRef = reference.Child("PlayerData");
        playerRef.RemoveValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                return;
            }
            if (task.IsCompleted)
            {
            }

        });
    }
    /// <summary>
    /// �ҷ����� ��
    /// </summary>
    public void PrintDB()
    {
        DatabaseReference playerRef = reference.Child("PlayerData");
        playerRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted) { Debug.Log("�ε带 ���ߤ���"); }
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string test = snapshot.Child("Youngil").Child("Level").Value.ToString();
                Debug.Log(test);
            }
        });
    }
    public class PlayerData
    {
        public int Level = 1;
        public float CurExp = 0;

        public PlayerData(int level, float curExp)
        {
            Level = level;
            CurExp = curExp;
        }
    }
}
