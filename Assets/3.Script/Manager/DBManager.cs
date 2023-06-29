using Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DBManager
{
    public string DBurl = "https://greater-rift-2b932-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    private readonly string _dataSlot1 = "DataSlot1";
    private readonly string _dataSlot2 = "DataSlot2";
    private readonly string _dataSlot3 = "DataSlot3";
    private List<string> _dataList = new();

    public string CurrecntUserID = string.Empty;
    public string CurrentDataSlot = string.Empty;
    public PlayerData CurrentPlayerData = null;
    public PlayerData Slot1Data = new("Empty");
    public PlayerData Slot2Data = new("Empty");
    public PlayerData Slot3Data = new("Empty");
    public void Init()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        InitDataList();
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
            if (task.IsFaulted)
            {
                Debug.Log("중복 체크 중 오류 발생");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
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
            if (task.IsFaulted)
            {
                Debug.Log("로그인 중 오류 발생");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    if (snapshot.Child("Password").Value.ToString() == password)
                    {
                        CurrecntUserID = id;
                        if (snapshot.HasChild(_dataSlot1))
                        {
                            Slot1Data.Name = snapshot.Child(_dataSlot1).Child("Name").Value.ToString();
                            Slot1Data.Level = int.Parse(snapshot.Child(_dataSlot1).Child("Level").Value.ToString());
                            Slot1Data.CurExp = int.Parse(snapshot.Child(_dataSlot1).Child("CurExp").Value.ToString());
                        }
                        if (snapshot.HasChild(_dataSlot2))
                        {
                            Slot2Data.Name = snapshot.Child(_dataSlot2).Child("Name").Value.ToString();
                            Slot2Data.Level = int.Parse(snapshot.Child(_dataSlot2).Child("Level").Value.ToString());
                            Slot2Data.CurExp = int.Parse(snapshot.Child(_dataSlot2).Child("CurExp").Value.ToString());
                        }
                        if (snapshot.HasChild(_dataSlot3))
                        {
                            Slot3Data.Name = snapshot.Child(_dataSlot3).Child("Name").Value.ToString();
                            Slot3Data.Level = int.Parse(snapshot.Child(_dataSlot3).Child("Level").Value.ToString());
                            Slot3Data.CurExp = int.Parse(snapshot.Child(_dataSlot3).Child("CurExp").Value.ToString());
                        }

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
        DatabaseReference accountRef = reference.Child("Account").Child("ID").Child(CurrecntUserID);
        accountRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("데이터 생성 중 오류 발생");
                return;
            }
            if (task.IsCompletedSuccessfully)
            {
                DataSnapshot snapshot = task.Result;
                PlayerData playerData = new(name);
                string playerDataJson = JsonUtility.ToJson(playerData);
                if (!snapshot.HasChild(_dataSlot1))
                {
                    reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot1).SetRawJsonValueAsync(playerDataJson);
                    CurrentDataSlot = _dataSlot1;
                    Managers.DB.CurrentPlayerData = Managers.DB.Slot1Data;
                    Managers.Event.DBEvent?.Invoke(Define.DB_Event.SuccessCreateNewPlayer);
                    return;
                }
                else if (!snapshot.HasChild(_dataSlot2))
                {
                    reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot2).SetRawJsonValueAsync(playerDataJson);
                    CurrentDataSlot = _dataSlot2;
                    Managers.DB.CurrentPlayerData = Managers.DB.Slot2Data;
                    Managers.Event.DBEvent?.Invoke(Define.DB_Event.SuccessCreateNewPlayer);
                    return;
                }
                else if (!snapshot.HasChild(_dataSlot3))
                {
                    reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot3).SetRawJsonValueAsync(playerDataJson);
                    CurrentDataSlot = _dataSlot3;
                    Managers.DB.CurrentPlayerData = Managers.DB.Slot3Data;
                    Managers.Event.DBEvent?.Invoke(Define.DB_Event.SuccessCreateNewPlayer);
                    return;
                }
                else
                {
                    Managers.Event.DBEvent?.Invoke(Define.DB_Event.FullDataSlot);
                }
            }
        });
    }

    public void SaveData(PlayerData playerData)
    {
        DatabaseReference dataRef = reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(CurrentDataSlot);
        dataRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("데이터 저장 중 오류");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string playerDataJson = JsonUtility.ToJson(playerData);
                reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(CurrentDataSlot).SetRawJsonValueAsync(playerDataJson);
            }
        });

    }

    public void DeletaData(int slotNum)
    {
        DatabaseReference accountDelRef = reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataList[slotNum - 1]);
        accountDelRef.RemoveValueAsync().ContinueWith(delTask =>
        {
            if (delTask.IsFaulted)
            {
                Debug.Log("데이터 삭제 중 오류 발생");
            }
            else if (delTask.IsCompleted)
            {
                DatabaseReference accountRef = reference.Child("Account").Child("ID").Child(CurrecntUserID);
                accountRef.GetValueAsync().ContinueWith(task =>
                {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.HasChild(_dataSlot1))
                    {
                        Slot1Data.Name = snapshot.Child(_dataSlot1).Child("Name").Value.ToString();
                        Slot1Data.Level = int.Parse(snapshot.Child(_dataSlot1).Child("Level").Value.ToString());
                        Slot1Data.CurExp = int.Parse(snapshot.Child(_dataSlot1).Child("CurExp").Value.ToString());
                    }
                    else
                    {
                        Slot1Data.Name = "Empty";
                    }
                    if (snapshot.HasChild(_dataSlot2))
                    {
                        Slot2Data.Name = snapshot.Child(_dataSlot2).Child("Name").Value.ToString();
                        Slot2Data.Level = int.Parse(snapshot.Child(_dataSlot2).Child("Level").Value.ToString());
                        Slot2Data.CurExp = int.Parse(snapshot.Child(_dataSlot2).Child("CurExp").Value.ToString());
                    }
                    else
                    {
                        Slot2Data.Name = "Empty";
                    }
                    if (snapshot.HasChild(_dataSlot3))
                    {
                        Slot3Data.Name = snapshot.Child(_dataSlot3).Child("Name").Value.ToString();
                        Slot3Data.Level = int.Parse(snapshot.Child(_dataSlot3).Child("Level").Value.ToString());
                        Slot3Data.CurExp = int.Parse(snapshot.Child(_dataSlot3).Child("CurExp").Value.ToString());
                    }
                    else
                    {
                        Slot3Data.Name = "Empty";
                    }
                    Managers.Event.DBEvent(Define.DB_Event.UpdateLoadData);
                });

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
        public int CurExp;

        public PlayerData(string name, int level = 1, int curExp = 0)
        {
            Name = name;
            Level = level;
            CurExp = curExp;
        }
    }
}
