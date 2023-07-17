using System;

public enum ItemType
{
    Null,
    Weapon,
    Helm,
    Cloaks,
    Gloves,
    Pants,
    Boots
}

public class Item
{
    public ItemType Type;
    public int Level;
    public int Life;
    public int Mana;
    public int Damage;
    public int Armor;
    public float MoveSpeed;
    public float CooldownReduction;

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
