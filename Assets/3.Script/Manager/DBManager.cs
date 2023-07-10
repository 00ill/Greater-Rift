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
    public PlayerSkillData CurrentPlayerSkillData = null;
    public PlayerSkillData Slot1SkillData = new();
    public PlayerSkillData Slot2SkillData = new();
    public PlayerSkillData Slot3SkillData = new();
    public Dictionary<int, Item> Slot1InventoryData = new();
    public Dictionary<int, Item> Slot2InventoryData = new();
    public Dictionary<int, Item> Slot3InventoryData = new();
    public Dictionary<int, Item> Slot1EquipmantData = new();
    public Dictionary<int, Item> Slot2EquipmantData = new();
    public Dictionary<int, Item> Slot3EquipmantData = new();

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
                            LoadData(Slot1Data, Slot1SkillData, Slot1InventoryData, Slot1EquipmantData, snapshot, _dataSlot1);
                        }
                        if (snapshot.HasChild(_dataSlot2))
                        {
                            LoadData(Slot2Data, Slot2SkillData, Slot2InventoryData, Slot2EquipmantData, snapshot, _dataSlot2);
                        }
                        if (snapshot.HasChild(_dataSlot3))
                        {
                            LoadData(Slot3Data, Slot3SkillData, Slot3InventoryData, Slot3EquipmantData, snapshot, _dataSlot3);
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
                PlayerSkillData playerSkillData = new();
                string playerSkillDataJson = JsonUtility.ToJson(playerSkillData);

                //새로운 스킬데이터와 인벤토리, 장착들을 할당해 줘야함

                Managers.Skill.CurrentM1SKillName = playerSkillData.M1SkillName;
                Managers.Skill.CurrentM2SKillName = playerSkillData.M2SkillName;
                Managers.Skill.CurrentNum1SKillName = playerSkillData.Num1SkillName;
                Managers.Skill.CurrentNum2SKillName = playerSkillData.Num2SkillName;
                Managers.Skill.CurrentNum4SKillName = playerSkillData.Num4SkillName;
                Managers.Skill.CurrentNum3SKillName = playerSkillData.Num3SkillName;

                Managers.Inventory.Inventory.Clear();
                Managers.Inventory.Equipment.Clear();

                Item NullItemForInit = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);

                for (int i = 0; i < Managers.Inventory.ItemCountMax; i++)
                {
                    Managers.Inventory.Inventory.Add(i, NullItemForInit);
                }
                for (int i = Managers.Inventory.HelmIndex; i <= Managers.Inventory.GlovesIndex; i++)
                {
                    Managers.Inventory.Equipment.Add(i, NullItemForInit);
                }


                if (!snapshot.HasChild(_dataSlot1))
                {
                    reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot1).SetRawJsonValueAsync(playerDataJson);
                    reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot1).Child("SkillSet").SetRawJsonValueAsync(playerSkillDataJson);
                    for (int i = 0; i < Managers.Inventory.Inventory.Count; i++)
                    {
                        Item NullItem = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
                        string itemDataJson = JsonUtility.ToJson(NullItem);
                        reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot1).Child("Inventory").Child("ItemSlot" + i.ToString()).SetRawJsonValueAsync(itemDataJson);
                    }
                    for (int i = (int)EquipmentIndex.HelmIndex; i <= (int)EquipmentIndex.GlovesIndex; i++)
                    {
                        Item NullItem = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
                        string itemDataJson = JsonUtility.ToJson(NullItem);
                        reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot1).Child("Equipment").Child("EquipSlot" + i.ToString()).SetRawJsonValueAsync(itemDataJson);
                    }
                    CurrentDataSlot = _dataSlot1;
                    Slot1Data.Name = name;
                    Managers.DB.CurrentPlayerData = Managers.DB.Slot1Data;
                    Managers.Event.DBEvent?.Invoke(Define.DB_Event.SuccessCreateNewPlayer);
                    return;
                }
                else if (!snapshot.HasChild(_dataSlot2))
                {
                    reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot2).SetRawJsonValueAsync(playerDataJson);
                    reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot2).Child("SkillSet").SetRawJsonValueAsync(playerSkillDataJson);
                    for (int i = 0; i < Managers.Inventory.Inventory.Count; i++)
                    {
                        Item NullItem = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
                        string itemDataJson = JsonUtility.ToJson(NullItem);
                        reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot2).Child("Inventory").Child("ItemSlot" + i.ToString()).SetRawJsonValueAsync(itemDataJson);
                    }
                    for (int i = (int)EquipmentIndex.HelmIndex; i <= (int)EquipmentIndex.GlovesIndex; i++)
                    {
                        Item NullItem = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
                        string itemDataJson = JsonUtility.ToJson(NullItem);
                        reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot2).Child("Equipment").Child("EquipSlot" + i.ToString()).SetRawJsonValueAsync(itemDataJson);
                    }
                    CurrentDataSlot = _dataSlot2;
                    Slot2Data.Name = name;
                    Managers.DB.CurrentPlayerData = Managers.DB.Slot2Data;
                    Managers.Event.DBEvent?.Invoke(Define.DB_Event.SuccessCreateNewPlayer);
                    return;
                }
                else if (!snapshot.HasChild(_dataSlot3))
                {
                    reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot3).SetRawJsonValueAsync(playerDataJson);
                    reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot3).Child("SkillSet").SetRawJsonValueAsync(playerSkillDataJson);
                    for (int i = 0; i < Managers.Inventory.Inventory.Count; i++)
                    {
                        Item NullItem = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
                        string itemDataJson = JsonUtility.ToJson(NullItem);
                        reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot3).Child("Inventory").Child("ItemSlot" + i.ToString()).SetRawJsonValueAsync(itemDataJson);
                    }
                    for (int i = (int)EquipmentIndex.HelmIndex; i <= (int)EquipmentIndex.GlovesIndex; i++)
                    {
                        Item NullItem = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
                        string itemDataJson = JsonUtility.ToJson(NullItem);
                        reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(_dataSlot3).Child("Equipment").Child("EquipSlot" + i.ToString()).SetRawJsonValueAsync(itemDataJson);
                    }
                    CurrentDataSlot = _dataSlot3;
                    Slot3Data.Name = name;
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
                PlayerSkillData playerSkillData = new(Managers.Skill.CurrentM1SKillName, Managers.Skill.CurrentM2SKillName,
                    Managers.Skill.CurrentNum1SKillName, Managers.Skill.CurrentNum2SKillName, Managers.Skill.CurrentNum3SKillName, Managers.Skill.CurrentNum4SKillName);
                string playerSkillDataJson = JsonUtility.ToJson(playerSkillData);
                reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(CurrentDataSlot).Child("SkillSet").SetRawJsonValueAsync(playerSkillDataJson);

                for (int i = 0; i < Managers.Inventory.Inventory.Count; i++)
                {
                    string itemDataJson = JsonUtility.ToJson(Managers.Inventory.Inventory[i]);
                    reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(CurrentDataSlot).Child("Inventory").Child("ItemSlot" + i.ToString()).SetRawJsonValueAsync(itemDataJson);
                }
                for (int i = (int)EquipmentIndex.HelmIndex; i <= (int)EquipmentIndex.GlovesIndex; i++)
                {
                    string itemDataJson = JsonUtility.ToJson(Managers.Inventory.Equipment[i]);
                    reference.Child("Account").Child("ID").Child(CurrecntUserID).Child(CurrentDataSlot).Child("Equipment").Child("EquipSlot" + i.ToString()).SetRawJsonValueAsync(itemDataJson);
                }


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

    private void LoadData(PlayerData playerData, PlayerSkillData playerSkillData, Dictionary<int, Item> inventoryData, Dictionary<int, Item> equipmentData, DataSnapshot snapshot, string dataSlot)
    {
        inventoryData.Clear();
        equipmentData.Clear();
        playerData.Name = snapshot.Child(dataSlot).Child("Name").Value.ToString();
        playerData.Level = int.Parse(snapshot.Child(dataSlot).Child("Level").Value.ToString());
        playerData.CurExp = int.Parse(snapshot.Child(dataSlot).Child("CurExp").Value.ToString());
        playerSkillData.M1SkillName = (SkillName)int.Parse(snapshot.Child(dataSlot).Child("SkillSet").Child("M1SkillName").Value.ToString());
        playerSkillData.M2SkillName = (SkillName)int.Parse(snapshot.Child(dataSlot).Child("SkillSet").Child("M2SkillName").Value.ToString());
        playerSkillData.Num1SkillName = (SkillName)int.Parse(snapshot.Child(dataSlot).Child("SkillSet").Child("Num1SkillName").Value.ToString());
        playerSkillData.Num2SkillName = (SkillName)int.Parse(snapshot.Child(dataSlot).Child("SkillSet").Child("Num2SkillName").Value.ToString());
        playerSkillData.Num3SkillName = (SkillName)int.Parse(snapshot.Child(dataSlot).Child("SkillSet").Child("Num3SkillName").Value.ToString());
        playerSkillData.Num4SkillName = (SkillName)int.Parse(snapshot.Child(dataSlot).Child("SkillSet").Child("Num4SkillName").Value.ToString());

        for (int i = 0; i < 35; i++)
        {
            Item temp = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
            Item item = temp;
            item.Type = (ItemType)int.Parse(snapshot.Child(dataSlot).Child("Inventory").Child("ItemSlot" + i.ToString()).Child("Type").Value.ToString());
            item.Level = int.Parse(snapshot.Child(dataSlot).Child("Inventory").Child("ItemSlot" + i.ToString()).Child("Level").Value.ToString());
            item.Life = int.Parse(snapshot.Child(dataSlot).Child("Inventory").Child("ItemSlot" + i.ToString()).Child("Life").Value.ToString());
            item.Mana = int.Parse(snapshot.Child(dataSlot).Child("Inventory").Child("ItemSlot" + i.ToString()).Child("Mana").Value.ToString());
            item.Damage = int.Parse(snapshot.Child(dataSlot).Child("Inventory").Child("ItemSlot" + i.ToString()).Child("Damage").Value.ToString());
            item.Armor = int.Parse(snapshot.Child(dataSlot).Child("Inventory").Child("ItemSlot" + i.ToString()).Child("Armor").Value.ToString());
            item.MoveSpeed = float.Parse(snapshot.Child(dataSlot).Child("Inventory").Child("ItemSlot" + i.ToString()).Child("MoveSpeed").Value.ToString());
            item.CooldownReduction = float.Parse(snapshot.Child(dataSlot).Child("Inventory").Child("ItemSlot" + i.ToString()).Child("CooldownReduction").Value.ToString());
            item.SpriteNum = int.Parse(snapshot.Child(dataSlot).Child("Inventory").Child("ItemSlot" + i.ToString()).Child("SpriteNum").Value.ToString());
            item.SpritePath = snapshot.Child(dataSlot).Child("Inventory").Child("ItemSlot" + i.ToString()).Child("SpritePath").Value.ToString();
            inventoryData.Add(i, item);
        }
        for (int i = 101; i <= 106; i++)
        {
            Item temp = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
            Item item = temp;
            item.Type = (ItemType)int.Parse(snapshot.Child(dataSlot).Child("Equipment").Child("EquipSlot" + i.ToString()).Child("Type").Value.ToString());
            item.Level = int.Parse(snapshot.Child(dataSlot).Child("Equipment").Child("EquipSlot" + i.ToString()).Child("Level").Value.ToString());
            item.Life = int.Parse(snapshot.Child(dataSlot).Child("Equipment").Child("EquipSlot" + i.ToString()).Child("Life").Value.ToString());
            item.Mana = int.Parse(snapshot.Child(dataSlot).Child("Equipment").Child("EquipSlot" + i.ToString()).Child("Mana").Value.ToString());
            item.Damage = int.Parse(snapshot.Child(dataSlot).Child("Equipment").Child("EquipSlot" + i.ToString()).Child("Damage").Value.ToString());
            item.Armor = int.Parse(snapshot.Child(dataSlot).Child("Equipment").Child("EquipSlot" + i.ToString()).Child("Armor").Value.ToString());
            item.MoveSpeed = float.Parse(snapshot.Child(dataSlot).Child("Equipment").Child("EquipSlot" + i.ToString()).Child("MoveSpeed").Value.ToString());
            item.CooldownReduction = float.Parse(snapshot.Child(dataSlot).Child("Equipment").Child("EquipSlot" + i.ToString()).Child("CooldownReduction").Value.ToString());
            item.SpriteNum = int.Parse(snapshot.Child(dataSlot).Child("Equipment").Child("EquipSlot" + i.ToString()).Child("SpriteNum").Value.ToString());
            item.SpritePath = snapshot.Child(dataSlot).Child("Equipment").Child("EquipSlot" + i.ToString()).Child("SpritePath").Value.ToString();
            equipmentData.Add(i, item);
        }
    }

    public void UpdateSkillData()
    {
        Managers.Skill.CurrentM1SKillName = CurrentPlayerSkillData.M1SkillName;
        Managers.Skill.CurrentM2SKillName = CurrentPlayerSkillData.M2SkillName;
        Managers.Skill.CurrentNum1SKillName = CurrentPlayerSkillData.Num1SkillName;
        Managers.Skill.CurrentNum2SKillName = CurrentPlayerSkillData.Num2SkillName;
        Managers.Skill.CurrentNum3SKillName = CurrentPlayerSkillData.Num3SkillName;
        Managers.Skill.CurrentNum4SKillName = CurrentPlayerSkillData.Num4SkillName;
    }

    public void ResetLoadData()
    {
        Slot1Data = new("Empty");
        Slot2Data = new("Empty");
        Slot3Data = new("Empty");
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

    public class PlayerSkillData
    {
        public SkillName M1SkillName;
        public SkillName M2SkillName;
        public SkillName Num1SkillName;
        public SkillName Num2SkillName;
        public SkillName Num3SkillName;
        public SkillName Num4SkillName;
        public PlayerSkillData(SkillName m1SkillName = SkillName.ShadowSlash, SkillName m2SkillName = SkillName.None,
            SkillName num1SkillName = SkillName.None, SkillName num2SkillName = SkillName.None, SkillName num3SkillName = SkillName.None, SkillName num4SkillName = SkillName.None)
        {
            M1SkillName = m1SkillName;
            M2SkillName = m2SkillName;
            Num1SkillName = num1SkillName;
            Num2SkillName = num2SkillName;
            Num3SkillName = num3SkillName;
            Num4SkillName = num4SkillName;
        }
    }

}
