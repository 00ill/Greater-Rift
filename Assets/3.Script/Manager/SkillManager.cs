using System.Collections.Generic;
using UnityEngine.Rendering;

public enum SkillName
{
    ShadowSlash,
    Kick,
    BladeSlash,
    DarkFlare
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
        SkillList.Add(new SkillData(SkillName.ShadowSlash, SkillType.M1Skill, 1f, 0, 1, "Cutting", 1.2f));
        SkillList.Add(new SkillData(SkillName.Kick, SkillType.M1Skill, 1f, 0, 5, "Kick", 1.2f));
        SkillList.Add(new SkillData(SkillName.BladeSlash, SkillType.M2Skill, 5f, 30, 1, "BladeSlash", 1.5f));
        SkillList.Add(new SkillData(SkillName.DarkFlare, SkillType.M2Skill, 3f, 10, 4, "DarkFlare", 1.2f));
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
    public SkillName CurrentM1SKillName;
    public SkillName CurrentM2SKillName = SkillName.BladeSlash;
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

    public SkillData GetSkillData(SkillName skillNameToGet)
    {
        return Skills.GetSkillData(skillNameToGet);
    }

    public void StartM1Cooldown()
    {
        M1SkillCooldownRemain = Skills.GetSkillData(CurrentM1SKillName).Cooldown;
    }
    public void StartM2Cooldown()
    {
        M2SkillCooldownRemain = Skills.GetSkillData(CurrentM2SKillName).Cooldown;
    }
    public void StartNum1Cooldown()
    {
        Num1SkillCooldownRemain = Skills.GetSkillData(CurrentNum1SKillName).Cooldown;
    }
    public void StartNum2Cooldown()
    {
        Num2SkillCooldownRemain = Skills.GetSkillData(CurrentNum2SKillName).Cooldown;
    }
    public void StartNum3Cooldown()
    {
        Num3SkillCooldownRemain = Skills.GetSkillData(CurrentNum3SKillName).Cooldown;
    }
    public void StartNum4Cooldown()
    {
        Num4SkillCooldownRemain = Skills.GetSkillData(CurrentNum4SKillName).Cooldown;
    }

}

