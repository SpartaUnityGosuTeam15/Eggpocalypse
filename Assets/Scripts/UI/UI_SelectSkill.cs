using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_SelectSkill : UI
{
    [SerializeField] private List<UI_SkillSelectSlot> skillSelectSlots = new(3);
    private List<SkillData> _selectedSkills = new();

    private List<AttackSkill> _playerAttackSkills;
    private List<StatSkill> _playerStatSkills;

    protected override void Awake()
    {
        base.Awake();

        Hide();
    }

    public void Init(List<AttackSkill> attackSkills, List<StatSkill> statSkills)
    {
        this._playerAttackSkills = attackSkills;
        this._playerStatSkills = statSkills;

        List<SkillData> pickedSkills = GetRandomSkills();

        for(int i = 0; i < 3; i++)
        {
            skillSelectSlots[i].SetSkill(pickedSkills[i]);
        }
    }

    public List<SkillData> GetRandomSkills()
    {
        List<SkillData> pickableSkills = new();
        List<SkillData> allAttackSkills = new();//추후 dataManager에서 가져옴
        List<SkillData> allStatSkills = new();//추후 dataManager에서 가져옴

        bool isAttackAllSelected = true;
        for(int i = 0; i < _playerAttackSkills.Count; i++)
        {
            AttackSkill skill = _playerAttackSkills[i];

            if (skill != null)
            {
                if (skill.skillLevel >= skill.maxLevel) allAttackSkills.Remove(skill.skillData);
            }
            else isAttackAllSelected = false;
        }

        bool isStatAllSelected = true;
        for(int i = 0; i < _playerStatSkills.Count; i++)
        {
            StatSkill skill = _playerStatSkills[i];

            if (skill != null)
            {
                if (skill.skillLevel >= skill.maxLevel) allStatSkills.Remove(skill.skillData);
            }
            else isStatAllSelected = false;
        }

        if (!isAttackAllSelected)
        {
            pickableSkills = allAttackSkills;
        }
        if (!isStatAllSelected)
        {
            foreach(SkillData skillData in allStatSkills)
            {
                pickableSkills.Add(skillData);
            }
        }

        List<SkillData> pickedSkills = new();
        for(int i = 0; i < 3; i++)
        {
            if(pickableSkills.Count > 0)
            {
                int randomIndex = Random.Range(0, pickableSkills.Count);
                pickableSkills.Add(pickableSkills[randomIndex]);
                pickableSkills.RemoveAt(randomIndex);
            }
            else
            {
                //체력회복 스킬을 pickedSkills에 추가해주는 메서드
            }
        }

        return pickedSkills;
    }
}
