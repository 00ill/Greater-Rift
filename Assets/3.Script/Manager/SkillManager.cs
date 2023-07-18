using System.Collections.Generic;

public enum SkillName
{
    None,
    ShadowSlash,
    Kick,
    BladeSlash,
    DarkFlare,
    ShadowCleave,
    ShadowBlast,
    ShadowRain,
    ShadowImpulse,
    Karma,
    Greed,
    BloodFlood,
    ExposeOfDarkness
}

public enum SkillType
{
    M1Skill,
    M2Skill,
    SkillSet1,
    SkillSet2,
    SkillSet3,
    SkillSet4
}

public class SkillData
{
    public SkillName Name;
    public SkillType Type;
    public float Cooldown;
    public int ManaCost;
    public int LevelLimit;
    public string ResourceName;
    public float DamageCoefficient;
    public SkillData(SkillName name, SkillType type, float cooldown, int manaCost, int levelLimit, string resourceName, float damageCoefficient)
    {
        Name = name;
        Type = type;
        Cooldown = cooldown;
        ManaCost = manaCost;
        LevelLimit = levelLimit;
        ResourceName = resourceName;
        DamageCoefficient = damageCoefficient;
    }

}

public class SkillGroup
{
    public List<SkillData> SkillList;
    public SkillGroup()
    {
        SkillList = new List<SkillData>();
        InitSkillList();
    }
    public void InitSkillList()
    {
        SkillList.Add(new SkillData(SkillName.None, SkillType.M1Skill, 0f, 0, 1, "None", 0f));
        SkillList.Add(new SkillData(SkillName.ShadowSlash, SkillType.M1Skill, 0.5f, 0, 1, "ShadowSlash", 1.2f));
        SkillList.Add(new SkillData(SkillName.Kick, SkillType.M1Skill, 1f, 0, 5, "Kick", 1.2f));
        SkillList.Add(new SkillData(SkillName.BladeSlash, SkillType.M2Skill, 5f, 30, 2, "BladeSlash", 2.9f));
        SkillList.Add(new SkillData(SkillName.DarkFlare, SkillType.M2Skill, 15f, 10, 4, "DarkFlare", 1.2f));
        SkillList.Add(new SkillData(SkillName.ShadowCleave, SkillType.SkillSet1, 5f, 15, 4, "ShadowCleave", 2f));
        SkillList.Add(new SkillData(SkillName.ShadowBlast, SkillType.SkillSet1, 5f, 40, 29, "ShadowBlast", 3.3f));
        SkillList.Add(new SkillData(SkillName.ShadowRain, SkillType.SkillSet2, 15f, 120, 10, "ShadowRain", 1.8f));
        SkillList.Add(new SkillData(SkillName.ShadowImpulse, SkillType.SkillSet2, 15f, 120, 50, "ShadowImpulse", 3.6f));
        SkillList.Add(new SkillData(SkillName.Karma, SkillType.SkillSet3, 60f, 200, 15, "Karma", 0f));
        SkillList.Add(new SkillData(SkillName.Greed, SkillType.SkillSet3, 60f, 200, 25, "Greed", 0f));
        SkillList.Add(new SkillData(SkillName.BloodFlood, SkillType.SkillSet4, 45f, 200, 30, "BloodFlood", 4f));
        SkillList.Add(new SkillData(SkillName.ExposeOfDarkness, SkillType.SkillSet4, 45f, 200, 60, "ExposeOfDarkness", 1.8f));
    }

    public SkillData GetSkillData(SkillName skillName)
    {
        return SkillList[(int)skillName];
    }
}


public class SkillManager
{
    public SkillType CurrentChangeSkillType;
    public SkillName CurrentSelectSkillName;
    public SkillGroup Skills = new();
    public SkillName CurrentM1SKillName = SkillName.ShadowSlash;
    public SkillName CurrentM2SKillName;
    public SkillName CurrentNum1SKillName;
    public SkillName CurrentNum2SKillName;
    public SkillName CurrentNum3SKillName;
    public SkillName CurrentNum4SKillName;
    public float M1SkillCooldownRemain;
    public float M2SkillCooldownRemain;
    public float Num1SkillCooldownRemain;
    public float Num2SkillCooldownRemain;
    public float Num3SkillCooldownRemain;
    public float Num4SkillCooldownRemain;

    public int AdditionalDamage;
    public int AdditionalArmor;
    public int AdditionalMoveSpeed;


    public SkillData GetSkillData(SkillName skillNameToGet)
    {
        return Skills.GetSkillData(skillNameToGet);
    }

    public void StartM1Cooldown()
    {
        M1SkillCooldownRemain = Skills.GetSkillData(CurrentM1SKillName).Cooldown - Managers.Inventory.ItemTotal.CooldownReduction;
    }
    public void StartM2Cooldown()
    {
        M2SkillCooldownRemain = Skills.GetSkillData(CurrentM2SKillName).Cooldown - Managers.Inventory.ItemTotal.CooldownReduction;
    }
    public void StartNum1Cooldown()
    {
        Num1SkillCooldownRemain = Skills.GetSkillData(CurrentNum1SKillName).Cooldown - Managers.Inventory.ItemTotal.CooldownReduction;
    }
    public void StartNum2Cooldown()
    {
        Num2SkillCooldownRemain = Skills.GetSkillData(CurrentNum2SKillName).Cooldown - Managers.Inventory.ItemTotal.CooldownReduction;
    }
    public void StartNum3Cooldown()
    {
        Num3SkillCooldownRemain = Skills.GetSkillData(CurrentNum3SKillName).Cooldown - Managers.Inventory.ItemTotal.CooldownReduction;
    }
    public void StartNum4Cooldown()
    {
        Num4SkillCooldownRemain = Skills.GetSkillData(CurrentNum4SKillName).Cooldown - Managers.Inventory.ItemTotal.CooldownReduction;
    }

    public void ResetSkillCooldown()
    {
        M1SkillCooldownRemain = 0;
        M2SkillCooldownRemain = 0;
        Num1SkillCooldownRemain = 0;
        Num2SkillCooldownRemain = 0;
        Num3SkillCooldownRemain = 0;
        Num4SkillCooldownRemain = 0;
    }

}