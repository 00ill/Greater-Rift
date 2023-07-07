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
        if (Managers.Inventory.ItemCount == Managers.Inventory.ItemCountMax)
        {
            Managers.Event.PostNotification(Define.EVENT_TYPE.FullInventory, this);
            return;
        }

        Managers.Inventory.ItemCount++;

        for (int i = 0; i < Managers.Inventory.ItemCountMax; i++)
        {
            if (Managers.Inventory.Inventory[i].Type == ItemType.Null)
            {
                Debug.Log("아이템 딕셔너리에 집어넣기");
                Managers.Inventory.Inventory[i] = ItemData;
                break;
            }
        }
        Managers.Resource.Destroy(gameObject);
    }
}
