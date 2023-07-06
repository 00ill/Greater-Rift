using System;
using UnityEngine;

public enum ItemType
{
    Null,
    Weapon,
    Helms,
    Cloaks,
    Gloves,
    Pants,
    Boots
}

//int값
//레벨, 현재 경험치, 체력, 마나, 데미지, 방어
//float 값
//공격속도, 이동속도

public class Item : MonoBehaviour
{
    public ItemType Type;
    public int Level;
    public int Life;
    public int Mana;
    public int Damage;
    public int Armor;
    public float MoveSpeed;
    public float CooldownReduction;

    //리소스 경로를 아이템타입 스트링+ 랜덤 숫자
    private readonly string _itemPath = "Images/Item/";
    public int SpriteNum;
    public string SpritePath;

    /// <summary>
    /// 입력 인자는 아이템 타입, 체력, 마나, 데미지, 방어, 이동속도, 쿨다운 감소, 리소스 순
    /// </summary>
    public Item(ItemType type, int level, int life, int mana, int damage, int armor, float moveSpeed, float cooldownReduction, int spriteNum)
    {
        Type = type;
        Level = level;
        Life = life;
        Mana = mana;
        Damage = damage;
        Armor = armor;
        MoveSpeed = moveSpeed;
        CooldownReduction = cooldownReduction;
        SpriteNum = spriteNum;
        SpritePath = _itemPath + Enum.GetName(typeof(ItemType), Type) + SpriteNum.ToString();
    }
}
