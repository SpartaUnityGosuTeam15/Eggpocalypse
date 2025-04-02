using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillSelectSlot : MonoBehaviour
{
    [SerializeField] private Button selectButton;
    [SerializeField] private Image skillIcon;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillLevelText;

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
