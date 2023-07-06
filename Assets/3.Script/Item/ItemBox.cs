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
        Debug.Log("������ ����");
        if (Managers.Inventory.ItemCount == Managers.Inventory.ItemCountMax)
        {
            Debug.Log("�κ��丮 �� ��");
            Managers.Event.PostNotification(Define.EVENT_TYPE.FullInventory, this);
            return;
        }

        Util.PrintText(string.Format($"�پ����� ���� ������ ����Ÿ {ItemData.Life}"));

        Managers.Inventory.ItemCount++;
        for (int i = 0; i < 35; i++)
        {
            if (Managers.Inventory.Inventory[i] == null)
            {
                Debug.Log("������ �־���");
                Managers.Inventory.Inventory[i] = ItemData;
                Debug.Log(string.Format($"���� �������� ü�� : {ItemData.Life}"));
                Debug.Log(string.Format($"�Ŵ����� ���� : {Managers.Inventory.Inventory[i].Life}"));
                break;
            }

        }

        Debug.Log("������ ���ش�.");
        Managers.Resource.Destroy(gameObject);

    }
}
