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
            skillNameText.text = "모든 스킬이 만렙입니다.";
            return;
        }
        skillLevelText.text = skill.level.ToString();
        skillNameText.text = skill.name;
        skillIcon.sprite = Resources.Load<Sprite>($"Arts/Skill/{skill.id}");
        skillIcon.color = Color.gray;
        //이미지도 나중에 추가
    }

    void SelectSkill()
    {
        //스킬 선택 기능
        //스킬 매니저의 함수를 호출 
        if (selectedSkill != null)
        {
            if (selectedSkill.type == 1) //스탯 
            {
                SkillManager.Instance.statSkillList[selectedSkill.id].LevelUP();
            }
            else //공격스킬
            {
                GameManager.Instance.player.GetAttackSkill(selectedSkill.id);
            }
            selectedSkill.level++;
        }
        Time.timeScale = 1f;
        UIManager.Instance.HideUI<UI_SelectSkill>();
    }
}
