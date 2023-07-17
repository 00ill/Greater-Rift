using UnityEngine;

public class ItemManager
{
    public int LegendaryProb = 1;
    public int RareProb = 9;
    public int MagicProb = 30;
    public int GradeCoefficient;
    public string ItemBox;
    public void GenerateItem(int level, Vector3 position)
    {
        ItemBox = CheckItemGrade();
        int type = UnityEngine.Random.Range(1, 7);
        int life = UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].LifeMin, Managers.Data.ItemStatusDataDict[level].LifeMax * GradeCoefficient);
        int mana = UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].ManaMin, Managers.Data.ItemStatusDataDict[level].ManaMax * GradeCoefficient);
        int damage = UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].DamageMin, Managers.Data.ItemStatusDataDict[level].DamageMax * GradeCoefficient);
        int armor = UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].ArmorMin, Managers.Data.ItemStatusDataDict[level].ArmorMax * GradeCoefficient);
        float moveSpeed = Mathf.Floor(UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].MoveSpeedMin, Managers.Data.ItemStatusDataDict[level].MoveSpeedMax) * 10) / 10;
        float cooldownReduction = Mathf.Floor(UnityEngine.Random.Range(Managers.Data.ItemStatusDataDict[level].CooldownReductionMin, Managers.Data.ItemStatusDataDict[level].CooldownReductionMax) * 10) / 10;
        int spriteNum = UnityEngine.Random.Range(1, 9);
        Item item = new((ItemType)type, level, life, mana, damage, armor, moveSpeed, cooldownReduction, spriteNum);


        GameObject itemBox = Managers.Resource.Instantiate(ItemBox);
        itemBox.transform.position = position;
        itemBox.GetComponent<ItemBox>().ItemData = item;
    }

    private string CheckItemGrade()
    {
        int randNum = Random.Range(1, 101);
        if (randNum <= LegendaryProb)
        {
            GradeCoefficient = 4;
            return "LegendaryItemBox";
        }
        else if (randNum <= RareProb)
        {
            GradeCoefficient = 3;
            return "RareItemBox";
        }
        else if (randNum <= MagicProb)
        {
            GradeCoefficient = 2;
            return "MagicItemBox";
        }
        else
        {
            GradeCoefficient = 1;
            return "CommonItemBox";
        }
    }
}