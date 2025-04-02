using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillSelectSlot : MonoBehaviour
{
    [SerializeField] private Button selectButton;
    [SerializeField] private Image skillIcon;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillLevelText;
    ChoiceSkillData selectedSkill;

    private void Awake()
    {
        selectButton = GetComponent<Button>();
        selectButton.onClick.AddListener(SelectSkill);
    }

    public void SetSkill(ChoiceSkillData skill)
    {
        selectedSkill = skill;
        if (skill == null)
        {
            skillLevelText.text = "";
            skillNameText.text = "��� ��ų�� �����Դϴ�.";
            return;
        }
        skillLevelText.text = skill.level.ToString();
        skillNameText.text = skill.name;
        skillIcon.sprite = Resources.Load<Sprite>($"Arts/Skill/{skill.id}");
        skillIcon.color = Color.gray;
        //�̹����� ���߿� �߰�
    }

    void SelectSkill()
    {
        //��ų ���� ���
        //��ų �Ŵ����� �Լ��� ȣ�� 
        if (selectedSkill != null)
        {
            if (selectedSkill.type == 1) //���� 
            {
                SkillManager.Instance.statSkillList[selectedSkill.id].LevelUP();
            }
            else //���ݽ�ų
            {
                GameManager.Instance.player.GetAttackSkill(selectedSkill.id);
            }
            selectedSkill.level++;
        }
        Time.timeScale = 1f;
        UIManager.Instance.HideUI<UI_SelectSkill>();
    }
}
