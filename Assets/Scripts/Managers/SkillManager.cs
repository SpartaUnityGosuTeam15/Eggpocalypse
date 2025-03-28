using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public List<BaseSkill> skillList;

    private void Start()
    {
        //JsonLoader를 이용해서 스킬 정보 받기

    }
}
