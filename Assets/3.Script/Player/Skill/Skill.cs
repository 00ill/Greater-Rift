//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.InteropServices.WindowsRuntime;
//using UnityEngine;


//public enum SkillName
//{
//    Cutting,
//    Kick,
//    BladeSlash
//}

//public enum SkillType
//{
//    M1Skill,
//    M2Skill,
//    SkillSet1,
//    SkillSet2,
//    SkillSet3,
//    SkillSet4
//}

//public class SkillData
//{
//    public SkillName Name;
//    public SkillType Type;
//    public float Cooldown;
//    public int LevelLimit;
//    public string SpriteName;

//    public SkillData(SkillName name, SkillType type, float cooldown, int levelLimit, string spriteName)
//    {
//        Name = name;
//        Type = type;
//        Cooldown = cooldown;
//        LevelLimit = levelLimit;
//        SpriteName = spriteName;
//    }

//}

//public class SkillGroup
//{
//    public List<SkillData> SkillList;
//    public SkillGroup()
//    {
//        SkillList = new List<SkillData>();
//    }
//    public void InitSkillList()
//    {
//        SkillList.Add(new SkillData(SkillName.Cutting, SkillType.M1Skill, 1f, 1, "Cutting"));
//        SkillList.Add(new SkillData(SkillName.Kick, SkillType.M1Skill, 1f, 5, "Kick"));
//        SkillList.Add(new SkillData(SkillName.BladeSlash, SkillType.M2Skill, 5f, 2, "BladeSlash"));
//    }

//    public SkillData GetSkillData(SkillName skillName)
//    {
//        return SkillList[(int)skillName];
//    }
//}



//public class Skill : MonoBehaviour
//{
//    public SkillGroup Skills;
//    public Skill()
//    {
//        Skills.InitSkillList();
//    }
//    private void Awake()
//    {
//        Skills = new SkillGroup();
//        Skills.InitSkillList();
//    }

//    private void Start()
//    {
//        Debug.Log(Skills.SkillList[(int)SkillType.M2Skill].Cooldown);
//    }

//    public SkillData GetSkillData(SkillName skillNameToGet)
//    {
//        return Skills.GetSkillData(skillNameToGet);
//    }
//}
