using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillName
{
    Cutting,
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
    public int LevelLimit;
    public string ResourceName;

    public SkillData(SkillName name, SkillType type, float cooldown, int levelLimit, string spriteName)
    {
        Name = name;
        Type = type;
        Cooldown = cooldown;
        LevelLimit = levelLimit;
        ResourceName = spriteName;
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
        SkillList.Add(new SkillData(SkillName.Cutting, SkillType.M1Skill, 1f, 1, "Cutting"));
        SkillList.Add(new SkillData(SkillName.Kick, SkillType.M1Skill, 1f, 5, "Kick"));
        SkillList.Add(new SkillData(SkillName.BladeSlash, SkillType.M2Skill, 5f, 1, "BladeSlash"));
        SkillList.Add(new SkillData(SkillName.DarkFlare, SkillType.M2Skill, 3f, 4, "DarkFlare"));
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

