using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    public int ItemCount = 0;
    public int ItemCountMax = 35;

    public Dictionary<int, Item> Inventory = new();

    public void Init()
    {
        for(int i = 0 ; i < 35; i++)
        {
            Item NullItem = new(ItemType.Null, 0, 0, 0, 0, 0, 0, 0, 0);
            Inventory.Add(i, NullItem);
        }
    }

}