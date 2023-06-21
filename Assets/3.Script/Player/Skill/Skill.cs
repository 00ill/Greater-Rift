using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    M2Skill,
    SkillSet1,
    SkillSet2,
    SkillSet3,
    SkillSet4
}

namespace M2Skill
{
    public enum M2List
    {
        SlashBlade
    }

}
public class SkillData
{
    public SkillType Type;
    public string Name;
    public float Cooldown;

    public int LevelLimit;

    public SkillData(SkillType type, string name, float cooldown, int levelLimit)
    {
        Type = type;
        Name = name;
        Cooldown = cooldown;
        LevelLimit = levelLimit;
    }
}

public class SkillGroup
{
    public List<SkillData> SkillList;
    public SkillGroup()
    {
        SkillList = new List<SkillData>();
    }
    public void InitSkillList()
    {
        SkillList.Add(new SkillData(SkillType.M2Skill, "BladeSlash", 5f, 2));
    }

}



public class Skill : MonoBehaviour
{
    public SkillGroup Skills;

    private void Awake()
    {
        Skills = new SkillGroup();
        Skills.InitSkillList();
    }

    private void Start()
    {
        Debug.Log(Skills.SkillList[(int)SkillType.M2Skill].Cooldown);
    }
}
