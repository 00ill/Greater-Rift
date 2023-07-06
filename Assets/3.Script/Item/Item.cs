using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Helms,
    Cloak,
    Glove,
    Pants,
    Boots
}

//int��
//����, ���� ����ġ, ü��, ����, ������, ���
//float ��
//���ݼӵ�, �̵��ӵ�

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

    //���ҽ� ��θ� ������Ÿ�� ��Ʈ��+ ���� ����
    public int SpriteNum;
    public string SpritePath;

    /// <summary>
    /// �Է� ���ڴ� ������ Ÿ��, ü��, ����, ������, ���, �̵��ӵ�, ��ٿ� ����, ���ҽ� ��
    /// </summary>
    public Item(ItemType type,int level, int life, int mana, int damage, int armor, float moveSpeed, float cooldownReduction, int spriteNum)
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
        SpritePath = Enum.GetName(typeof(ItemType), Type) + SpriteNum.ToString();
    }
}
