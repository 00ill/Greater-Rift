using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentIndex
{
    HelmIndex = 101,
    CloaksIndex,
    PantsIndex,
    BootsIndex,
    WeaponIndex,
    GlovesIndex
}


public class InventoryManager
{
    public int ItemCount;
    public int ItemCountMax;

    public Dictionary<int, Item> Inventory = new();
    public int SelectedItemIndex;
    public int PointedItemIndex;

    public int HelmIndex = 101;
    public int CloakIndex = 102;
    public int PantsIndex = 103;
    public int BootsIndex = 104;
    public int WeaponIndex = 105;
    public int GlovesIndex = 106;

    public int EquipmentItemTypeCount = 6;
    public Dictionary<int, Item> Equipment = new();
    public int IndexInterval = 63;

    /// <summary>
    /// 여기 DB 연동하면 저장된 아이템 불러오는 걸로 바꿔야함
    /// </summary>
    public void Init()
    {
        ItemCount = 0;
        ItemCountMax = 35;
        Item NullItem = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
        for (int i = 0; i < ItemCountMax; i++)
        {
            Inventory.Add(i, NullItem);
        }
        for (int i = HelmIndex; i <= GlovesIndex; i++)
        {
            Equipment.Add(i, NullItem);
        }


    }

    public void ChangeItem()
    {
        (Inventory[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Inventory[PointedItemIndex]);
    }

    public void EquipItem()
    {
        switch (PointedItemIndex)
        {
            case (int)EquipmentIndex.HelmIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Helm)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                    }
                    break;
                }
            case (int)EquipmentIndex.CloaksIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Cloaks)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                    }
                    break;
                }
            case (int)EquipmentIndex.PantsIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Pants)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                    }
                    break;
                }
            case (int)EquipmentIndex.BootsIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Boots)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                    }
                    break;
                }
            case (int)EquipmentIndex.WeaponIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Weapon)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                    }
                    break;
                }
            case (int)EquipmentIndex.GlovesIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Gloves)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                    }
                    break;
                }
        }
    }
}