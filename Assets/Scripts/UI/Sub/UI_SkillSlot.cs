using TMPro;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class UI_SkillSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private GameObject levelBackground;
    [SerializeField] private GameObject maxLevelImage;

    protected void Awake()
    {
        icon.gameObject.SetActive(false);
        levelBackground.SetActive(false);
        maxLevelImage.SetActive(false);
    }
}
