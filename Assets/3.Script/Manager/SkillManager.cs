using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillName
{
    Cutting,
    Kick,
    BladeSlash
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
    public string SpriteName;

    public SkillData(SkillName name, SkillType type, float cooldown, int levelLimit, string spriteName)
    {
        Name = name;
        Type = type;
        Cooldown = cooldown;
        LevelLimit = levelLimit;
        SpriteName = spriteName;
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
        SkillList.Add(new SkillData(SkillName.BladeSlash, SkillType.M2Skill, 5f, 2, "BladeSlash"));
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

    public SkillData GetSkillData(SkillName skillNameToGet)
    {
        return Skills.GetSkillData(skillNameToGet);
    }
}

