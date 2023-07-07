using System.Collections.Generic;

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

    public Item ItemTotal;

    /// <summary>
    /// 여기 DB 연동하면 저장된 아이템 불러오는 걸로 바꿔야함
    /// </summary>
    public void Init()
    {
        ItemCount = 0;
        ItemCountMax = 35;
        ItemTotal = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
        Item NullItem = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
        for (int i = 0; i < ItemCountMax; i++)
        {
            Inventory.Add(i, NullItem);
            //Inventory = Managers.DB.CurrentInventoryData;
            //Inventory.Add(i, Managers.DB.CurrentInventoryData[i]);
        }
        for (int i = HelmIndex; i <= GlovesIndex; i++)
        {
            Equipment.Add(i, NullItem);
        }
        CalcItemTotal();

    }

    /// <summary>
    /// 인벤토리 창안에서 서로 바꾸는 메서드
    /// </summary>
    public void ChangeItem()
    {
        (Inventory[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Inventory[PointedItemIndex]);
    }

    /// <summary>
    /// 아이템 장착 메서드
    /// </summary>
    public void EquipItem()
    {
        switch (PointedItemIndex)
        {
            case (int)EquipmentIndex.HelmIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Helm)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                        ItemCount--;
                    }
                    break;
                }
            case (int)EquipmentIndex.CloaksIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Cloaks)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                        ItemCount--;
                    }
                    break;
                }
            case (int)EquipmentIndex.PantsIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Pants)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                        ItemCount--;
                    }
                    break;
                }
            case (int)EquipmentIndex.BootsIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Boots)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                        ItemCount--;
                    }
                    break;
                }
            case (int)EquipmentIndex.WeaponIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Weapon)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                        ItemCount--;
                    }
                    break;
                }
            case (int)EquipmentIndex.GlovesIndex:
                {
                    if (Inventory[SelectedItemIndex].Type == ItemType.Gloves)
                    {
                        (Equipment[PointedItemIndex], Inventory[SelectedItemIndex]) = (Inventory[SelectedItemIndex], Equipment[PointedItemIndex]);
                        ItemCount--;
                    }
                    break;
                }
        }
        CalcItemTotal();
        Managers.Event.PostNotification(Define.EVENT_TYPE.ChangeStatus, null);
    }

    /// <summary>
    /// 아이템 해제 메서드
    /// </summary>
    public void Disarm()
    {
        (Equipment[SelectedItemIndex], Inventory[PointedItemIndex]) = (Inventory[PointedItemIndex], Equipment[SelectedItemIndex]);
        ItemCount++;
        CalcItemTotal();
        Managers.Event.PostNotification(Define.EVENT_TYPE.ChangeStatus, null);
    }

    public void CalcItemTotal()
    {
        Item newItem = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
        ItemTotal = newItem;
        for (int i = (int)EquipmentIndex.HelmIndex; i <= (int)EquipmentIndex.GlovesIndex; i++)
        {
            int temp = i;
            ItemTotal.Life += Equipment[temp].Life;
            ItemTotal.Mana += Equipment[temp].Mana;
            ItemTotal.Damage += Equipment[temp].Damage;
            ItemTotal.Armor += Equipment[temp].Armor;
            ItemTotal.MoveSpeed += Equipment[temp].MoveSpeed;
            ItemTotal.CooldownReduction += Equipment[temp].CooldownReduction;
        }
    }
}