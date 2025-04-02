using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseSkill
{
    public string SkillName {  get; set; }
    public string SkillDescription { get; set; }
    public int SkillLevel { get; set; }
    public int MaxLevel { get;}


    public void LevelUP();
}
