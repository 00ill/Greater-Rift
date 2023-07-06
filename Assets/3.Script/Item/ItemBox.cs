using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InteractableObject))]
[RequireComponent(typeof(Collider))]

public class ItemBox : MonoBehaviour
{

    private InteractableObject _interactableObject;
    public Item ItemData;

    private void Awake()
    {
        TryGetComponent(out _interactableObject);
    }
    private void OnEnable()
    {
        _interactableObject.AddInteract(GetItem);
    }

    private void GetItem()
    {
        Debug.Log("아이템 먹음");
        if (Managers.Inventory.ItemCount == Managers.Inventory.ItemCountMax)
        {
            Debug.Log("인벤토리 꽉 참");
            Managers.Event.PostNotification(Define.EVENT_TYPE.FullInventory, this);
            return;
        }

        Util.PrintText(string.Format($"겟아이템 안의 아이템 데이타 {ItemData.Life}"));

        Managers.Inventory.ItemCount++;
        for (int i = 0; i < 35; i++)
        {
            if (Managers.Inventory.Inventory[i] == null)
            {
                Debug.Log("아이템 넣었다");
                Managers.Inventory.Inventory[i] = ItemData;
                Debug.Log(string.Format($"먹은 아이템의 체력 : {ItemData.Life}"));
                Debug.Log(string.Format($"매니저에 들어갔나 : {Managers.Inventory.Inventory[i].Life}"));
                break;
            }

        }

        Debug.Log("아이템 없앤다.");
        Managers.Resource.Destroy(gameObject);

    }
}
