using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : UI_Popup, IListener
{
    enum Buttons
    {
        Quit
    }
    enum Texts
    {
        ItemType,
        ItemLevel,
        ItemLife,
        ItemMana,
        ItemDamage,
        ItemArmor,
        ItemMoveSpeed,
        ItemCooldownReduction
    }
    enum Images
    {
        ItemImage1 = 0,
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
        ItemImage35,
        Cursor,
        SelectedItem,
        InventoryItemBackground,
        HelmImage = 101,
        CloaksImage,
        PantsImage,
        BootsImage,
        WeaponImage,
        GlovesImage
    }

    enum Objects
    {
        ItemDetail,
        TopCanvas
    }

    private PlayerControlInput _playerControlInput;
    private Transform _previousParent;

    private void Awake()
    {
        _playerControlInput = FindAnyObjectByType<PlayerControlInput>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        GetImage((int)Images.Cursor).transform.position = _playerControlInput.MouseInputPosition + new Vector3(13.3f, -31f, 0);
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(Objects));

        GetButton((int)Buttons.Quit).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Game.IsUiPopUp = false;
            ClosePopupUI();
        });

        Managers.Event.AddListener(Define.EVENT_TYPE.GetItem, this);

        UpdateInventory();
        UpdateEquipment();
        BindInventoryItem();
        BindEquipmentItem();
        GetObject((int)Objects.ItemDetail).SetActive(false);
        GetImage((int)Images.SelectedItem).gameObject.SetActive(false);


    }

    private void BindInventoryItem()
    {
        for (int i = 0; i < Managers.Inventory.ItemCountMax; i++)
        {
            int temp = i;


            GetImage((int)(Images)i).gameObject.BindEvent((PointerEventData data) =>
            {
                GetText((int)Texts.ItemType).text = string.Format($"Item Type : " + Enum.GetName(typeof(ItemType), Managers.Inventory.Inventory[temp].Type));
                GetText((int)Texts.ItemLevel).text = string.Format($"Item Level : {Managers.Inventory.Inventory[temp].Level}");
                GetText((int)Texts.ItemLife).text = string.Format($"Life : +{Managers.Inventory.Inventory[temp].Life}");
                GetText((int)Texts.ItemMana).text = string.Format($"Mana : +{Managers.Inventory.Inventory[temp].Mana}");
                GetText((int)Texts.ItemDamage).text = string.Format($"Damage : +{Managers.Inventory.Inventory[temp].Damage}");
                GetText((int)Texts.ItemArmor).text = string.Format($"Armor : +{Managers.Inventory.Inventory[temp].Armor}");
                GetText((int)Texts.ItemMoveSpeed).text = string.Format($"MoveSpeed : +{Managers.Inventory.Inventory[temp].MoveSpeed}");
                GetText((int)Texts.ItemCooldownReduction).text = string.Format($"CooldownReduction : -{Managers.Inventory.Inventory[temp].CooldownReduction}");

                if (Managers.Inventory.Inventory[temp].Type != ItemType.Null)
                {
                    GetObject((int)Objects.ItemDetail).SetActive(true);
                }

                Managers.Inventory.PointedItemIndex = temp;
                GetImage((int)(Images)temp).color = new UnityEngine.Color(255, 255, 255, 120);
            }, Define.UIEvent.PointerEnter);

            GetImage((int)(Images)i).gameObject.BindEvent((PointerEventData data) =>
            {
                GetObject((int)Objects.ItemDetail).SetActive(false);
                GetImage((int)(Images)temp).color = new UnityEngine.Color(255, 255, 255, 255);
                Managers.Inventory.PointedItemIndex = -1;
            }, Define.UIEvent.PointerExit);

            GetImage((int)(Images)i).gameObject.BindEvent((PointerEventData data) =>
            {
                if (Managers.Inventory.Inventory[temp].Type != ItemType.Null)
                {
                    Managers.Inventory.SelectedItemIndex = temp;
                    _previousParent = GetImage((int)(Images)temp).transform.parent;
                    GetImage((int)(Images)temp).transform.SetParent(GetObject((int)Objects.TopCanvas).transform);
                    GetObject((int)Objects.TopCanvas).GetComponent<CanvasGroup>().blocksRaycasts = false;

                    GetImage((int)Images.SelectedItem).sprite = Managers.Resource.Load<Sprite>(Managers.Inventory.Inventory[temp].SpritePath);
                    GetImage((int)Images.SelectedItem).transform.position = _playerControlInput.MouseInputPosition;
                    GetImage((int)Images.SelectedItem).gameObject.SetActive(true);
                }

            }, Define.UIEvent.OnBeginDrag);

            GetImage((int)(Images)i).gameObject.BindEvent((PointerEventData data) =>
            {
                if (Managers.Inventory.Inventory[temp].Type != ItemType.Null)
                {
                    GetImage((int)Images.SelectedItem).transform.position = _playerControlInput.MouseInputPosition;
                }
            }, Define.UIEvent.OnDrag);

            GetImage((int)(Images)i).gameObject.BindEvent((PointerEventData data) =>
            {
                GetImage((int)(Images)temp).transform.SetParent(_previousParent);
                GetImage((int)Images.SelectedItem).gameObject.SetActive(false);

                if (Managers.Inventory.PointedItemIndex >= (int)EquipmentIndex.HelmIndex)
                {
                    Managers.Inventory.EquipItem();
                    UpdateEquipment();
                    UpdateInventory();
                    return;
                }

                if (Managers.Inventory.PointedItemIndex >= 0)
                {
                    Managers.Inventory.ChangeItem();
                    UpdateEquipment();
                    UpdateInventory();
                }


            }, Define.UIEvent.OnEndDrag);
        }
    }

    private void BindEquipmentItem()
    {
        for (int i = (int)Images.HelmImage; i <= (int)Images.GlovesImage; i++)
        {
            int temp = i;
            GetImage((int)(Images)i - Managers.Inventory.IndexInterval).gameObject.BindEvent((PointerEventData data) =>
            {
                GetText((int)Texts.ItemType).text = string.Format($"Item Type : " + Enum.GetName(typeof(ItemType), Managers.Inventory.Equipment[temp].Type));
                GetText((int)Texts.ItemLevel).text = string.Format($"Item Level : {Managers.Inventory.Equipment[temp].Level}");
                GetText((int)Texts.ItemLife).text = string.Format($"Life : +{Managers.Inventory.Equipment[temp].Life}");
                GetText((int)Texts.ItemMana).text = string.Format($"Mana : +{Managers.Inventory.Equipment[temp].Mana}");
                GetText((int)Texts.ItemDamage).text = string.Format($"Damage : +{Managers.Inventory.Equipment[temp].Damage}");
                GetText((int)Texts.ItemArmor).text = string.Format($"Armor : +{Managers.Inventory.Equipment[temp].Armor}");
                GetText((int)Texts.ItemMoveSpeed).text = string.Format($"MoveSpeed : +{Managers.Inventory.Equipment[temp].MoveSpeed}");
                GetText((int)Texts.ItemCooldownReduction).text = string.Format($"CooldownReduction : -{Managers.Inventory.Equipment[temp].CooldownReduction}");

                if (Managers.Inventory.Equipment[temp].Type != ItemType.Null)
                {
                    GetObject((int)Objects.ItemDetail).SetActive(true);
                }

                Managers.Inventory.PointedItemIndex = temp;
            }, Define.UIEvent.PointerEnter);

            GetImage((int)(Images)i - Managers.Inventory.IndexInterval).gameObject.BindEvent((PointerEventData data) =>
            {
                GetObject((int)Objects.ItemDetail).SetActive(false);
                GetImage((int)(Images)temp - Managers.Inventory.IndexInterval).color = new UnityEngine.Color(255, 255, 255, 255);
                Managers.Inventory.PointedItemIndex = -1;
            }, Define.UIEvent.PointerExit);

            GetImage((int)(Images)i - Managers.Inventory.IndexInterval).gameObject.BindEvent((PointerEventData data) =>
            {
                if (Managers.Inventory.Equipment[temp].Type != ItemType.Null)
                {
                    Managers.Inventory.SelectedItemIndex = temp;
                    _previousParent = GetImage((int)(Images)temp - Managers.Inventory.IndexInterval).transform.parent;
                    GetImage((int)(Images)temp - Managers.Inventory.IndexInterval).transform.SetParent(GetObject((int)Objects.TopCanvas).transform);
                    GetObject((int)Objects.TopCanvas).GetComponent<CanvasGroup>().blocksRaycasts = false;

                    GetImage((int)Images.SelectedItem).sprite = Managers.Resource.Load<Sprite>(Managers.Inventory.Equipment[temp].SpritePath);
                    GetImage((int)Images.SelectedItem).transform.position = _playerControlInput.MouseInputPosition;
                    GetImage((int)Images.SelectedItem).gameObject.SetActive(true);
                }
            }, Define.UIEvent.OnBeginDrag);

            GetImage((int)(Images)i - Managers.Inventory.IndexInterval).gameObject.BindEvent((PointerEventData data) =>
            {
                if (Managers.Inventory.Equipment[temp].Type != ItemType.Null)
                {
                    GetImage((int)Images.SelectedItem).transform.position = _playerControlInput.MouseInputPosition;
                }
            }, Define.UIEvent.OnDrag);

            GetImage((int)(Images)i - Managers.Inventory.IndexInterval).gameObject.BindEvent((PointerEventData data) =>
            {
                GetImage((int)(Images)temp - Managers.Inventory.IndexInterval).transform.SetParent(_previousParent);
                GetImage((int)Images.SelectedItem).gameObject.SetActive(false);

                if (Managers.Inventory.PointedItemIndex < 0)
                {
                    return;
                }
                if (Managers.Inventory.PointedItemIndex >= 101)
                {
                    return;
                }
                if (Managers.Inventory.Inventory[Managers.Inventory.PointedItemIndex].Type == ItemType.Null)
                {
                    Managers.Inventory.Disarm();
                    UpdateEquipment();
                    UpdateInventory();
                }

            }, Define.UIEvent.OnEndDrag);
        }
    }

    private void UpdateInventory()
    {
        for (int i = 0; i < Managers.Inventory.ItemCountMax; i++)
        {
            if (Managers.Inventory.Inventory[i].Type != ItemType.Null)
            {
                GetImage((int)(Images)i).sprite = Managers.Resource.Load<Sprite>(Managers.Inventory.Inventory[i].SpritePath);
            }
            else
            {
                GetImage((int)(Images)i).sprite = Managers.Resource.Load<Sprite>("Images/Item/EmptyItem");
            }
        }
    }

    private void UpdateEquipment()
    {
        for (int i = (int)Images.HelmImage; i <= (int)Images.GlovesImage; i++)
        {
            int temp = i;
            if (Managers.Inventory.Equipment[temp].Type != ItemType.Null)
            {
                GetImage((int)(Images)temp - Managers.Inventory.IndexInterval).sprite = Managers.Resource.Load<Sprite>(Managers.Inventory.Equipment[temp].SpritePath);
            }
            else
            {
                switch (temp)
                {
                    case (int)Images.HelmImage:
                        {
                            GetImage((int)(Images)temp - Managers.Inventory.IndexInterval).sprite = Managers.Resource.Load<Sprite>("Images/Item/DefaultHelm");
                            break;
                        }
                    case (int)Images.CloaksImage:
                        {
                            GetImage((int)(Images)temp - Managers.Inventory.IndexInterval).sprite = Managers.Resource.Load<Sprite>("Images/Item/DefaultCloaks");
                            break;
                        }
                    case (int)Images.PantsImage:
                        {
                            GetImage((int)(Images)temp - Managers.Inventory.IndexInterval).sprite = Managers.Resource.Load<Sprite>("Images/Item/DefaultPants");
                            break;
                        }
                    case (int)Images.BootsImage:
                        {
                            GetImage((int)(Images)temp - Managers.Inventory.IndexInterval).sprite = Managers.Resource.Load<Sprite>("Images/Item/DefaultBoots");
                            break;
                        }
                    case (int)Images.WeaponImage:
                        {
                            GetImage((int)(Images)temp - Managers.Inventory.IndexInterval).sprite = Managers.Resource.Load<Sprite>("Images/Item/DefaultWeapon");
                            break;
                        }
                    case (int)Images.GlovesImage:
                        {
                            GetImage((int)(Images)temp - Managers.Inventory.IndexInterval).sprite = Managers.Resource.Load<Sprite>("Images/Item/DefaultGloves");
                            break;
                        }
                }

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
