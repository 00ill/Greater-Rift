using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DBManager
{
    public string DBurl = "https://greater-rift-2b932-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    private string _dataSlot1 = "DataSlot1";
    private string _dataSlot2 = "DataSlot2";
    private string _dataSlot3 = "DataSlot3";
    private List<string> _dataList = new();
    public void Init()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        InitDataList();
        LoadData();
    }

    private void InitDataList()
    {
        _dataList.Add(_dataSlot1);
        _dataList.Add(_dataSlot2);
        _dataList.Add(_dataSlot3);
    }

    public void CreateAccount(string id, string password)
    {
        DatabaseReference duplicateRef = reference.Child("Account").Child("ID").Child(id);
        duplicateRef.GetValueAsync().ContinueWith(task =>
        {
            if(task.IsFaulted)
            {
                Debug.Log("중복 체크 중 오류 발생");
            }
            else if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if(snapshot.Exists)
                {
                    Debug.Log("중복 발생");
                    Managers.Event.DBEvent?.Invoke(Define.DB_Event.DuplicateID);
                    return;
                }
                else
                {
                    DatabaseReference accountRef = reference.Child("Account");
                    accountRef.GetValueAsync().ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.Log("계정 생성 중 오류 발생");
                        }
                        else if (task.IsCompleted)
                        {
                            //AccountData accountData = new(id, password);
                            //string accountDatajson = JsonUtility.ToJson(accountData);
                            //아이디의 키값이 패스워드
                            reference.Child("Account").Child("ID").Child(id).Child("Password").SetValueAsync(password);
                            Managers.Event.DBEvent?.Invoke(Define.DB_Event.SuccessCreateNewAccount);
                        }
                    });
                }
            }
        });
    }

    public void Login(string id, string password) 
    {
        DatabaseReference accountRef = reference.Child("Account").Child("ID").Child(id);
        accountRef.GetValueAsync().ContinueWith(task =>
        {
            if(task.IsFaulted)
            {
                Debug.Log("로그인 중 오류 발생");
            }
            else if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if(snapshot.Exists)
                {
                    if(snapshot.Child("Password").Value.ToString() == password)
                    {
                        Managers.Event.DBEvent?.Invoke(Define.DB_Event.SuccessLogin);
                    }
                    else
                    {
                        Managers.Event.DBEvent?.Invoke(Define.DB_Event.WrongPassword);
                    }
                }
                else
                {
                    Managers.Event.DBEvent?.Invoke(Define.DB_Event.NonExistID);
                }
            }
        });
    }


    public void CreatePlayerData(string name)
    {
        DatabaseReference dataRef = reference.Child("PlayerData");
        dataRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("데이터 생성 중 오류 발생");
                return;
            }
            if (task.IsCompletedSuccessfully)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Managers.Event.DBEvent?.Invoke(Define.DB_Event.DuplicateNickname);
                }
                else
                {
                    Managers.Event.DBEvent?.Invoke(Define.DB_Event.SuccessCreateNewPlayer);
                    if (CountData() < 3)
                    {
                        PlayerData playerData = new(name);
                        string playerDataJson = JsonUtility.ToJson(playerData);
                        reference.Child("PlayerData").Child(_dataList[CountData()]).SetRawJsonValueAsync(playerDataJson);
                    }
                }
            }
        });
    }

    private void LoadData()
    {
        FirebaseDatabase.DefaultInstance.GetReference("PlayerData").GetValueAsync().ContinueWithOnMainThread
            (task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Error");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    Debug.Log(snapshot.Child("Youngil").Child("Level").Value.ToString());
                    Debug.Log(snapshot.Child("Youngil").Child("CurExp").Value.ToString());
                }
            });
    }
    private bool CheckDuplicateName(string name)
    {
        bool isDuplicate = false;
        FirebaseDatabase.DefaultInstance.GetReference("PlayerData").GetValueAsync().ContinueWithOnMainThread
            (task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Error");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    if (name == snapshot.Child(_dataSlot1).Child("Name").Value.ToString())
                    {
                        isDuplicate = true;
                    }

                    for(int i = 0; i<snapshot.ChildrenCount; i++)
                    {

                    }
                }
            });
        return isDuplicate;
    }

    private int CountData()
    {
        int dataCount = 0;
        FirebaseDatabase.DefaultInstance.GetReference("PlayerData").GetValueAsync().ContinueWithOnMainThread
       (task =>
       {
           if (task.IsFaulted)
           {
               Debug.Log("Error");
           }
           else if (task.IsCompleted)
           {
               DataSnapshot snapshot = task.Result;
               dataCount = (int)snapshot.ChildrenCount;
           }
       });
        return dataCount;
    }
    /// <summary>
    /// 추가하는 법
    /// </summary>
    public void WriteDB()
    {
        //PlayerData test = new PlayerData(10, 50);
        //string jsontest = JsonUtility.ToJson(test);
        //reference.Child("PlayerData").Child("Youngil").SetRawJsonValueAsync(jsontest);
    }

    /// <summary>
    /// 삭제하는 법
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
    /// 불러오는 법
    /// </summary>
    public void PrintDB()
    {
        DatabaseReference playerRef = reference.Child("PlayerData");
        playerRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted) { Debug.Log("로드를 못했ㄷㅇ"); }
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string test = snapshot.Child("Youngil").Child("Level").Value.ToString();
                Debug.Log(test);
            }
        });
    }

    public class AccountData
    {
        public string ID;
        public string Password;

        public AccountData(string iD, string password)
        {
            ID = iD;
            Password = password;
        }
    }
    public class PlayerData
    {
        public string Name;
        public int Level;
        public float CurExp;

        public PlayerData(string name, int level = 1, float curExp = 0f)
        {
            Name = name;
            Level = level;
            CurExp = curExp;
        }
    }
}
