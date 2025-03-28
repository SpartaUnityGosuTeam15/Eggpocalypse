using TMPro;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class UI_SkillSlot : UI
{
    [SerializeField] private Image icon;
    
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private GameObject levelBackground;
    [SerializeField] private GameObject maxLevelImage;

    protected override void Awake()
    {
        base.Awake();

        icon.gameObject.SetActive(false);
        levelBackground.SetActive(false);
        maxLevelImage.SetActive(false);
    }
}
