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

        List<ChoiceSkillData> pickedSkills = GetRandomSkills();

        for(int i = 0; i < 3; i++)
        {
            if (pickedSkills == null)
                skillSelectSlots[i].SetSkill(null);
            else
                skillSelectSlots[i].SetSkill(pickedSkills[i]);
        }
    }

    public List<ChoiceSkillData> GetRandomSkills()
    {
        List<ChoiceSkillData> skillList = SkillManager.Instance.allSkillDict.Values.ToList(); //스킬과 스탯 포함한 리스트

        if (GameManager.Instance.player.GetComponent<PlayerCondition>().level == 1) //게임 시작시 스킬만 고르게
        {
            skillList.RemoveAll(item => item.type == 1);
        }
        else
        {
            if (GameManager.Instance.player.attackSkills.Count == 3) //이미 사용 스킬이 3개인 경우 나머지 공격스킬 제거
            {
                skillList.RemoveAll(item => item.type == 0 && item.level == 0);
            }
        }
        skillList.RemoveAll(item => item.level == 6); //만렙 제거

        if (skillList.Count == 0)
        {
            //모든 스킬의 레벨이 만렙일 경우 체력 및 기타 능력
            return null;
        }

        //실제로 스킬 3개 뽑음
        List<ChoiceSkillData> pickedSkills = new();

        for(int i = 0; i < 3; i++) //아이템 중복을 허용 //남은 리스트가 3개 이하인 경우 중복으로 무조건 나오게
        {
            int randomIndex = Random.Range(0, skillList.Count);
            pickedSkills.Add(skillList[randomIndex]);
        }

        return pickedSkills;
    }
}
