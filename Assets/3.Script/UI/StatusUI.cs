using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusUI : UI_Popup, IListener
{
    enum Texts
    {
        Level,
        Life,
        Mana,
        Damage,
        Armor,
        MoveSpeed,
        CooldownReduction
    }

    enum Buttons
    {
        Exit
    }

    enum Images
    {
        Cursor
    }

    private PlayerControlInput _playerControlInput;
    private void Awake()
    {
        _playerControlInput = FindObjectOfType<PlayerControlInput>();
    }
    private void Start()
    {
        Init();
        UpdateStatus();
    }

    private void Update()
    {
        GetImage((int)Images.Cursor).transform.position = _playerControlInput.MouseInputPosition + new Vector3(13.3f, -31f, 0);
    }
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.Exit).gameObject.BindEvent((PointerEventData data) =>
        {
            Managers.Game.IsUiPopUp = false;
            base.ClosePopupUI();
        });

        Managers.Event.AddListener(Define.EVENT_TYPE.ChangeStatus, this);
    }

    private void UpdateStatus()
    {
        int level;
        int life;
        int mana;
        int damage;
        int armor;
        float moveSpeed;
        float cooldownReduction;

        level = Managers.Game.PlayerLevel;
        life = Managers.Inventory.ItemTotal.Life + Managers.Data.PlayerStatusDataDict[level].Life;
        mana = Managers.Inventory.ItemTotal.Mana + Managers.Data.PlayerStatusDataDict[level].Mana;
        damage = Managers.Inventory.ItemTotal.Damage + Managers.Data.PlayerStatusDataDict[level].Damage + Managers.Skill.AdditionalDamage;
        armor = Managers.Inventory.ItemTotal.Armor + Managers.Data.PlayerStatusDataDict[level].Armor + Managers.Skill.AdditionalArmor;
        moveSpeed = Managers.Inventory.ItemTotal.MoveSpeed + 10f + Managers.Skill.AdditionalMoveSpeed;
        cooldownReduction = Managers.Inventory.ItemTotal.CooldownReduction;

        GetText((int)Texts.Level).text = $"Level : {level}";
        GetText((int)Texts.Life).text = $"Life : {life}";
        GetText((int)Texts.Mana).text = $"Mana : {mana}";
        GetText((int)Texts.Damage).text = $"Damage : {damage}";
        GetText((int)Texts.Armor).text = $"Armor : {armor}";
        GetText((int)Texts.MoveSpeed).text = $"MoveSpeed : {moveSpeed}";
        GetText((int)Texts.CooldownReduction).text = $"CooldownReduction : {cooldownReduction}";

    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        if (Event_Type == Define.EVENT_TYPE.ChangeStatus)
        {
            UpdateStatus();
        }
    }
}
