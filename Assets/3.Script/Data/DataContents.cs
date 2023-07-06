using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class PlayerSaveData
    {
        public string Name;
        public int Level;
        public float CurExp;
    }
    [Serializable]
    public class PlayerSaveDataLoader : ILoader<string, PlayerSaveData>
    {
        public List<PlayerSaveData> PlayerSaveDataList = new();

        public Dictionary<string, PlayerSaveData> MakeDict()
        {
            Dictionary<string, PlayerSaveData> dict = new();
            foreach (PlayerSaveData data in PlayerSaveDataList)
            {
                dict.Add(data.Name, data);
            }
            return dict;
        }
    }

    [Serializable]
    public class PlayerStatus
    {
        public int Level;
        public int Life;
        public int Mana;
        public int Damage;
        public int Armor;
        public int RequireExp;
    }
    [Serializable]
    public class PlayerStatusLoader : ILoader<int, Data.PlayerStatus>
    {
        public List<PlayerStatus> PlayerStatusData = new();

        public Dictionary<int, PlayerStatus> MakeDict()
        {
            Dictionary<int, PlayerStatus> dict = new();
            foreach (PlayerStatus data in PlayerStatusData)
            {
                dict.Add(data.Level, data);
            }
            return dict;
        }
    }
    [Serializable]
    public class ItemStatus
    {
        public int ItemLevel;
        public int LifeMin;
        public int LifeMax;
        public int ManaMin;
        public int ManaMax;
        public int DamageMin;
        public int DamageMax;
        public int ArmorMin;
        public int ArmorMax;
        public float MoveSpeedMin;
        public float MoveSpeedMax;
        public float CooldownReductionMin;
        public float CooldownReductionMax;
    }
    [Serializable]
    public class ItemStatusLoader : ILoader<int, Data.ItemStatus>
    {
        public List<ItemStatus> ItemStatusData = new();
        public Dictionary<int, ItemStatus> MakeDict()
        {
            Dictionary<int, ItemStatus> dict = new();
            foreach (ItemStatus data in ItemStatusData)
            {
                dict.Add(data.ItemLevel, data);
            }
            return dict;
        }
    }

}
