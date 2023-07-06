using UnityEngine;

public class ItemManager
{
    public Item GenerateItem(int level)
    {
        int type = UnityEngine.Random.Range(1, 7);
        int life = UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].LifeMin, Managers.Data.ItemStatusDataDict[level].LifeMax);
        int mana = UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].ManaMin, Managers.Data.ItemStatusDataDict[level].ManaMax);
        int damage = UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].DamageMin, Managers.Data.ItemStatusDataDict[level].DamageMax);
        int armor = UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].ArmorMin, Managers.Data.ItemStatusDataDict[level].ArmorMax);
        float moveSpeed = Mathf.Floor(UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].MoveSpeedMin, Managers.Data.ItemStatusDataDict[level].MoveSpeedMax) * 10) / 10;
        float cooldownReduction = Mathf.Floor(UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].CooldownReductionMin, Managers.Data.ItemStatusDataDict[level].CooldownReductionMax) * 10) / 10;
        int spriteNum = UnityEngine.Random.Range(0, 10);
        Item item = new((ItemType)type, level, life, mana, damage, armor, moveSpeed, cooldownReduction, spriteNum);
        return item;
    }

}