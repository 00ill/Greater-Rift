public class Define
{
    public enum Scene
    {
        None,
        LoadingScene,
        MainMenu,
        Town
    }

    public enum InteractType
    {
        Dungeon,
        Gem
    }

    public enum SortingOrder
    {
        CharacterSelectButton = 15,
        GameStartUI = 5,
        LogBookUI = 6,
        DetailInLogBook = 20,
        MouseInteraction = 100,
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
        NotEnoughMana

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
    public enum Skill_Type
    {
        M1Skill,
        M2Skill,
        Num1Skill,
        Num2Skill,
        Num3Skill,
        Num4Skill
    }
}
