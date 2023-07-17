public class Define
{
    public enum Scene
    {
        None,
        LoadingScene,
        MainMenu,
        Town,
        NRDungeon,
        NRDungeon2,
        Desert

    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }

    public enum UIEvent
    {
        Click,
        OnDrag,
        PointerEnter,
        PointerExit,
        OnBeginDrag,
        OnEndDrag,
        OnDrop
    }

    public enum EVENT_TYPE
    {
        PlayerHpChange,
        PlayerManaChange,
        PlayerExpChange,
        CheckInteractableObject,
        SkillSettingUIOpen,
        Pause,
        LevelUp,
        SkillInCooldown,
        NotEnoughMana,
        OpenPortalInTown,
        PlayerPortalAlreadyOpen,
        PlayerDeath,
        FullInventory,
        GetItem,
        ChangeStatus,
        CountEnemyDeath,
        GuardianHpChange,
        TurnBackTown,
        AllPopupUIClose

    }

    public enum DB_Event
    {
        DuplicateID,
        NonExistID,
        WrongPassword,
        SuccessLogin,
        SuccessCreateNewAccount,
        DuplicateNickname,
        SuccessCreateNewPlayer,
        FullDataSlot,
        UpdateLoadData
    }
}
