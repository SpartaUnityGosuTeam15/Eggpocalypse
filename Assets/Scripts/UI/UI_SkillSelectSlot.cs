using UnityEngine;
using UnityEngine.UI;

public class UI_SkillSelectSlot : MonoBehaviour
{
    private Button selectButton;

    private void Awake()
    {
        selectButton = GetComponent<Button>();
        selectButton.onClick.AddListener(SelectSkill);
    }

    public void SetSkill(SkillData skill)
    {

    }

    void SelectSkill()
    {
        //스킬 선택 기능
        Time.timeScale = 1f;
        UIManager.Instance.HideUI<UI_SelectSkill>();
    }
}
