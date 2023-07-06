using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : UI_Popup,IListener
{
    enum Buttons
    {
        Quit
    }
    enum Texts
    {
    }
    enum Images
    {
        ItemImage1,
        ItemImage2,
        ItemImage3,
        ItemImage4,
        ItemImage5,
        ItemImage6,
        ItemImage7,
        ItemImage8,
        ItemImage9,
        ItemImage10,
        ItemImage11,
        ItemImage12,
        ItemImage13,
        ItemImage14,
        ItemImage15,
        ItemImage16,
        ItemImage17,
        ItemImage18,
        ItemImage19,
        ItemImage20,
        ItemImage21,
        ItemImage22,
        ItemImage23,
        ItemImage24,
        ItemImage25,
        ItemImage26,
        ItemImage27,
        ItemImage28,
        ItemImage29,
        ItemImage30,
        ItemImage31,
        ItemImage32,
        ItemImage33,
        ItemImage34,
        ItemImage35

    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        UpdateInventory();
        Managers.Event.AddListener(Define.EVENT_TYPE.GetItem, this);
        Debug.Log("인벤유아이 초기화");
        GetButton((int)Buttons.Quit).gameObject.BindEvent((PointerEventData data) =>
        {
            Debug.Log("인벤아웃버튼");
            Managers.Game.IsUiPopUp = false;
            ClosePopupUI();
        });
    }

    private void UpdateInventory()
    {
        Debug.Log("인벤토리 업데이트");
        for(int i = 0; i<35; i++)
        {
            if (Managers.Inventory.Inventory[i].Type != ItemType.Null) 
            {
                Debug.Log("이미지 바꾸기");
                GetImage((int)(Images)i).sprite = Managers.Resource.Load<Sprite>(Managers.Inventory.Inventory[i].SpritePath);
                Debug.Log($"{Managers.Inventory.Inventory[i].SpritePath}");
            }
            else
            {
                GetImage((int)(Images)i).sprite = Managers.Resource.Load<Sprite>("Images/Item/EmptyItem");
            }
        }
    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type) 
        {
            case Define.EVENT_TYPE.GetItem:
                {
                    UpdateInventory();
                    break;
                }
        }
    }
}
