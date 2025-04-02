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
        List<ChoiceSkillData> skillList = SkillManager.Instance.allSkillDict.Values.ToList(); //��ų�� ���� ������ ����Ʈ

        if (GameManager.Instance.player.GetComponent<PlayerCondition>().level == 1) //���� ���۽� ��ų�� ����
        {
            skillList.RemoveAll(item => item.type == 1);
        }
        else
        {
            if (GameManager.Instance.player.attackSkills.Count == 3) //�̹� ��� ��ų�� 3���� ��� ������ ���ݽ�ų ����
            {
                skillList.RemoveAll(item => item.type == 0 && item.level == 0);
            }
        }
        skillList.RemoveAll(item => item.level == 6); //���� ����

        if (skillList.Count == 0)
        {
            //��� ��ų�� ������ ������ ��� ü�� �� ��Ÿ �ɷ�
            return null;
        }

        //������ ��ų 3�� ����
        List<ChoiceSkillData> pickedSkills = new();

        for(int i = 0; i < 3; i++) //������ �ߺ��� ��� //���� ����Ʈ�� 3�� ������ ��� �ߺ����� ������ ������
        {
            int randomIndex = Random.Range(0, skillList.Count);
            pickedSkills.Add(skillList[randomIndex]);
        }

        return pickedSkills;
    }
}
